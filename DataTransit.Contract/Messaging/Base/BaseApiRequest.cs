using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransit.Contract.Messaging.Base
{
    public abstract class BaseApiRequest
    {
    }

    public abstract class BaseApiRequest<T> : BaseApiRequest
    {
        public T ViewModel { get; set; }
    }


}
