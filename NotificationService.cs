using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Linq;
using GiftNotation.Data;
using GiftNotation.Models;
using Microsoft.EntityFrameworkCore;

namespace GiftNotation.Services
{
    public class NotificationService
    {
        private readonly GiftNotationDbContext _context;

        public NotificationService(GiftNotationDbContext context){

            _context = context;
        }

        public async Task<IEnumerable<Event>> GetAllUpcomingEventsAsync()
        {
            var today = DateTime.Now.Date;

            // Получаем события с сегодняшнего дня
            return await _context.Events
                .Where(e => e.EventDate >= today)
                .OrderBy(e => e.EventDate) // Упорядочиваем по дате
                .ToListAsync();
        }

        public void CheckNextEvents(IEnumerable<Event> events)
        {
            foreach(var event_ in events)
            {
                SendHolidayNotification(event_.EventDate, event_.EventName);
            }
        }

        // Метод для отправки уведомлений за день и за неделю до события
        public void SendHolidayNotification(DateTime eventDate, string eventName)
        {
            var now = DateTime.Now;

            // Проверяем, если до события остается 1 день или 7 дней
            if (eventDate.Date == now.Date.AddDays(1)) // За 1 день до события
            {
                ShowToast($"Напоминание: Завтра праздник '{eventName}'");
            }
            else if (eventDate.Date == now.Date.AddDays(7)) // За 7 дней до события
            {
                ShowToast($"Напоминание: Через неделю праздник '{eventName}'");
            }
        }

        // Отправка Toast уведомления
        private void ShowToast(string message)
        {
            new ToastContentBuilder()
                .AddArgument("action", "viewEvent")
                .AddArgument("eventId", 1) // Пример, нужно заменить на реальный ID события
                .AddText(message)
                .Show();
        }
    }
}
