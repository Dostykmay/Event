using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Policy;
using Microsoft.EntityFrameworkCore;

namespace GiftNotation.Models;

[Index(nameof(EventTypeName), IsUnique = true)]
public class EventType
{
    public int EventTypeId { get; set; }
    public string EventTypeName { get; set; }
    public ICollection<Event> Events { get; set; }
}

