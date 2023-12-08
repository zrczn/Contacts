using Contacts.Domain.DbEntites;
using Contacts.Domain.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Persistence
{
    public class ContactsDatabaseContext : DbContext
    {
        public ContactsDatabaseContext(DbContextOptions<ContactsDatabaseContext> opt) : base(opt)
        {
            
        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Contact> Contacts { get; set; }

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>()
               .HasOne(x => x.ContactRel)
               .WithMany(y => y.Contacts)
               .HasForeignKey(z => z.ParentContactId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasOne(x => x.Role)
                .WithMany(y => y.Users)
                .HasForeignKey(z => z.RoleId);
        }
    }
}
