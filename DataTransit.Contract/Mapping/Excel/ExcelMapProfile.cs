using AutoMapper;
using DataTransit.Contract.ViewModel.Excel;
using DataTransit.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransit.Contract.Mapping.Exel
{
    public class ExcelMapProfile : Profile
    {
        public ExcelMapProfile()
        {
            CreateMap<ExcelViewModel, ExcelModel>().ReverseMap();
        }
    }
}
