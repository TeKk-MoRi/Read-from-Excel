using AutoMapper;
using DataTransit.Common.Helper;
using DataTransit.Common.Redis;
using DataTransit.Contract.Commands.Excel;
using DataTransit.Contract.Messaging.ExelModel;
using DataTransit.Contract.ViewModel.Excel;
using DataTransit.Domain.Models;
using DataTransit.Service.Services.Exel;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransit.Contract.Handlers._ٍExcel
{
    public class InsertBulkDataFromExcelHandler : IRequestHandler<InserBulkDataFromExcelCommand, InsertBulkDataFromExcelResponse>
    {
        private readonly IExcelService _exelService;
        private readonly IMapper _mapper;
        public InsertBulkDataFromExcelHandler(IExcelService exelService, IMapper mapper)
        {
            this._exelService = exelService;
            this._mapper = mapper;
        }
        public async Task<InsertBulkDataFromExcelResponse> Handle(InserBulkDataFromExcelCommand request, CancellationToken cancellationToken)
        {
            InsertBulkDataFromExcelResponse response = new();
            try
            {


                //Using Entity FrameWork core bulk insert => about 6 seconds
                // bool inserted =  await _exelService.InsertbulkDataFromExcel();

                //Using Dapper bulk insert (Much faster) => about 3 seconds
                bool inserted =  await _exelService.InsertbulkDataFromExcelByDapper();


                //if user wants to import data directly from excel not the loaded data
                if(!inserted)
                {
                    // file should be csv format(the only format of excel that sql can read) => source : microsoft document
                    if (!string.IsNullOrEmpty(request.Request.Address))
                    {
                        await _exelService.InsertbulkRawDataFromExcelByDapper(FindFile.CorrectFilePath(request.Request.Address));
                    }

                }


                response.Result = true;
                response.Succeed();
                response.SuccessMessage("500,000 rows affected");
                response.SuccessMessage();

                return response;
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Failed();
                response.FailedMessage("Please check your connection strings");
                response.FailedMessage();

                return response;
            }
        }
    }
}
