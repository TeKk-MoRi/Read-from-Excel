using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransit.Contract
{
    public static class StartUp
    {
        public static void Start(IServiceCollection services)
        {
            Service.StartUp.Start(services);

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }
    }
}
