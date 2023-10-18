using Amazon.IdentityManagement.Model;
using API.Model;
using Microsoft.EntityFrameworkCore;
using System;

public class MyContext : DbContext
{
	public MyContext(DbContextOptions<MyContext>  options) : base(options)
	{

	}

	public DbSet<Employee> Employees { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Education> Educations { get; set; }
    public DbSet<Profilling> profillings { get; set; }
    public DbSet<University> universities { get; set; }

    //protected override void OnModelCreating(ModelBuilder builder) => builder.Entity<Employee>()
    //        .HasIndex(u => new { u.Phone, u.Email })
    //        .IsUnique();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Account>()
            .HasOne(a => a.employees)
            .WithOne(b => b.Accounts)
            .HasForeignKey<Account>(b => b.NIK);

        modelBuilder.Entity<Profilling>()
            .HasOne(c => c.Accounts)
            .WithOne(d => d.profillings)
            .HasForeignKey<Profilling>(d => d.NIK);

        modelBuilder.Entity<Profilling>()
            .HasOne(e => e.Educations)
            .WithMany(f => f.profillings)
            .HasForeignKey(e => e.Education_id);

        modelBuilder.Entity<Education>()
            .HasOne(g => g.universities)
            .WithMany(i => i.Educations)
            .HasForeignKey(g => g.University_id);
    }

}
