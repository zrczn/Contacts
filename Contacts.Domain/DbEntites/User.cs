using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Domain.DbEntites
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public required string Login { get; set; }
        [Required]
        public required string Password { get; set; }

        public Guid RoleId { get; set; }
        public Role Role { get; set; }
    }
}
