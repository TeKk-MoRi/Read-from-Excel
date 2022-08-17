using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataTransit.Common.Helper
{
    public static class ConfigurationManager
    {
        public static IConfiguration Configuration { private get; set; }

        public static IConfiguration GetConfiguration()
        {
            return Configuration;
        }

        public static string GetConnectionString(string index)
        {
            var res = Configuration.GetConnectionString(index);
            return res;
        }


    }
}
