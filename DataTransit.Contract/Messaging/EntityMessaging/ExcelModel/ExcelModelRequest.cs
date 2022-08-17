using DataTransit.Contract.Messaging.Base;
using DataTransit.Contract.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransit.Contract.Messaging.ExelModel
{
    public class ExcelModelRequest : BaseApiRequest<string> { }
    public class InsertBulkDataFromExcelRequest : BaseApiRequest
    {
        public string Address { get; set; }
    }
}
