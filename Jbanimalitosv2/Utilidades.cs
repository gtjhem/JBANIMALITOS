using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Windows.Forms;

namespace Jbanimalitosv2
{
    class Utilidades
    {
        
        //Utilidades CN = new Utilidades();
        public string CONEC =  System.Configuration.ConfigurationManager.ConnectionStrings["CNX"].ConnectionString;

        
        private   CheckedListBox  CLB = new CheckedListBox();

        public void sr_llenar_sorteos  (ref CheckedListBox CLB, int vr_key_codigo)
        {

            animalitos db = new animalitos(CONEC);

            var query = (from qrysorteos in db.dbSorteos
                         join qryhorario in db.dbhorarios on qrysorteos.ID_SORTEO equals qryhorario.IDSORTEOHR
                         where qrysorteos.ESTATUS == "A" && qrysorteos.ID_SORTEO == vr_key_codigo
                         select new { qryhorario.HORA, qrysorteos.NOMBRE_SORTEO, qrysorteos.ID_SORTEO }).ToList();


            CLB.Items.Clear();
            foreach (var aqui in query)

                if (aqui.HORA >= DateTime.Now.TimeOfDay)
                {

                    CLB.Items.Add(aqui.HORA + " - " + aqui.NOMBRE_SORTEO.ToString().ToUpper() + " - " + aqui.ID_SORTEO);
                }        
                                    
        }

        private ComboBox cmb = new ComboBox();
        public void sr_llenar_loteria (ref ComboBox CMB)
        {

            animalitos db = new animalitos(CONEC);

            var query = (from qrysorteos in db.dbSorteos
                         where qrysorteos.ESTATUS == "A"
                         select new { qrysorteos.NOMBRE_SORTEO, qrysorteos.ID_SORTEO }).ToList();


            CMB.Items.Clear();

            foreach (var aqui in query)

                CMB.Items.Add(aqui.NOMBRE_SORTEO.ToString().ToUpper() + " - " + aqui.ID_SORTEO);
        }
        
        public void sr_llenar_animales(ref CheckedListBox CLB, int vr_key_loteria)
        {
            animalitos db = new animalitos(CONEC);


            var query = (from qryanimalito in db.dbanimalitos
                         where qryanimalito.IDSORTEOAN == vr_key_loteria

                         select new { qryanimalito.CODIGO, qryanimalito.NOMBRE_ANIMALITO }).ToList();


            CLB.Items.Clear();

            foreach (var aqui in query)
                CLB.Items.Add(aqui.CODIGO.ToString() + " - " + aqui.NOMBRE_ANIMALITO.ToString());
        }


        



    }
}
