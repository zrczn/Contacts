using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Domain
{
    public class ValidateDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            DateTime passedDate;
            
            if(value is DateTime date)
            {
                passedDate = date;
            }
            else
            {
                try
                {
                    passedDate = Convert.ToDateTime(value);
                }
                catch
                {
                    return false;
                }
            }

            DateTime currentDate = DateTime.Now;

            if (currentDate < passedDate)
                return false;

            return true;
        }
    }
}
