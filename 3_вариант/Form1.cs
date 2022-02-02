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
        List<TextBox> list_textbox = new List<TextBox>();

        List<TextBox> list_textbox2 = new List<TextBox>();
        int papka=1;
        bool peretaskivanie = false;
        int x_mouse, y_mouse;
        int x_tb, y_tb;
        int y_tb_konech;


        public Form1()
        {
            InitializeComponent();
            textBox1.Visible = false;
            listBox1.Visible = false;
            listView1.Visible = false;
            button2.Visible = false;
            button3.Visible = false;
            button4.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e) // добавление в левую колонку папок
        {
            TextBox textBox = new TextBox();
            panel2.Controls.Add(textBox);

            textBox.Width = panel2.Width-1; 
            textBox.BackColor = panel2.BackColor;          

            int i = (list_textbox.Count) * textBox.Size.Height;
            textBox.Location = new Point(1, i);

            textBox.Text = "Папка " + papka;
            papka++;

            textBox.TextAlign = HorizontalAlignment.Center; // выровнять по центру
            textBox.ShortcutsEnabled = false; // убрать контекстное меню

            list_textbox.Add(textBox);
            textBox.MouseDown += textBox1_MouseDown;
            textBox.MouseUp += textBox1_MouseUp;
            textBox.MouseMove += textBox1_MouseMove;
        }

        private void textBox1_MouseDown(object sender, MouseEventArgs e) // кликнул и не отпустил 
        {
            peretaskivanie = true;
            x_mouse = Cursor.Position.X;
            y_mouse = Cursor.Position.Y;
            TextBox textb = (TextBox)sender;
            x_tb = textb.Location.X;
            y_tb = textb.Location.Y;
            y_tb_konech = y_tb;
        }

        private void textBox1_MouseMove(object sender, MouseEventArgs e) // пока мышь находится на элементе
        {
            if (peretaskivanie)
            {
                TextBox tb = (TextBox)sender;
                int y = Cursor.Position.Y - y_mouse;
                tb.Location = new Point(x_tb, y_tb + y);
                
                // переставляет в левой колонке
                foreach (TextBox textb in list_textbox)  
                {
                    if (textb != tb && list_textbox.Contains(tb) 
                        && tb.Location.Y >= textb.Location.Y - 7 && tb.Location.Y <= textb.Location.Y + 7)
                    {
                        int help = y_tb_konech;
                        y_tb_konech = textb.Location.Y;
                        textb.Location = new Point(textb.Location.X, help);
                    }
                }
                // переставляет в правой колонке
                foreach (TextBox textb in list_textbox2) 
                {
                    if (textb != tb && list_textbox2.Contains(tb)
                        && tb.Location.Y >= textb.Location.Y - 7 && tb.Location.Y <= textb.Location.Y + 7)
                    {
                        int help = y_tb_konech;
                        y_tb_konech = textb.Location.Y;
                        textb.Location = new Point(textb.Location.X, help);
                    }
                }
                // из левой в правую                
                if (Cursor.Position.X - x_mouse > tb.Size.Width / 2 && list_textbox.Contains(tb)) {
                    list_textbox.Remove(tb); // удалил из первого
                    list_textbox2.Add(tb); // добавил во второй
                    tb.BackColor = panel3.BackColor;
                    panel3.Controls.Add(tb); // во вторую панель перенести
                    foreach (TextBox textb in list_textbox) // поправляет левую колонку
                    {
                        if (textb.Location.Y > tb.Location.Y)
                        {
                            textb.Location = new Point(textb.Location.X, textb.Location.Y - textb.Size.Height) ;
                        }
                    }
                    
                    y_tb_konech = (list_textbox2.Count - 1) * tb.Size.Height;
                }
                // из правой в левую
                if ((Cursor.Position.X - x_mouse)*(-1) > tb.Size.Width / 2 && list_textbox2.Contains(tb)) 
                {
                    list_textbox2.Remove(tb); // удалил из первого
                    list_textbox.Add(tb); // добавил во второй
                    tb.BackColor = panel2.BackColor;
                    panel2.Controls.Add(tb); // во вторую панель перенести
                    
                    foreach (TextBox textb in list_textbox2) // поправляет левую колонку
                    {
                        if (textb.Location.Y > tb.Location.Y)
                        {
                            textb.Location = new Point(textb.Location.X, textb.Location.Y - textb.Size.Height);
                        }
                    }                    
                    y_tb_konech = (list_textbox.Count - 1) * tb.Size.Height;
                }
            }
        }

        private void textBox1_MouseUp(object sender, MouseEventArgs e) // при отпускании левой кнопки мыши 
        {
            peretaskivanie = false;
            TextBox tb = (TextBox)sender;
            tb.Location = new Point(x_tb, y_tb_konech);
        }
    }
}
