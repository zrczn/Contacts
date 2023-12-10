using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Persistence.Repositories
{
    public abstract class AbstractRepository
    {
        //abstrakcyjne repozytorium
        //każde z repo potrzebuje połączenia z DB

        internal readonly ContactsDatabaseContext _DbCon;
        public AbstractRepository(ContactsDatabaseContext _DbCon)
            => this._DbCon = _DbCon;
    }
}
