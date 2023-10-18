
using Amazon.CloudSearch_2011_02_01.Model;
using Microsoft.Azure.Amqp.Framing;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Index(nameof(Phone), nameof(Email), IsUnique = true)]
public class Employee
{
    [Key]
    public string NIK { get; set; }
    public string FisrtName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
    public DateTime BirthDate { get; set; }
    public int salary { get; set; }
    public string Email { get; set; }
    public Gender Gender { get; set; }

    public Account Accounts { get; set; }

}
public enum Gender
{
	Male,
    Female
}



