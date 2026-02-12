using ContactManager.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactManager.Context;

public class ContactDbContext : DbContext
{
    public DbSet<Contact> Contacts { get; set; }
    
    public ContactDbContext(DbContextOptions<ContactDbContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }
}