using DataTransit.Domain.Models;
using DataTransit.Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity = DataTransit.Domain;


namespace DataTransit.Service.Services.Exel
{
    public interface IExcelService : IBaseService<Entity.Models.ExcelModel>
    {
        Task<T> GetDataFromExel<T>(string address);
        Task<bool> InsertbulkDataFromExcel();
        Task<bool> InsertbulkDataFromExcelByDapper();
        Task InsertbulkRawDataFromExcelByDapper(string address);
    }
}
