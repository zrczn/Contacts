using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Domain.DbEntites
{
    public class Contact
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(15)]
        public string? Name { get; set; }

        public List<Person>? Persons { get; set; }

        public List<Contact>? Contacts { get; set; }
        public Guid? ParentContactId { get; set; }
        public Contact? ContactRel { get; set; }
    }
}
