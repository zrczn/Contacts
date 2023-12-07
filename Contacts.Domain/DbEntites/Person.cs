using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Domain.DbEntites
{
    public class Person
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(30, ErrorMessage = $"{nameof(FirstName)} max length is 30")]
        public required string FirstName { get; set; }

        [Required]
        [MaxLength(30, ErrorMessage =$"{nameof(LastName)} max length is 30")]
        public required string LastName { get; set; }

        [Required]
        //https://regexlib.com
        [RegularExpression("^((([!#$%&'*+\\-/=?^_`{|}~\\w])|([!#$%&'*+\\-/=?^_`{|}~\\w][!#$%&'*+\\-/=?^_`{|}~\\.\\w]{0,}" +
            "[!#$%&'*+\\-/=?^_`{|}~\\w]))[@]\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*)$", ErrorMessage = $"{nameof(Email)} Email is invalid, try another")] 
        public required string Email { get; set; }

        [Required]
        //https://regexlib.com
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$",
            ErrorMessage = $"{nameof(Password)} password should contain minimun eight characters, Including at least:\none letter\none number\n one special character")]
        public required string Password { get; set; }

        [Required]
        [RegularExpression("^[0-9]{9}$", ErrorMessage = $"{nameof(PhoneNumber)} at least 9 characters required")]
        public int PhoneNumber { get; set; }

        public DateTime DateOfBirth { get; set; }


        public Guid ContactId { get; set; }
        public Contact Contact { get; set; }
    }
}
