using GiftNotation.Data;
using GiftNotation.Models;
using GiftNotation.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiftNotation.Services
{
    public class EventService
    {
        private readonly GiftNotationDbContext _context;
        public event Action StateChanged;

        public EventService(GiftNotationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DisplayEventModel>> GetEventsAsync()
        {
            return await _context.Events
                .Include(e => e.EventType) // Подгружаем тип события
                .Include(e => e.EventContacts) // Подгружаем связи событие-контакты
                    .ThenInclude(ec => ec.Contact) // Подгружаем сами контакты
                .Select(e => new DisplayEventModel
                {
                    EventId = e.EventId,
                    EventName = e.EventName,
                    EventDate = e.EventDate,
                    EventTypeName = e.EventType.EventTypeName ?? string.Empty,
                    ContactsOnEvent = new ObservableCollection<Contact?>(
                        e.EventContacts.Select(ec => ec.Contact)
                    )
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<Event>> GetAllEvents()
        {
            return await _context.Events.ToListAsync();
        }

        public async Task<IEnumerable<DateTime>> GetAllEventDates()
        {
            return await _context.Events.Select(d => d.EventDate).ToListAsync();
        }

        public async Task AddEventAsync(DisplayEventModel eventModel, AddEventViewModel addEventViewModel)
        {
            var typeId = await EnsureTypeAsync(eventModel.EventTypeName);

            var event_ = new Event
            {
                EventName = eventModel.EventName,
                EventDate = eventModel.EventDate,
                EventTypeId = typeId
            };

            _context.Events.Add(event_);
            await _context.SaveChangesAsync();

            var addedEvent = await _context.Events
            .OrderByDescending(e => e.EventId) // Упорядочиваем по убыванию ID
            .FirstOrDefaultAsync();

            if (addedEvent != null)
            {
                // 5. Создаем связи между событием и контактами
                foreach (var contact in addEventViewModel.ContactsOnEvent)
                {
                    var eventContact = new EventContact
                    {
                        EventId = addedEvent.EventId,
                        ContactId = contact.ContactId
                    };

                    // Добавляем связь в контекст
                    _context.EventContacts.Add(eventContact);
                }

                // 6. Сохраняем изменения
                await _context.SaveChangesAsync();
            }

        }

        public async Task<int?> EnsureTypeAsync(string? typeName)
        {
            if (string.IsNullOrEmpty(typeName))
                return null; // Возвращаем null, если статус не указан

            // Проверяем, существует ли статус
            var existingStatusId = await _context.EventTypes
                .Where(s => s.EventTypeName == typeName)
                .Select(s => s.EventTypeId)
                .FirstOrDefaultAsync();

            if (existingStatusId != 0)
                return existingStatusId;

            return null;
        }

        public async Task DeleteEventAsync(int eventId)
        {
            var event_ = await _context.Events
                .Include(g => g.EventContacts)
                .Include(g => g.GiftEvents)
                .FirstOrDefaultAsync(g => g.EventId == eventId);

            if (event_ != null)
            {
                _context.EventContacts.RemoveRange(event_.EventContacts);
                _context.GiftEvents.RemoveRange(event_.GiftEvents);

                _context.Events.Remove(event_);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<DisplayEventModel>> GetDisplayEventModelByID(int id)
        {
            var event_ = await _context.Events
                .Include(e => e.EventType) // Подгружаем тип события
                .Include(e => e.EventContacts) // Подгружаем связи событие-контакты
                    .ThenInclude(ec => ec.Contact) // Подгружаем сами контакты
                .Where(g => g.EventId == id)
                .FirstOrDefaultAsync();

            return await _context.Events
                .Include(e => e.EventType) // Подгружаем тип события
                .Include(e => e.EventContacts) // Подгружаем связи событие-контакты
                    .ThenInclude(ec => ec.Contact) // Подгружаем сами контакты
                .Where(g => g.EventId == id)
                .Select(e => new DisplayEventModel
                {
                    EventId = e.EventId,
                    EventDate = e.EventDate,
                    EventName = e.EventName,
                    EventTypeName = e.EventType.EventTypeName,

                })
                .ToListAsync();

        }
        public async Task UpdateEventAsync(DisplayEventModel eventModel, ChangeEventViewModel addEventViewModel)
        {
            // 1. Получаем событие по ID (предполагается, что eventModel содержит EventId)
            var existingEvent = await _context.Events
                .FirstOrDefaultAsync(e => e.EventId == eventModel.EventId);

            if (existingEvent == null)
            {
                // Если событие не найдено, можно выбросить исключение или обработать ошибку
                throw new Exception("Событие не найдено");
            }

            // 2. Обновляем информацию о событии
            existingEvent.EventName = eventModel.EventName;
            existingEvent.EventDate = eventModel.EventDate;
            existingEvent.EventTypeId = await EnsureTypeAsync(eventModel.EventTypeName);

            // 3. Удаляем старые связи между событием и контактами
            var eventContactsToRemove = _context.EventContacts
                .Where(ec => ec.EventId == eventModel.EventId);

            _context.EventContacts.RemoveRange(eventContactsToRemove);

            // 4. Создаем новые связи между событием и контактами
            foreach (var contact in addEventViewModel.ContactsOnEvent)
            {
                var eventContact = new EventContact
                {
                    EventId = eventModel.EventId,
                    ContactId = contact.ContactId
                };

                // Добавляем связь в контекст
                _context.EventContacts.Add(eventContact);
            }

            // 5. Сохраняем изменения
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<EventType>> GetEventTypesAsync()
        {
            return await _context.EventTypes.ToListAsync();
        }

    }
}

