using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Jbanimalitosv2
{
    class Utilidades
    {
        public string CONEC =  System.Configuration.ConfigurationManager.ConnectionStrings["CNX"].ConnectionString;
    }
}
