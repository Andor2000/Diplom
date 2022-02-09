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

        int id_papki = 1;
        //int id_aktivnoy_papki=0;

        Papka aktivnaya_papka = new();
        bool peretaskivanie = false;

        int x_mouse, y_mouse;
        int y_tb;
        public Form1()
        {
            InitializeComponent();
            // textBox1.Visible = false;
            listBox1.Visible = false;
            listView1.Visible = false;
            //button2.Visible = false;
            button2.Enabled = false;
            button3.Visible = false;
            button4.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e) // добавление в левую колонку папок
        {
            Papka papka = new Papka();
            id_papki = papka.sozdanie_papki(panel2, list_textbox_left, id_papki);

            papka.kolonka = 1;

            int kolvo_lev_kolon = 0;
            foreach (Papka pap in list_textbox_left)
            {
                if (pap.kolonka == 1)
                {
                    kolvo_lev_kolon++;
                }
            }
            papka.mesto_v_spiske = kolvo_lev_kolon + 1;

            if (list_textbox_left.Count == 0) // значит создается вообще первая папка
            {
                papka.papka_roditel = 0;
            }
            else
            {
                papka.papka_roditel = list_textbox_left[0].papka_roditel;
                if (papka.papka_roditel != 0)
                {
                    foreach(Papka pap in list_textbox)
                    {
                        if (pap.id == papka.papka_roditel)
                        {
                            pap.papki_potomki.Add(papka);
                            break;
                        }
                        
                    }
                }
            }


            panel2.Controls.Add(papka.textBox); // лобавляем в левую колонку
            list_textbox.Add(papka);            // добавление в общий список папок
            list_textbox_left.Add(papka);       // спиок папок в левой колонке

            papka.textBox.MouseDown += kliknyl_po_textBox;
            papka.textBox.MouseUp += otpustil_textBox;
            papka.textBox.MouseMove += derjish_textBox;
            papka.textBox.MouseDoubleClick += perehod_v_papky;
        }
        private void perestavlenie_v_kolonke(Papka papka)
        {
            List<Papka> list = papka.kolonka == 1 ? list_textbox_left : list_textbox_right;


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
            if (papka.kolonka == 1)
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
                && list.Contains(papka)
                && papka.textBox.BackColor != Color.GreenYellow
                && label1.Text != "label1")
            {
                foreach (Papka pap in list) // поправление всех папок в прошлой колонке которые ниже
                {
                    if (pap.mesto_v_spiske > papka.mesto_v_spiske)
                    {
                        pap.textBox.Location = new Point(pap.textBox.Location.X, pap.textBox.Location.Y - pap.textBox.Size.Height);
                        pap.mesto_v_spiske--;
                    }
                }
                pn.Controls.Add(papka.textBox); // добавление на новую панель
                list.Remove(papka); // убираем из прошлой коолонки
                new_list.Add(papka); // добавляем в новую колонку
                papka.kolonka = papka.kolonka == 1 ? 2 : 1; // показываем, в какой теперь колонке
                papka.mesto_v_spiske = new_list.Count;
                papka.textBox.Location = new Point(papka.Location.X, papka.mesto_v_spiske * papka.textBox.Size.Width);

                if (pn == panel3)   // если добавляем из левой колонки в правую
                {
                    if (aktivnaya_papka.papka_roditel != 0)
                    {
                         foreach(Papka pap in list_textbox)
                        {
                            if(pap.id == aktivnaya_papka.papka_roditel) // удаляем из предыдущей папки перенесенного потомка
                            {
                                pap.papki_potomki.Remove(papka);
                            }
                        }
                    }

                    aktivnaya_papka.papki_potomki.Add(papka);

                    papka.papka_roditel = aktivnaya_papka.id;
                }
                else
                {
                    aktivnaya_papka.papki_potomki.Remove(papka);// удаляем из потомка
                    papka.papka_roditel = aktivnaya_papka.papka_roditel;
                }
                string str = "";

                foreach (Papka papa in aktivnaya_papka.papki_potomki)
                {
                    str += papa.textBox.Text + "\n";
                }

                label2.Text = str;
            }
        }
        private void kliknyl_po_textBox(object sender, MouseEventArgs e) // кликнул и не отпустил 
        {
            peretaskivanie = true;
            x_mouse = Cursor.Position.X;
            y_mouse = Cursor.Position.Y;
            TextBox textb = (TextBox)sender;
            y_tb = textb.Location.Y;
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
        private void otpustil_textBox(object sender, MouseEventArgs e) // при отпускании левой кнопки мыши 
        {
            peretaskivanie = false;
            foreach (Papka pap in list_textbox)
            {
                if (pap.textBox == ((TextBox)sender))
                {
                    ((TextBox)sender).Location = new Point(((TextBox)sender).Location.X, (pap.mesto_v_spiske - 1) * pap.textBox.Size.Height);

                    break;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e) // вернуться назад в предыдущую папку
        {
            // list_textbox_left[0].papka_roditel;
            panel2.Controls.Clear(); // очищаем левую колонку
            panel3.Controls.Clear(); // очищаем правую колонку
            list_textbox_right.Clear();
            // переносим левую колонку вправо
            int i = 1;
            while (i <= list_textbox_left.Count) // количество дочерних папок
            {
                foreach (Papka pap in list_textbox_left) // смотрим каждую папку 
                {
                    if (pap.mesto_v_spiske == i)
                    {
                       // pap.textBox.BackColor = panel2.BackColor;
                        pap.kolonka = 2;
                        panel3.Controls.Add(pap.textBox); // добавляем в правую колонку 
                        list_textbox_right.Add(pap);    // добавляем в лист, ответчстввенный за правую колонку
                        break;
                    }
                }
                i++;
            }
            list_textbox_left.Clear();

            // находим родителя-родителя
            int rod = 0;
            foreach(Papka pap in list_textbox)
            {
                if(aktivnaya_papka.papka_roditel == pap.id)
                {
                    rod = pap.papka_roditel;
                }
            }
            // делаем левую колонку через родителя-родителя

            //MessageBox.Show(Convert.ToString(rod));
            int kolvo_pred_papok = 0;
            foreach(Papka pap in list_textbox)
            {
                if(pap.papka_roditel == rod)
                {
                    kolvo_pred_papok++;
                }

            }
            //MessageBox.Show(Convert.ToString(kolvo_pred_papok));
            i = 1;
            while (i <= kolvo_pred_papok) // количество родительских папок
            {
                foreach (Papka pap in list_textbox) // смотрим каждую папку 
                {
                    if (pap.mesto_v_spiske == i && pap.papka_roditel == rod)
                    {
                        pap.textBox.BackColor = panel2.BackColor;
                        if (pap.id == aktivnaya_papka.papka_roditel)
                        {
                           
                            aktivnaya_papka.textBox.BackColor = panel2.BackColor;
                            aktivnaya_papka = pap;
                            aktivnaya_papka.textBox.BackColor = Color.GreenYellow;
                            label1.Text = aktivnaya_papka.textBox.Text;
                        }
                        
                        pap.kolonka = 1;
                        panel2.Controls.Add(pap.textBox); // добавляем в правую колонку 
                        list_textbox_left.Add(pap);    // добавляем в лист, ответстввенный за правую колонку
                        break;
                    }
                }
                i++;
            }
            if (aktivnaya_papka.papka_roditel == 0)
            {
                button2.Enabled = false;
            }
            

        }


        private void perehod_v_papky(object sender, MouseEventArgs e) // два раза клинкул по папке
        {
            aktivnaya_papka.textBox.BackColor = panel2.BackColor;  // меняем цвет на фон у предыдущей активной папки 

            foreach (Papka pap in list_textbox) // делаем папку активной
            {
                if (pap.textBox == ((TextBox)sender))
                {
                    aktivnaya_papka = pap;
                    aktivnaya_papka.textBox.BackColor = Color.GreenYellow;
                    label1.Text = ((TextBox)sender).Text;
                    break;
                }
            }


            if (aktivnaya_papka.kolonka == 1)
            {
                panel3.Controls.Clear(); // очищаем правую
                list_textbox_right.Clear(); // очищаем лист с правой колонки

                int i = 1;
                while (i <= aktivnaya_papka.papki_potomki.Count) // количество дочерних папок
                {
                    foreach (Papka pap in aktivnaya_papka.papki_potomki) // смотрим каждую папку 
                    {
                        if (pap.mesto_v_spiske == i)
                        {
                            pap.kolonka = 2;
                            panel3.Controls.Add(pap.textBox); // добавляем в правую колонку 
                            list_textbox_right.Add(pap);    // добавляем в лист, ответчстввенный за правую колонку
                            break;
                        }
                    }
                    i++;
                }
            }
            else
            {
                button2.Enabled=true; // включил кнопку возврата назад

                panel2.Controls.Clear(); // очищаем левую колонку
                panel3.Controls.Clear(); // очищаем правую колонку
                list_textbox_left.Clear();

                int i = 1;
                while (i <= list_textbox_right.Count)
                {
                    foreach (Papka pap in list_textbox_right)
                    {
                        if (pap.mesto_v_spiske == i)
                        {
                            pap.kolonka = 1;
                            panel2.Controls.Add(pap.textBox);
                            list_textbox_left.Add(pap);
                            break;
                        }
                    }
                    i++;
                }
                list_textbox_right.Clear();

               i = 1;
                while (i <= aktivnaya_papka.papki_potomki.Count) // количество дочерних папок
                {
                    foreach (Papka pap in aktivnaya_papka.papki_potomki) // смотрим каждую подпапку 
                    {
                        if (pap.mesto_v_spiske == i)
                        {
                            pap.kolonka = 2;
                            panel3.Controls.Add(pap.textBox); // добавляем в правую колонку 
                            list_textbox_right.Add(pap);    // добавляем в лист, ответчстввенный за правую колонку
                            break;
                        }
                    }
                    i++;
                }
            }



            string str = "";
            foreach (Papka papa in aktivnaya_papka.papki_potomki)
            {
                str += papa.textBox.Text + "\n";
            }

            label2.Text = str;
        }
    }
}