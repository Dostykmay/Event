using GiftNotation.Data;
using GiftNotation.Models;
using GiftNotation.Commands.ContactCommands;
using GiftNotation.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace GiftNotation.Services
{
    public class GiftService 
    {
        private readonly GiftNotationDbContext _context;
        public event Action StateChanged;

        public GiftService(GiftNotationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DisplayGiftModel>> GetGiftAsync()
        {
            return await _context.Gifts
                .Include(g => g.Status)
                .Include(g => g.GiftContacts)
                    .ThenInclude(gc => gc.Contact)
                .Include(g => g.GiftEvents)
                    .ThenInclude(ge => ge.Event)
                .Select(g => new DisplayGiftModel
                {
                    GiftId = g.GiftId,
                    GiftName = g.GiftName ?? string.Empty,
                    Description = g.Description ?? string.Empty,
                    Price = g.Price,
                    GiftPic = g.GiftPic ?? string.Empty,
                    Url = g.Url ?? string.Empty,
                    StatusName = g.Status.StatusName ?? string.Empty,

                    ContactId = g.GiftContacts.Select(gc => gc.ContactId).First(),
                    ContactName = g.GiftContacts.Select(gc => gc.Contact.ContactName ?? string.Empty).FirstOrDefault(),

                    EventId = g.GiftEvents.Select(ge => ge.EventId).First(),
                    EventName = g.GiftEvents.Select(ge => ge.Event.EventName ?? string.Empty).FirstOrDefault()
                })
                .ToListAsync();
        }


        public async Task AddGiftAsync(DisplayGiftModel giftModel)
        {

            // Убеждаемся, что статус существует, или добавляем его
            var statusId = await EnsureStatusAsync(giftModel.StatusName);

            var gift = new Gifts
            {
                GiftName = giftModel.GiftName ?? string.Empty,
                Description = giftModel.Description ?? string.Empty,
                Price = giftModel.Price,
                GiftPic = giftModel.GiftPic ?? string.Empty,
                Url = giftModel.Url ?? string.Empty,
                StatusId = statusId // Может быть null
            };

            _context.Gifts.Add(gift);
            await _context.SaveChangesAsync();


            var addedGift = await _context.Gifts
        .OrderByDescending(e => e.GiftId) // Упорядочиваем по убыванию идентификатора
        .FirstOrDefaultAsync();

            if (giftModel.SelectedEventId != null)
            {
                var newGiftEvent = new GiftEvent()
                {
                    EventId = giftModel.SelectedEventId ?? 0,
                    GiftId = addedGift.GiftId
                };
                _context.GiftEvents.Add(newGiftEvent);
            }
            if (giftModel.SelectedEventId != null)
            {
                var newGiftContact = new GiftContact()
                {
                    ContactId = giftModel.SelectedContactId ?? 0,
                    GiftId = addedGift.GiftId
                };
                _context.GiftContacts.Add(newGiftContact);

                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteGiftAsync(int giftId)
        {
            var gift = await _context.Gifts
                .Include(g => g.GiftContacts)
                .Include(g => g.GiftEvents)
                .FirstOrDefaultAsync(g => g.GiftId == giftId);

            if (gift != null)
            {
                // Удаляем связанные контакты и события
                _context.GiftContacts.RemoveRange(gift.GiftContacts);
                _context.GiftEvents.RemoveRange(gift.GiftEvents);

                // Удаляем сам подарок
                _context.Gifts.Remove(gift);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<DisplayGiftModel>> GetDisplayGiftModelByID(int giftId)
        {
                var gift = await _context.Gifts
            .Include(g => g.Status)
            .Include(g => g.GiftContacts)
                .ThenInclude(gc => gc.Contact)
            .Include(g => g.GiftEvents)
                .ThenInclude(ge => ge.Event)
            .Where(g => g.GiftId == giftId)
            .FirstOrDefaultAsync();

            return await _context.Gifts
                 .Include(g => g.Status)
                 .Include(g => g.GiftContacts)
                     .ThenInclude(gc => gc.Contact)
                 .Where(g => g.GiftId == giftId)
                 .Include(g => g.GiftEvents)
                     .ThenInclude(ge => ge.Event)
                 .Where(g => g.GiftId == giftId)
                 .Select(g => new DisplayGiftModel
                 {
                     GiftId = g.GiftId,
                     GiftName = g.GiftName ?? string.Empty,
                     Description = g.Description ?? string.Empty,
                     Price = g.Price,
                     GiftPic = g.GiftPic ?? string.Empty,
                     Url = g.Url ?? string.Empty,
                     StatusName = g.Status.StatusName ?? string.Empty,
                     ContactId = g.GiftContacts
                     .Where(gc => gc.GiftId == gift.GiftId)
                     .Select(gc => gc.ContactId)
                     .First(),
                     ContactName = g.GiftContacts
                     .Where(gc => gc.GiftId == gift.GiftId)
                     .Select(gc => gc.Contact.ContactName)
                     .FirstOrDefault() ?? string.Empty,
                     EventId = g.GiftEvents
                     .Where(gc => gc.GiftId == gift.GiftId)
                     .Select(gc => gc.EventId)
                     .First(),
                     EventName = g.GiftEvents
                     .Where(ge => ge.GiftId == gift.GiftId)
                     .Select(ge => ge.Event.EventName)
                     .FirstOrDefault() ?? string.Empty
                 })
                 .ToListAsync();
        }



        public async Task<IEnumerable<Status>> GetAllStatuses()
        {
            return await _context.Statuses.ToListAsync();
        }

        public async Task UpdateGiftAsync(DisplayGiftModel _gift)
        {
            var giftChange = await _context.Gifts.FindAsync(_gift.GiftId);
            var giftEventChange = await _context.GiftEvents.FindAsync(_gift.GiftId, _gift.EventId);
            var giftContactChange = await _context.GiftContacts.FindAsync(_gift.GiftId, _gift.ContactId);

            giftChange.GiftName = _gift.GiftName ?? string.Empty;
            giftChange.Description = _gift.Description ?? string.Empty;
            giftChange.Url = _gift.Url ?? string.Empty;
            giftChange.Price = _gift.Price;
            giftChange.GiftPic = _gift.GiftPic ?? string.Empty;
            giftChange.StatusId = await EnsureStatusAsync(_gift.StatusName);

            if(giftEventChange != null)
            {
                _context.GiftEvents.Remove(giftEventChange);
                var newGiftEvent = new GiftEvent()
                {
                    EventId = _gift.SelectedEventId ?? 0,
                    GiftId = _gift.GiftId
                };
                _context.GiftEvents.Add(newGiftEvent);
            }
            else
            {
                if (_gift.SelectedEventId != null)
                {
                    var newGiftEvent = new GiftEvent()
                    {
                        EventId = _gift.SelectedEventId ?? 0,
                        GiftId = _gift.GiftId
                    };
                    
                    _context.GiftEvents.Add(newGiftEvent);
                }
            }

            if (giftContactChange != null)
            {
                _context.GiftContacts.Remove(giftContactChange);
                var newGiftContact = new GiftContact()
                {
                    ContactId = _gift.SelectedContactId ?? 0,
                    GiftId = _gift.GiftId
                };
                _context.GiftContacts.Add(newGiftContact);
            }
            else
            {
                if (_gift.SelectedContactId != null)
                {
                    var newGiftContact = new GiftContact()
                    {
                        ContactId = _gift.SelectedContactId ?? 0,
                        GiftId = _gift.GiftId
                    };

                    _context.GiftContacts.Add(newGiftContact);
                }
            }

            await _context.SaveChangesAsync();
        }

        public async Task<int?> EnsureStatusAsync(string? statusName)
        {
            if (string.IsNullOrEmpty(statusName))
                return null; // Возвращаем null, если статус не указан

            // Проверяем, существует ли статус
            var existingStatusId = await _context.Statuses
                .Where(s => s.StatusName == statusName)
                .Select(s => s.StatusId)
                .FirstOrDefaultAsync();

            if (existingStatusId != 0)
                return existingStatusId;

            return null;
        }

        public async Task<IEnumerable<Gifts>> GetAllGifts()
        {
            return await _context.Gifts.ToListAsync();
        }

    }
}