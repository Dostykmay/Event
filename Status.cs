using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Policy;
using Microsoft.EntityFrameworkCore;

namespace GiftNotation.Models;

[Index(nameof(StatusName), IsUnique = true)]
public class Status
{
    public int StatusId { get; set; }
    public string StatusName { get; set; } = string.Empty;
    public ICollection<Gifts> Gifts { get; set; }
}

