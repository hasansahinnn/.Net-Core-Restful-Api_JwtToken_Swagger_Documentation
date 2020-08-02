using System;
using System.Collections.Generic;
using System.Text;

namespace Saglikcim.Entities.ServiceUtilities
{
    public class ResponseHandler
    {
        public static ResponseMessage ResponseMessageHandler(int statusCode, string message, object data = null)
        {
            Status status = new Status()
            {
                code = statusCode
            };

            if (!string.IsNullOrEmpty(message))
            {
                status.value = message;
            }

            if (data == null)
            {
                data = new object();
            }

            ResponseMessage responseMessage = new ResponseMessage()
            {
                status = status,
                data = data
            };

            return responseMessage;
        }
    }
}
