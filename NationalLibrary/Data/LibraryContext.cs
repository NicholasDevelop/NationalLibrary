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

    public DbSet<Person> People { get; set; }
    public DbSet<Document> Documents { get; set; }
    public DbSet<Residence> Residences { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Rent> Rents { get; set; }
    public DbSet<WaitingList> WaitingLists { get; set; }
    public DbSet<WaitingList_Book> WaitingList_Books { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Location> Locations { get; set; }

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
                    .IsRequired();

        // Relation Book 1-1 Rent(FK)
        //modelBuilder.Entity<Book>()
        //            .HasOne(b => b.Rent)
        //            .WithOne(r => r.Book)
        //            .HasForeignKey<Book>(r => r.BookGuid);

        // Relation Location 1-1 Book(FK)
        modelBuilder.Entity<Location>()
                    .HasOne(l => l.Book)
                    .WithOne(b => b.Location)
                    .HasForeignKey<Book>(b => b.LocationGuidFK);

        // Relation WaitingList N-N Book
        // (WaitingList 1-N WaitingList_Book N-1 Book)

        modelBuilder.Entity<WaitingList_Book>()
            .HasKey(wb => new { wb.WaitingGuid, wb.BookGuid });

        modelBuilder.Entity<WaitingList_Book>()
            .HasOne(wb => wb.WaitingList)
            .WithMany(wl => wl.WaitingList_Books)
            .HasForeignKey(wb => wb.WaitingGuid);

        modelBuilder.Entity<WaitingList_Book>()
            .HasOne(wb => wb.Book)
            .WithMany(b => b.WaitingList_Books)
            .HasForeignKey(wb => wb.BookGuid);

    }
}