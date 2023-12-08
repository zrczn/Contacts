using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Persistence.Repositories
{
    public abstract class AbstractRepository
    {
        internal readonly ContactsDatabaseContext _DbCon;
        public AbstractRepository(ContactsDatabaseContext _DbCon)
            => this._DbCon = _DbCon;
    }
}
