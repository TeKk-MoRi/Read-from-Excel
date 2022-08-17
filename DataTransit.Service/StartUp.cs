using Dapper.Helper;
using DataTransit.Service.Base;
using DataTransit.Service.Services.Exel;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransit.Service
{
    public static class StartUp
    {
        public static void Start(IServiceCollection services)
        {
            services.AddScoped(typeof(IBaseService<>), typeof(BaseService<>));
            services.AddScoped<IExcelService, ExcelService>();
            services.AddSingleton<IDapperHelper, DapperHelper>();
        }
    }
}
