using Contacts.Domain.Repositories;
using Contacts.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Persistence
{
    public static class InfrastructureInstaller
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection descriptors)
        {
            descriptors.AddScoped<IContactRepository, ContactRepository>();
            return descriptors;
        }
    }
}
