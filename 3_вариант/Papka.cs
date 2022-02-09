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
    public partial class  Papka: Form
    {
        public TextBox textBox = new TextBox();
        public int id;
        public int kolonka;
        public int mesto_v_spiske;
        public int papka_roditel;
        public List<Papka> papki_potomki = new();

        
        public int sozdanie_papki(Panel panel2, List<Papka> list_textbox, int id_papki)
        {
            id = id_papki;

            this.textBox.Width = panel2.Width - 1;
            textBox.BackColor = panel2.BackColor;

            int i = (list_textbox.Count) * textBox.Size.Height;
            textBox.Location = new Point(1, i);

            textBox.Text = "Папка " + id_papki;
            id_papki++; 
            textBox.TextAlign = HorizontalAlignment.Center; // выровнять по центру
            textBox.ShortcutsEnabled = false; // убрать контекстное меню

            return id_papki;
        }
    }
}
