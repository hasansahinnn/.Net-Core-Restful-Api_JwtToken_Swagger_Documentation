using System;
using System.Collections.Generic;
using System.Text;

namespace Saglikcim.Entities.ServiceUtilities
{
    public class ResponseMessage
    {
        public Status status { get; set; }
        public object data { get; set; }
    }
}
