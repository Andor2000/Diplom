using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace _4_этап
{
    public partial class Form1 : Form
    {
        private Color color_fona_navel_na_papky = ColorTranslator.FromHtml("#3d3d5a");
        private Color color_fona_aktivnoy_papke = ColorTranslator.FromHtml("#6B90B8");
        //private Color color_fona = Color.FromArgb(51, 51, 76);
        bool zajata_mishka_na_ikonke = false;
        Timer timer_vhod;
        int timer_value = 1;
        MouseButtons mb;
        bool panel_svernuta = false;

        PanelNavigatii papka = new();

        public Form1()
        {
            InitializeComponent();
            papka.object_kotorie_prigodatsa(this, panel3, panel4, richTextBox1, buttonBackClick, label2);
            //button3.Enabled = false;

            LocalDB open_db = new();
            open_db.openBook();
            timer_vhod = new();
            timer_vhod.Interval = 100;
            timer_vhod.Tick += timer_hvod_focus;
            timer_vhod.Enabled = true;         
        }
        private void addPapka_Click(object sender, EventArgs e)  // добавление в левую колонку папок
        {
            PanelNavigatii pap = new();
            pap.sozdanie_papki(pap);
        }
        private void addList_Click(object sender, EventArgs e)
        {
            PanelNavigatii list = new();
            list.sozdanie_lista(list);
        }
        private void navel_na_ikonky_MouseMove(object sender, MouseEventArgs e)     // навел на иконку              
        {
            if (!zajata_mishka_na_ikonke) // если кнопка зажата, то фон остается синим (нажатым)
            {
                ((PictureBox)sender).Parent.BackColor = color_fona_navel_na_papky;
            }
        }
        private void navel_na_ikonky_MouseLeave(object sender, EventArgs e)         // убрал мышку с иконки         
        {
            ((PictureBox)sender).Parent.BackColor = ((PictureBox)sender).Parent.Parent.BackColor;
        }
        private void navel_na_ikonky_MouseDown(object sender, MouseEventArgs e)     // нажал на мышь (не отпустил)  
        {
            ((PictureBox)sender).Parent.BackColor = color_fona_aktivnoy_papke;
            zajata_mishka_na_ikonke = true;
        }
        private void navel_na_ikonky_MouseUp(object sender, MouseEventArgs e)       // отпустил мышь                
        {
            ((PictureBox)sender).Parent.BackColor = Color.FromArgb(51, 51, 76);
            zajata_mishka_na_ikonke = false;
        }
        private void button1_Click(object sender, EventArgs e)
       {
            label2.Text = Convert.ToString(Application.ExecutablePath);
        }     
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            papka.close_book();
        }
        private void BackClick_Click(object sender, EventArgs e)
        {
            papka.vernutsa_nazad();            
        }       
        private void timer_hvod_focus(object sender, EventArgs e)
        {
            timer_value++;
            if (timer_value >= 1)
            {
                richTextBox1.Focus();
                timer_vhod.Enabled = false;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (panel_svernuta)
            {
                panel1.Size = new Size(300, this.Size.Height);
                nazadButton.Image = Image.FromFile(@"image\svernut.png");
                this.MinimumSize = new Size(1350, 0);
                //if (this.Size.Width < 1350)
                //{
                //    this.Size = new(1350, this.Size.Width);
                //}

            }
            else
            {
                panel1.Size = new Size(0, this.Size.Height);
                nazadButton.Image = Image.FromFile(@"image\razvernut.png");
                this.MinimumSize = new Size(1050, 0);
            }
            panel_svernuta = !panel_svernuta;
        }
    }
}
