using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APICatalog.Services.Impl
{
    public class FromService : IFromService
    {
        public string Default(string msg)
        {
            return $"Welcome {msg} \n\n{DateTime.Now}";
        }
    }
}
