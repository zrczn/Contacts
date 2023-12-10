using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Domain.DbEntites
{
    public class Role
    {
        //role dla użytkownika
        [Key]
        public Guid Id { get; set; }
        [Required]
        public required string RoleType { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
