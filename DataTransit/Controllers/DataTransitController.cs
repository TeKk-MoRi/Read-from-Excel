using DataTransit.Contract.Commands.Excel;
using DataTransit.Contract.Queries.Excel;
using DataTransit.Contract.ViewModel.Excel;
using DataTransit.Extension;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DataTransit.Controllers
{
    public class DataTransitController : BaseController
    {
        private readonly IMediator _mediator;
        public DataTransitController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetExelData(string address)
        {
            var res = await _mediator.Send(new GetExcelDataFromFromExelQuery(new Contract.Messaging.ExelModel.ExcelModelRequest { ViewModel = address }));
            return Response(res);
        }

        [HttpPost]
        public async Task<IActionResult> InsertData(InsertDataViewModel model)
        {
            var res = await _mediator.Send(new InserBulkDataFromExcelCommand(new Contract.Messaging.ExelModel.InsertBulkDataFromExcelRequest { Address = model.Address}));
            return Response(res);
        }
    }
}
