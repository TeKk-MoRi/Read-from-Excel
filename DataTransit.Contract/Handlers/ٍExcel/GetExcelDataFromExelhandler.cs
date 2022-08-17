using AutoMapper;
using DataTransit.Common.Redis;
using DataTransit.Contract.Messaging.Base;
using DataTransit.Contract.Messaging.ExelModel;
using DataTransit.Contract.Queries.Excel;
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

namespace DataTransit.Contract.Handlers
{
    public class GetExcelDataFromExelhandler : IRequestHandler<GetExcelDataFromFromExelQuery, ExcelModelResponse>
    {
        private readonly IExcelService _exelService;
        private readonly IMapper _mapper;

        public GetExcelDataFromExelhandler(IExcelService exelService, IMapper mapper)
        {
            this._exelService = exelService;
            this._mapper = mapper;
        }

        public async Task<ExcelModelResponse> Handle(GetExcelDataFromFromExelQuery request, CancellationToken cancellationToken)
        {
            ExcelModelResponse response = new();
            try
            {
                var res = _mapper.Map<List<ExcelViewModel>>(await _exelService.GetDataFromExel<List<ExcelModel>>(request.Request.ViewModel));

                if (res is null)
                    response.SuccessMessage("there is no record");
                response.SuccessMessage();
                response.Succeed();
                response.Result = res;
                return response;
            }
            catch (Exception ex)
            {
                response.Failed();
                response.FailedMessage("Please check your connection strings");
                response.FailedMessage();

                return response;

            }
        }
    }
}
