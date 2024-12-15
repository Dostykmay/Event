using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Policy;
using Microsoft.EntityFrameworkCore;

namespace GiftNotation.Models;

[Index(nameof(RelpTypeName), IsUnique = true)]
public class RelpType
{
    public int RelpTypeId { get; set; }
    public string RelpTypeName { get; set; }
    public ICollection<Contact> Contacts { get; set; }
}

