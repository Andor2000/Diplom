using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Диплом
{
    public partial class Form1 : Form
    {
        int i = 0;
        bool mouse = false;
        object obect_mouse=null;
        public Form1()
        {
            InitializeComponent();
            //button1.Image = Properties.Resources._;
            treeView1.Nodes.Add("ЕГЭ");
            richTextBox1.Text = "Ну типа какой-то текст, который надо обработать";          
        }
        private void button1_Click(object sender, EventArgs e)
       {

            TreeNode newNode = new TreeNode(Convert.ToString(i));   // Создание нового элемента дерева
            i++;
            treeView1.SelectedNode.Nodes.Add(newNode);      // добавление нового элемента дерева
            treeView1.SelectedNode.Expand();    // раскрытие списка элемента дерева
        }

        private void smena_chveta_text(object sender, EventArgs e)
        {
            label1.BackColor = (sender as Button).BackColor; // смена цвета фона label на цвет button который был нажат
            richTextBox1.SelectionColor = label1.BackColor;
        }

        private void smena_chveta_fona(object sender, EventArgs e)
        {
            richTextBox1.BackColor = (sender as Button).BackColor; // смена цвета фона richTextBox1 на цвет button который был нажат
            //richTextBox1.SelectionColor = label1.BackColor;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            if (treeView1.Nodes.Count == 0)
            {
                return;
            }
            treeView1.SelectedNode.Remove();
        }
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            label3.Text = Convert.ToString(e.X);
            label4.Text = Convert.ToString(e.Y);
        }

        private void button20_Click(object sender, EventArgs e)
        {
            panel1.Width = 15;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            treeView1.Nodes.Add("ЕГЭ");
        }
    }
}

//{ перетаскивание объектов
    //private void MouseEvent(object sender, MouseEventArgs e)
    //{
    //    //throw new NotImplementedException();
    //    if(obect_mouse!=null)
    //    obect_mouse.GetType().GetProperty("Location").SetValue(obect_mouse, new Point(Cursor.Position.X, Cursor.Position.Y));
    //}
    //private  void richTextBox1_MouseDown(object sender, MouseEventArgs e)
    //{
    //    mouse = true;
    //    obect_mouse = sender;
    //    //MouseEvent(sender, e);

    //}

    //private void richTextBox1_MouseUp(object sender, MouseEventArgs e)
    //{
    //    mouse = false;
    //}
//}