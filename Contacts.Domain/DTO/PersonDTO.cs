using Contacts.Domain.DbEntites;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Domain.DTO
{
    public class PersonDTO : AbstractPerson
    {
        [Required]
        public Guid ParentContactId { get; set; }

        public Guid SubContactId { get; set; }

        [Required]
        public string ParentContactName { get; set; }

        public string SubContactName { get; set; }
    }
}
