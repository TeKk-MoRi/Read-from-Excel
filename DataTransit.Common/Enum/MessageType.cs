using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransit.Common.Enum
{
    public enum MessageType : byte
    {
        Error = 0,

        Success = 1,

        Information = 2,

        Warning = 3,
    }
}
