using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Domain.DbEntites
{
    public class Person : AbstractPerson
    {
        public Guid ContactId { get; set; }
        public Contact Contact { get; set; }
    }
}
