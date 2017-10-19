


using Jbanimalitosv2;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Jbanimalitosv2
{


    public partial class Form1 : Form
    {


        public Form1()
        {
            InitializeComponent();
        }

        //
        // Declaraciones del API de Windows (y constantes usadas para mover el form)
        //
        const int WM_SYSCOMMAND = 0x112;
        const int MOUSE_MOVE = 0xF012;
        //
        // Declaraciones del API
        [System.Runtime.InteropServices.DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        //
        [System.Runtime.InteropServices.DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);


        private void Form1_Load(object sender, EventArgs e)
        {
            this.lstSorteos.SetItemChecked(0, true);

            animalitos db = new animalitos(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=animalitos;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True");
            var query = from qrysorteos in db.dbSorteos where qrysorteos.estatus == "A" select qrysorteos;
            foreach (var qrysorteos in query)
                //Console.WriteLine("id = {0}, City = {1}", cust.CustomerID,
                //  cust.City);
                this.lstSorteos.Items.Add(qrysorteos.sorteo);
        }

        private void Minz_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Cerrar_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close ();
        }

        private void Relog_Tick(object sender, EventArgs e)
        {
            this.Hora.Text = DateTime.Now.ToString("G");
        }

        private void moverForm()
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, MOUSE_MOVE, 0);
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            moverForm();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Ticket.Items.Count <= 0) {

                foreach (object itemChecked in lstSorteos.CheckedItems) // obtengo el valor seleccionado 
                {
                    this.Ticket.Items.Add(itemChecked.ToString());
                                    }

            }
           
            this.Ticket.Items.Add(this.Animal.Text + " - " + this.Nombre.Text + " - " + this.Monto.Text);

            Calcular();
        }

        public void Calcular()
        {
            int vrtotal;
            string[] Matriz = new string[Ticket.Items.Count];
            vrtotal = 0;
            for (int i = 1; i < Ticket.Items.Count; i++) { 
                Matriz[i] = Ticket.Items[i].ToString();
                string[] picados = Ticket.Items[i].ToString().Split('-');
                vrtotal = vrtotal + int.Parse(picados[2]);
            }
            this.Total.Text = vrtotal.ToString ();
        }

        private void Monto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {

                e.Handled = false;

            }
            else if (Char.IsControl(e.KeyChar))
            {           
                e.Handled = false;
            }
            //SI lo activas permite usar el espacio
            //else if (Char.IsSeparator(e.KeyChar))
            //{
             //   e.Handled = false;
            //}
            else
            {
                e.Handled = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (this.Ticket.SelectedIndex > -1  && this.Ticket.SelectedIndex != 0)
            {
                this.Ticket.Items.RemoveAt(this.Ticket.SelectedIndex);
            }
            Calcular();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Ticket.Items.Clear();
            Calcular();
        }
        

        private void Animales_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Animales.Text = "";
        }

        private void linkLabel8_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void lstSorteos_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (lstSorteos.CheckedItems.Count == 1)
            {
                Boolean isCheckedItemBeingUnchecked = (e.CurrentValue == CheckState.Checked);
                if (isCheckedItemBeingUnchecked)
                {
                    e.NewValue = CheckState.Checked;
                }
                else
                {
                    Int32 checkedItemIndex = lstSorteos.CheckedIndices[0];
                    lstSorteos.ItemCheck -= lstSorteos_ItemCheck;
                    lstSorteos.SetItemChecked(checkedItemIndex, false);
                    lstSorteos.ItemCheck += lstSorteos_ItemCheck;
                }

                // Indica que sorteo es en el ticket
                if (Ticket.Items.Count > 0)
                {
                    if (Ticket.Items[0].ToString() != "")
                    {
                        Ticket.Items.RemoveAt(0);
                        Ticket.Items.Insert(0, lstSorteos.SelectedItem);

                    }
                }
                return;
            }

        }
    }
}
