using API.Model;
using API.ViewModel;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Account
{
    [Key]

    public String NIK { get; set; }
    public string Password { get; set; }
    public string? otp { get; set; }  // ? artinya boleh null

    public bool? isUsed { get; set; } 
    public DateTime? Expired { get; set; }

    public Employee employees { get; set; }
    public Profilling profillings { get; set; }
}

