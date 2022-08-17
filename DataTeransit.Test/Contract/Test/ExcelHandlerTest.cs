using AutoMapper;
using DataTransit.Common.Redis;
using DataTransit.Contract.Handlers;
using DataTransit.Contract.Messaging.ExelModel;
using DataTransit.Contract.Queries.Excel;
using DataTransit.Contract.ViewModel.Excel;
using DataTransit.Domain.Models;
using DataTransit.Service.Services.Exel;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static DataTeransit.Test.Contract.Model.ExcelModelTest;

namespace DataTeransit.Test.Contract.Test
{
    public class ExcelHandlerTest
    {
        private Mock<IExcelService> _excelService;
        private IMapper _mapper;
        public ExcelHandlerTest()
        {
            _excelService = new Mock<IExcelService>();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.CreateMap<ExcelViewModel, ExcelModel>().ReverseMap();
            });

            IMapper mapper = mappingConfig.CreateMapper();
            _mapper = mapper;

        }


        [Theory]
        [ClassData(typeof(ExcelModelRequestTest))]
        //naming convention MethodName_expectedBehavior_StateUnderTest
        public async Task GetExcelDataFromExelhandler_ReadFromExcel_ShouldReturnListOfExcelModel(GetExcelDataFromFromExelQuery req)
        {

            _excelService.Setup(x => x.GetDataFromExel<List<ExcelModel>>(It.IsAny<string>()))
                .ReturnsAsync(GetSampleData);

            var contract = new GetExcelDataFromExelhandler(_excelService.Object, _mapper);
            var res = await contract.Handle(req, CancellationToken.None);

            Assert.NotNull(res);
            Assert.True(res.IsSucceed);
            Assert.Equal(2, res.Result.Count);

        }
        private List<ExcelModel> GetSampleData()
        {
            List<ExcelModel> output = new List<ExcelModel>();
            output.Add(new ExcelModel
            {
                Id = 1,
                Country = "Iran"
            });

            output.Add(new ExcelModel
            {
                Id = 2,
                Country = "Canada"
            });

            return output;
        }

        private List<ExcelViewModel> GetSampleViewModel()
        {
            List<ExcelViewModel> output = new List<ExcelViewModel>();
            output.Add(new ExcelViewModel
            {
                Country = "Iran"
            });

            output.Add(new ExcelViewModel
            {
                Country = "Canada"
            });

            return output;
        }
    }
}
