using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jbanimalitosv2
{
    public partial class animalitos : DataContext
    {
        public Table<Sorteos__> dbSorteos;
        public Table<Horarios> dbhorarios;
        public animalitos(string connection) : base(connection) { }
    }
}
