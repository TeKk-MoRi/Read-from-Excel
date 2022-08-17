using DataTransit.Contract.Messaging.Base;
using DataTransit.Contract.Messaging.ExelModel;
using DataTransit.Contract.ViewModel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransit.Contract.Queries.Excel
{
    public record GetExcelDataFromFromExelQuery(ExcelModelRequest Request) : IRequest<ExcelModelResponse>;
}
