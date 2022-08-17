using DataTransit.Contract.Messaging.Base;
using DataTransit.Contract.ViewModel.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransit.Contract.Messaging.ExelModel
{
    public class ExcelModelResponse : BaseApiResponse<List<ExcelViewModel>> { }
    public class InsertBulkDataFromExcelResponse : BaseApiResponse<bool> { }

}
