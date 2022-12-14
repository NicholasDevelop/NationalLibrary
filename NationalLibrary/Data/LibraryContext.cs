using Microsoft.EntityFrameworkCore;
using NationalLibrary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

public class LibraryContext : DbContext
{
    public LibraryContext(DbContextOptions<LibraryContext> options) : base(options)
    { }
    public LibraryContext() : base()
    { }
    public DbSet<Person> People { get; set; }
    public DbSet<Document> Documents { get; set; }
    public DbSet<Residence> Residences { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Rent> Rents { get; set; }
    public DbSet<WaitingList> WaitingLists { get; set; }
    public DbSet<ISBNList> ISBNLists { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<Request> Requests { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Relation Document 1-1 Person(FK)
        modelBuilder.Entity<Document>()
                    .HasOne(d => d.Person)
                    .WithOne(p => p.Document)
                    .HasForeignKey<Person>(d => d.DocumentNumberFK)
                    .IsRequired();

        // Relation Person 1-1 User(FK)
        modelBuilder.Entity<Person>()
                    .HasOne(p => p.User)
                    .WithOne(u => u.Person)
                    .HasForeignKey<User>(u => u.FiscalCode)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.ClientCascade);

        // Relation Location 1-1 Book(FK)
        modelBuilder.Entity<Location>()
                    .HasOne(l => l.Book)
                    .WithOne(b => b.Location)
                    .HasForeignKey<Book>(b => b.LocationGuidFK);
    }
}