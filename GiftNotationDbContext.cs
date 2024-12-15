using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Windows.Controls;
using GiftNotation.Models;
using Microsoft.EntityFrameworkCore;

namespace GiftNotation.Data;

public partial class GiftNotationDbContext : DbContext
{
    public GiftNotationDbContext(DbContextOptions<GiftNotationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Contact> Contacts { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<EventType> EventTypes { get; set; }
    public DbSet<Gifts> Gifts { get; set; }
    public DbSet<RelpType> RelpTypes { get; set; }
    public DbSet<Status> Statuses { get; set; }
    public DbSet<GiftContact> GiftContacts { get; set; }
    public DbSet<GiftEvent> GiftEvents { get; set; }
    public DbSet<EventContact> EventContacts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Gifts>()
            .HasKey(ec => new { ec.GiftId });
        // Конфигурация таблицы-связи `event_contact`
        modelBuilder.Entity<EventContact>()
            .HasKey(ec => new { ec.EventId, ec.ContactId });

        modelBuilder.Entity<EventContact>()
            .HasOne(ec => ec.Event)
            .WithMany(e => e.EventContacts)
            .HasForeignKey(ec => ec.EventId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<EventContact>()
            .HasOne(ec => ec.Contact)
            .WithMany(c => c.EventContacts)
            .HasForeignKey(ec => ec.ContactId)
            .OnDelete(DeleteBehavior.Cascade);

        // Конфигурация таблицы-связи `gift_contact`
        modelBuilder.Entity<GiftContact>()
            .HasKey(gc => new { gc.GiftId, gc.ContactId });

        modelBuilder.Entity<GiftContact>()
            .HasOne(gc => gc.Gift)
            .WithMany(g => g.GiftContacts)
            .HasForeignKey(gc => gc.GiftId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<GiftContact>()
            .HasOne(gc => gc.Contact)
            .WithMany(c => c.GiftContacts)
            .HasForeignKey(gc => gc.ContactId)
            .OnDelete(DeleteBehavior.Cascade);

        // Конфигурация таблицы-связи `gift_event`
        modelBuilder.Entity<GiftEvent>()
            .HasKey(ge => new { ge.GiftId, ge.EventId });

        modelBuilder.Entity<GiftEvent>()
            .HasOne(ge => ge.Gift)
            .WithMany(g => g.GiftEvents)
            .HasForeignKey(ge => ge.GiftId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<GiftEvent>()
            .HasOne(ge => ge.Event)
            .WithMany(e => e.GiftEvents)
            .HasForeignKey(ge => ge.EventId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Status>().HasIndex(x => x.StatusName).IsUnique();
        modelBuilder.Entity<EventType>().HasIndex(x => x.EventTypeName).IsUnique();
        modelBuilder.Entity<RelpType>().HasIndex(x => x.RelpTypeName).IsUnique();

        modelBuilder.Entity<Status>().HasData(
            new Status { StatusId = 1, StatusName = "В процессе покупки" },
            new Status { StatusId = 2, StatusName = "Куплен" },
            new Status { StatusId = 3, StatusName = "Упакован" },
            new Status { StatusId = 4, StatusName = "Подарен" }
        );

        modelBuilder.Entity<EventType>().HasData(
            new EventType { EventTypeId = 1, EventTypeName = "День Рождения" },
            new EventType { EventTypeId = 2, EventTypeName = "23 февраля" },
            new EventType { EventTypeId = 3, EventTypeName = "Годовщина" },
            new EventType { EventTypeId = 4, EventTypeName = "Новый год" },
            new EventType { EventTypeId = 5, EventTypeName = "8 марта" },
            new EventType { EventTypeId = 6, EventTypeName = "9 мая" },
            new EventType { EventTypeId = 7, EventTypeName = "Рождество" },
            new EventType { EventTypeId = 8, EventTypeName = "Свадьба" },
            new EventType { EventTypeId = 9, EventTypeName = "Просто подарочек" },
            new EventType { EventTypeId = 10, EventTypeName = "Важное событие" }
        );

        modelBuilder.Entity<RelpType>().HasData(
             new RelpType { RelpTypeId = 1, RelpTypeName = "Друг" },
             new RelpType { RelpTypeId = 2, RelpTypeName = "Родственник" }, 
             new RelpType { RelpTypeId = 3, RelpTypeName = "Коллега" },
             new RelpType { RelpTypeId = 4, RelpTypeName = "Знакомый" }
        );

        base.OnModelCreating(modelBuilder);
    }
}


