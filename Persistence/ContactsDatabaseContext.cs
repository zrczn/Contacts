using Contacts.Domain.DbEntites;
using Contacts.Domain.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contacts.Persistence.Services;
using Contacts.Persistence.Contracts;

namespace Contacts.Persistence
{
    public class ContactsDatabaseContext : DbContext
    {

        //context bazy danych
        //podejście code first

        public ContactsDatabaseContext(DbContextOptions<ContactsDatabaseContext> opt) : base(opt)
        {

        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Contact> Contacts { get; set; }

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //swtórz tabelę z kluczem PK i FK jednocześnie
            //FK działa jako pointer na kontakt nadrzędny (sluzbowy)
            //PK referencja do kontaktu podrzędnego (szef)
            //zablokuj w celu przeciwdziałania usuwania kaskadowego

            modelBuilder.Entity<Contact>()
               .HasOne(x => x.ContactRel)
               .WithMany(y => y.Contacts)
               .HasForeignKey(z => z.ParentContactId)
               .OnDelete(DeleteBehavior.Restrict);

            //konfiguracja opcjonalna
            //one-to-many nałożony poprzez konwencję w klasie domenowej

            modelBuilder.Entity<User>()
                .HasOne(x => x.Role)
                .WithMany(y => y.Users)
                .HasForeignKey(z => z.RoleId);

            //mock data 

            Role role = new Role()
            {
                Id = Guid.NewGuid(),
                RoleType = "User",

            };

            User usr1 = new User()
            {
                Id = Guid.NewGuid(),
                Login = "TestUser",
                Password = "0db32429d0398d23872b01db915daf6d6e75015eab57f2a43b2952a70efe79da", //supertajnehaslo
                RoleId = role.Id
            };

            //Contact cntct1 = new Contact()
            //{
            //    Id = Guid.NewGuid(),
            //    Name = "sluzbowy",
            //    ParentContactId = null,
            //};

            Contact cntct2 = new Contact()
            {
                Id = Guid.NewGuid(),
                Name = "prywatny",
                ParentContactId = null,
            };

            Contact cntct3 = new Contact()
            {
                Id = Guid.NewGuid(),
                Name = "inny",
                ParentContactId = null,
            };

            Contact cntct4 = new Contact()
            {
                Id = Guid.NewGuid(),
                Name = "sluzbowy",
                ParentContactId = null
            };

            Contact cntct5 = new Contact()
            {
                Id = Guid.NewGuid(),
                Name = "szef",
                ParentContactId = cntct4.Id,
            };

            Contact cntct6 = new Contact()
            {
                Id = Guid.NewGuid(),
                Name = "klient",
                ParentContactId = cntct4.Id,
            };

            Person prsn = new Person()
            {
                Id = Guid.NewGuid(),
                FirstName = "Test1",
                LastName = "LastName1",
                Email = "Test1@gmail.com",
                Password = "haslo1",
                PhoneNumber = 000000000,
                DateOfBirth = new DateTime(1990, 10, 10),
                ContactId = cntct5.Id
            };

            Person prsn2 = new Person()
            {
                Id = Guid.NewGuid(),
                FirstName = "Test2",
                LastName = "LastName2",
                Email = "Test2@gmail.com",
                Password = "haslo2",
                PhoneNumber = 111111111,
                DateOfBirth = new DateTime(1995, 10, 10),
                ContactId = cntct5.Id
            };

            modelBuilder.Entity<User>()
                .HasData(usr1);
            modelBuilder.Entity<Contact>()
                .HasData(cntct2, cntct3, cntct4, cntct5, cntct6);
            modelBuilder.Entity<Person>()
                .HasData(prsn, prsn2);
            modelBuilder.Entity<Role>()
                .HasData(role);

        }
    }
}
