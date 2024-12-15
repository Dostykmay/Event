using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GiftNotation.Models;

public class Contact
{
    public int ContactId { get; set; }
    public string? ContactName { get; set; }
    public DateTime Bday { get; set; }

    public int? RelpTypeId { get; set; }
    public RelpType RelpType { get; set; }

    public ICollection<EventContact> EventContacts { get; set; }
    public ICollection<GiftContact> GiftContacts { get; set; }
}

