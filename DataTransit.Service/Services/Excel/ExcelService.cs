using Dapper.Helper;
using DataTransit.Common.Exel;
using DataTransit.Common.Redis;
using DataTransit.Datalayer.Context;
using DataTransit.Domain.Models;
using DataTransit.Service.Base;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using Entity = DataTransit.Domain;


namespace DataTransit.Service.Services.Exel
{
    public class ExcelService : BaseService<Entity.Models.ExcelModel>, IExcelService
    {
        private readonly IDapperHelper _dapperHelper;
        private readonly IDistributedCache _cache;
        public ExcelService(DataTransitContext context, IDapperHelper dapperHelper
            , IDistributedCache cache) : base(context)
        {
            this._dapperHelper = dapperHelper;
            this._cache = cache;
        }

        public async Task<T> GetDataFromExel<T>(string address)
        {
            //Redis
            string recordKey = "Data" + "_" + "Excel";
            var record = await _cache.GetRecordAsync<T>(recordKey);
            if (record == null)
            {
                // return GetExelDataExtension.GetData(address).ToList();
                var res = GetExelDataExtension.ReadFromExcel<T>(address);

                if (res != null)
                {
                    // lifetime 120 sec on Redis
                    await _cache.SetRecordAsync(recordKey, res, TimeSpan.FromHours(2));
                    record = res;
                }
            }
            return record;
        }

        public async Task<bool> InsertbulkDataFromExcel()
        {
            //record key for getting value from Redis
            string recordKey = "Data" + "_" + "Excel";

            var record = await _cache.GetRecordAsync<List<ExcelModel>>(recordKey);
            if (record != null)
            {
                await Entities.BulkInsertAsync(record);
                return true;
            }
             return false;
        }

        public async Task<bool> InsertbulkDataFromExcelByDapper()
        {
            //record key for getting value from Redis
            string recordKey = "Data" + "_" + "Excel";

            var record = await _cache.GetRecordAsync<List<ExcelModel>>(recordKey);
            if (record != null)
            {
                _dapperHelper.BulkInser(record);
                return true;
            }
            return false;
        }
        public async Task InsertbulkRawDataFromExcelByDapper(string address)
        {
            var query = (await _dapperHelper.QueryAsync<ExcelModel>("BULK INSERT ExelModels FROM " + "'" + address + "'" + " WITH (FIELDTERMINATOR = ',',ROWTERMINATOR = '\n');"));
        }
    }
}
