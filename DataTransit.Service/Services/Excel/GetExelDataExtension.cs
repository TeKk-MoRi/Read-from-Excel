using DataTransit.Domain.Models;
using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DataTransit.Common.Helper;
using LicenseContext = OfficeOpenXml.LicenseContext;
using System.IO;

namespace DataTransit.Common.Exel
{
    public static class GetExelDataExtension
    {
        //this is a faster way
        public static IEnumerable<ExcelModel> GetData(string path)
        {
            try
            {
                var file = FindFile.GetFile(path, "*.xlsx");
                var dataList = new List<ExcelModel>();

                var fileName = file.FullName;
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using var package = new ExcelPackage(file);
                var currentSheet = package.Workbook.Worksheets;
                var workSheet = currentSheet.First();
                var noOfCol = workSheet.Dimension.End.Column;
                var noOfRow = workSheet.Dimension.End.Row;
                for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                {
                    var exelViewModel = new ExcelModel
                    {
                        Country = workSheet.Cells[rowIterator, 1].Value?.ToString(),
                    };
                    dataList.Add(exelViewModel);
                }

                return dataList;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        //this is a way which client decide what the implementation should be like
        public static T ReadFromExcel<T>(string path, bool hasHeader = true)
        {
            try
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                var file = FindFile.GetFile(path, "*.xlsx");
                using (var excelPack = new ExcelPackage(file))
                {

                    //Lets Deal with first worksheet.(You may iterate here if dealing with multiple sheets)
                    var ws = excelPack.Workbook.Worksheets[0];

                    //Get all details as DataTable -because Datatable make life easy :)
                    DataTable excelasTable = new DataTable();
                    foreach (var firstRowCell in ws.Cells[1, 1, 1, ws.Dimension.End.Column])
                    {
                        //Get colummn details
                        if (!string.IsNullOrEmpty(firstRowCell.Text))
                        {
                            string firstColumn = string.Format("Column {0}", firstRowCell.Start.Column);
                            excelasTable.Columns.Add(hasHeader ? firstRowCell.Text : firstColumn);
                        }
                    }
                    var startRow = hasHeader ? 2 : 1;
                    //Get row details
                    for (int rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
                    {
                        var wsRow = ws.Cells[rowNum, 1, rowNum, excelasTable.Columns.Count];
                        DataRow row = excelasTable.Rows.Add();
                        foreach (var cell in wsRow)
                        {
                            row[cell.Start.Column - 1] = cell.Text;
                        }
                    }
                    //Get everything as generics and let end user decides on casting to required type
                    var generatedType = JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(excelasTable));
                    return (T)Convert.ChangeType(generatedType, typeof(T));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }

        }
    }
}

