using DataTransit.Contract.Messaging.ExelModel;
using DataTransit.Contract.Queries.Excel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTeransit.Test.Contract.Model
{
    public static class ExcelModelTest
    {
        public class ExcelModelRequestTest : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] {
                    new GetExcelDataFromFromExelQuery(new ExcelModelRequest { ViewModel = "test"})
                    {
                        Request = new ExcelModelRequest { ViewModel = "test"},
                    }
                };
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                throw new NotImplementedException();
            }
        }
    }
}
