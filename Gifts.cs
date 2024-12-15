using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GiftNotation.Models;

public class Gifts
{
    public int GiftId { get; set; }
    public string? GiftName { get; set; }
    public string? Description { get; set; }
    public string? Url { get; set; }
    public double Price { get; set; }
    public string? GiftPic { get; set; }
    public int? StatusId { get; set; }
    public Status? Status { get; set; }

    public ICollection<GiftContact> GiftContacts { get; set; }
    public ICollection<GiftEvent> GiftEvents { get; set; }
}

