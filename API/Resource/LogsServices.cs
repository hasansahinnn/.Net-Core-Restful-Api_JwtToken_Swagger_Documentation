using Data;
using Data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saglikcim.Entities.ServiceUtilities
{
    static public class LogsServices
    {
        static DataContext dbcontext = new DataContext();
        public static void Log(string Page, string errormessage, int type)
        {
            dbcontext.Logs.Add(new Logs() { IDate = DateTime.Now, Page = Page, ErrorMessage = errormessage, LogType = type });
            dbcontext.SaveChanges();
        }
    }
}
