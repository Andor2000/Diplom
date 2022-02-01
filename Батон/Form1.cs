using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Батон
{
    public partial class Form1 : Form
    {
        List<TextBox> list_textbox = new List<TextBox>();

        public Form1()
        {
            InitializeComponent();
            saveFileDialog1.Filter = "RTF (*.rtf)|*.rtf|TXT (*.txt)|*.txt";

            openFileDialog1.Filter = "RTF (*.rtf)|*.rtf|TXT (*.txt)|*.txt";
            saveFileDialog1.FileName = "Лист1";
        }
        int i = 0;

        private void leave_rename_textBox()
        {

        }
        int finya = 1;
        private void button1_Click(object sender, EventArgs e)
        {
            TextBox textb = new TextBox();
            textb.Location = new Point(panel1.Location.X, panel1.Location.Y);
            
            textb.Left = panel1.Location.X + 25; // отступ слева
            
            textb.Top = panel2.Height + i;  // левая верхняя точка 
            i += textb.Height + 1;
            
            textb.Width = panel1.Width - 30; // ширина
            
            textb.Text = "Фигня "+finya;
            finya++;
            
            textb.BorderStyle = BorderStyle.None; // убираем обводку
            textb.BackColor = panel1.BackColor;
            textb.Leave += textBox_Leave;
            textb.Click += textBox_Click;

            textb.MouseDown += textBox1_MouseDown;
            textb.MouseUp += textBox1_MouseUp;
            textb.MouseMove += textBox1_MouseMove;

            //this.Controls.Add(textb);
            list_textbox.Add(textb);
            panel1.Controls.Add(textb);
            

            // button1.Visible = false;
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e) // сохранить
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string fname = saveFileDialog1.FileName;
                richTextBox1.SaveFile(fname);
                //File.WriteAllText(fname, richTextBox1.Text);
            }
            

        }

        private void button5_Click(object sender, EventArgs e) // открыть
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string fname = openFileDialog1.FileName;
                richTextBox1.LoadFile(fname);
                //richTextBox1.Text = File.ReadAllText(fname);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void textBox_Leave(object sender, EventArgs e)
        {
            TextBox textb = (TextBox)sender;
            textb.ReadOnly = true;
            textb.BackColor = panel1.BackColor;
        }

        private void textBox_Click(object sender, EventArgs e)
        {
            TextBox textb = (TextBox)sender;
            textb.BackColor = Color.SpringGreen;
        }

        private void richTextBox1_Click(object sender, EventArgs e)
        {

        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        bool peretaskivanie = false;
        int x_mouse, y_mouse;
        int x_tb, y_tb;
        int y_tb_konech;

        private void textBox1_MouseDown(object sender, MouseEventArgs e) // кликнул и не отпустил 
        {
            peretaskivanie = true;
            x_mouse = Cursor.Position.X;
            y_mouse = Cursor.Position.Y;
            TextBox textb = (TextBox)sender;
            x_tb = textb.Location.X;
            y_tb = textb.Location.Y;
            y_tb_konech = y_tb;
            label1.Text = Convert.ToString( textb.Location.Y);
        }

        private void textBox1_MouseMove(object sender, MouseEventArgs e) // пока мышь находится на элементе
        {
            if (peretaskivanie)
            {
                TextBox tb = (TextBox)sender;
                //tb.Location = new Point(Cursor.Position.X - this.Location.X-10, Cursor.Position.Y - this.Location.Y-45);
                //tb.Location = new Point(x_tb + (x_mouse + (this.Location.X - Cursor.Position.X)), y_tb + (y_mouse + (this.Location.Y - Cursor.Position.Y)));

                //int x = Cursor.Position.X - x_mouse ;
                int y = Cursor.Position.Y - y_mouse;
                tb.Location = new Point(x_tb , y_tb + y);

                label3.Text = Convert.ToString(tb.Location.Y);

                foreach (TextBox textb in list_textbox)
                {
                    if ( tb.Location.Y >= textb.Location.Y-7 && tb.Location.Y <= textb.Location.Y + 7 && textb != tb)
                    {
                        int help = y_tb_konech;
                        y_tb_konech = textb.Location.Y;
                        textb.Location = new Point(textb.Location.X, help);

                        // y_mouse += 28;
                    }
                }
                

                //label1.Text = Convert.ToString(x);
                //label2.Text = Convert.ToString(y);

                //label3.Text = Convert.ToString(tb.Location.X);
                //label4.Text = Convert.ToString(tb.Location.Y);


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
