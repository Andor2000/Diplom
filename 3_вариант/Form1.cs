using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration; 
using System.Data.SqlClient;

namespace _3_вариант
{
    public partial class Form1 : Form
    {
        List<Papka> list_textbox = new();
        List<Papka> list_textbox_left = new();
        List<Papka> list_textbox_right = new();

        int nomer_papki=1;

        bool peretaskivanie = false;

        int x_mouse, y_mouse;
        int y_tb;
        public Form1()
        {
            InitializeComponent();
           // textBox1.Visible = false;
            listBox1.Visible = false;
            listView1.Visible = false;
            button2.Visible = false;
            button3.Visible = false;
            button4.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e) // добавление в левую колонку папок
        {
            Papka papka = new Papka();
            nomer_papki = papka.sozdanie_papki(panel2, list_textbox_left, nomer_papki);
            
            papka.kolonka = 1;

            int kolvo_lev_kolon = 0;
            foreach (Papka pap in list_textbox_left)
            {
                if(pap.kolonka==1)
                {
                    kolvo_lev_kolon++;
                }
            }
            papka.mesto_v_spiske = kolvo_lev_kolon + 1;

            panel2.Controls.Add(papka.textBox);
            list_textbox.Add(papka);
            list_textbox_left.Add(papka);

            papka.textBox.MouseDown += kliknyl_po_textBox;
            papka.textBox.MouseUp += otpustil_textBox;
            papka.textBox.MouseMove +=derjish_textBox ;
            //papka.textBox.MouseDoubleClick += perehod_po_papkam;
        }

        private void kliknyl_po_textBox(object sender, MouseEventArgs e) // кликнул и не отпустил 
        {
            peretaskivanie = true;
            x_mouse = Cursor.Position.X;
            y_mouse = Cursor.Position.Y;
            TextBox textb = (TextBox)sender;
            y_tb = textb.Location.Y;
        }

        private void perestavlenie_v_kolonke(Papka papka)
        {
            List<Papka> list = papka.kolonka==1 ? list_textbox_left: list_textbox_right;


            foreach (Papka pap in list) // проверяем все папки колонке
            {
                if (pap.textBox != papka.textBox && papka.textBox.Location.Y >= pap.textBox.Location.Y - 7 && papka.textBox.Location.Y <= pap.textBox.Location.Y + 7)
                {

                    int help = pap.mesto_v_spiske;
                    pap.mesto_v_spiske = papka.mesto_v_spiske;
                    papka.mesto_v_spiske = help;
                    pap.textBox.Location = new Point(pap.textBox.Location.X, (pap.mesto_v_spiske - 1) * pap.textBox.Size.Height);
                    break;
                }
            }
        }
        private void peretaskivanie_mejdy_kolonkami(Papka papka)
        {
            List<Papka> list;
            List<Papka> new_list;
            int x_koord1, x_koord2;
            Panel pn;
            if(papka.kolonka == 1)
            {
                x_koord1 = Cursor.Position.X;
                x_koord2 = x_mouse;
                list = list_textbox_left;
                new_list = list_textbox_right;
                pn = panel3;
            }
            else
            {
                x_koord1 = x_mouse;
                x_koord2 = Cursor.Position.X;
                list = list_textbox_right;
                new_list = list_textbox_left;
                pn = panel2;
            }
            if (x_koord1 - x_koord2 > papka.textBox.Size.Width / 2
                && list.Contains(papka))
            {
                foreach (Papka pap in list)
                {
                    if (pap.mesto_v_spiske > papka.mesto_v_spiske)
                    {
                        pap.textBox.Location = new Point(pap.textBox.Location.X, pap.textBox.Location.Y - pap.textBox.Size.Height);
                        pap.mesto_v_spiske--;
                    }
                }
                pn.Controls.Add(papka.textBox);
                list.Remove(papka);
                new_list.Add(papka);
                papka.kolonka = papka.kolonka == 1 ? 2 : 1;
                papka.mesto_v_spiske = new_list.Count;
                papka.textBox.Location = new Point(papka.Location.X, papka.mesto_v_spiske * papka.textBox.Size.Width);
            }

        }
        private void derjish_textBox(object sender, MouseEventArgs e) // пока мышь находится на элементе
        {
            if (peretaskivanie)
            {
                Papka papka = new Papka();

                foreach (Papka pap in list_textbox)
                {
                    if (pap.textBox == (TextBox)sender)
                    {
                        papka = pap;
                        break;
                    }
                }
                int y = Cursor.Position.Y - y_mouse;
                papka.textBox.Location = new Point(papka.textBox.Location.X, y_tb + y);

                perestavlenie_v_kolonke(papka);    
                peretaskivanie_mejdy_kolonkami(papka);           

            }
        }

        //private void perehod_po_papkam(object sender, MouseEventArgs e)
        //{
        //    label1.Text = ((TextBox)sender).Text;
        //}

        private void otpustil_textBox(object sender, MouseEventArgs e) // при отпускании левой кнопки мыши 
        {
            peretaskivanie = false;
            foreach(Papka pap in list_textbox)
            {
                if (pap.textBox == ((TextBox)sender))
                {
                    ((TextBox)sender).Location = new Point(((TextBox)sender).Location.X, (pap.mesto_v_spiske-1) * pap.textBox.Size.Height);

                    break;
                }
            }
        }
    }
}