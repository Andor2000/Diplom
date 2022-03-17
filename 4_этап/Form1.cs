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
        private Color color_fona_left_navel_na_ikonku = ColorTranslator.FromHtml("#3d3d5a");
        private Color color_fona_left_aktivnoy_ikonki = ColorTranslator.FromHtml("#6B90B8");
        private Color color_fona_left_panel = Color.FromArgb(51, 51, 76);
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
                ((PictureBox)sender).Parent.BackColor = color_fona_left_navel_na_ikonku;
            }
        }
        private void navel_na_ikonky_MouseLeave(object sender, EventArgs e)         // убрал мышку с иконки         
        {
            ((PictureBox)sender).Parent.BackColor = ((PictureBox)sender).Parent.Parent.BackColor;
        }
        private void navel_na_ikonky_MouseDown(object sender, MouseEventArgs e)     // нажал на мышь (не отпустил)  
        {
            ((PictureBox)sender).Parent.BackColor = color_fona_left_aktivnoy_ikonki;
            zajata_mishka_na_ikonke = true;
        }
        private void navel_na_ikonky_MouseUp(object sender, MouseEventArgs e)       // отпустил мышь                
        {
            ((PictureBox)sender).Parent.BackColor = color_fona_left_panel;
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
                btnNazad.Image = Image.FromFile(@"image\svernut.png");
                this.MinimumSize = new Size(1350, 0);
            }
            else
            {
                panel1.Size = new Size(0, this.Size.Height);
                btnNazad.Image = Image.FromFile(@"image\razvernut.png");
                this.MinimumSize = new Size(1050, 0);
            }
            panel_svernuta = !panel_svernuta;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            fontDialog1.ShowDialog();
            richTextBox1.SelectionFont = fontDialog1.Font;

            return;

            string path = Application.ExecutablePath;
            for (int i = 0; i < 4; i++)
            {
                while (path[path.Length - 1] != '\\')
                {
                    path = path.Remove(path.Length - 1, 1);
                }
                path = path.Remove(path.Length - 1, 1);
            }          
            
            string strConnect = path + @"\DataBase1234567890.mdf";
            string strConnect2 = path + @"\DataBase1234567890.ldf";
            //MessageBox.Show(strConnect);

            SqlConnection connection = new SqlConnection("Server=(LocalDB)\\MSSQLLocalDB;Integrated security=SSPI;database=master");
            string CreateDatabase = "CREATE DATABASE приветмир ON PRIMARY " +
                             "(NAME = MyDatabase_Data, " +
                             @"FILENAME = '" + strConnect + "', " +
                             "SIZE = 6MB, MAXSIZE = 4GB, FILEGROWTH = 10%) " +
                             "LOG ON (NAME = MyDatabase_Log, " +
                             @"FILENAME = '" + strConnect2 + "', " +
                             "SIZE = 1MB, MAXSIZE = 200MB, FILEGROWTH = 10%)";

            
                string delete = "DROP DATABASE приветмир;";
                SqlCommand command  = new SqlCommand(CreateDatabase, connection);
            try
            {
                connection.Open();
                command.ExecuteNonQuery();  // добавление бд

                command = new SqlCommand(delete, connection);
                command.ExecuteNonQuery();  // удаление бд
                MessageBox.Show("Ну крч базу данных добавил, и удалил сразу (успешно)", "Database", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Database", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (textBox1.Text.Length >= 5 && e.KeyChar!=8) // если уже 5 букв введено, но разрешить BackSpace
            {
                e.Handled = true;
            }
        }
    }
}
