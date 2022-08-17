using DataTransit.Contract.Messaging.ExelModel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransit.Contract.Commands.Excel
{

    public record InserBulkDataFromExcelCommand(InsertBulkDataFromExcelRequest Request) : IRequest<InsertBulkDataFromExcelResponse>;
}
