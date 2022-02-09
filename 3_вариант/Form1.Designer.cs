
namespace _3_вариант
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("пн");
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("пр");
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("нр");
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem("гр");
            System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem("ер");
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.addElement = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.добавитьПапкуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sdfsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sdfsdfToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sdfsdfToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.sdfsdfToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.sdfsdfsdfToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sdfdsfdsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.добавитьПапкуToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.добавитьНепапкуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.addElement.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.button4);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Location = new System.Drawing.Point(31, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(360, 547);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(226, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = "label1";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(182, 16);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(94, 29);
            this.button4.TabIndex = 5;
            this.button4.Text = "button4";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(135, 16);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(31, 29);
            this.button3.TabIndex = 4;
            this.button3.Text = ">";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(6, 50);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(31, 29);
            this.button2.TabIndex = 3;
            this.button2.Text = "<";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.ContextMenuStrip = this.addElement;
            this.button1.Location = new System.Drawing.Point(6, 16);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(86, 29);
            this.button1.TabIndex = 2;
            this.button1.Text = "Добавить";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // addElement
            // 
            this.addElement.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.addElement.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.добавитьПапкуToolStripMenuItem,
            this.добавитьПапкуToolStripMenuItem1,
            this.добавитьНепапкуToolStripMenuItem});
            this.addElement.Name = "addElement";
            this.addElement.Size = new System.Drawing.Size(205, 76);
            // 
            // добавитьПапкуToolStripMenuItem
            // 
            this.добавитьПапкуToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sdfsToolStripMenuItem});
            this.добавитьПапкуToolStripMenuItem.Name = "добавитьПапкуToolStripMenuItem";
            this.добавитьПапкуToolStripMenuItem.Size = new System.Drawing.Size(204, 24);
            this.добавитьПапкуToolStripMenuItem.Text = "Добавить папку";
            // 
            // sdfsToolStripMenuItem
            // 
            this.sdfsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sdfsdfToolStripMenuItem});
            this.sdfsToolStripMenuItem.Name = "sdfsToolStripMenuItem";
            this.sdfsToolStripMenuItem.Size = new System.Drawing.Size(118, 26);
            this.sdfsToolStripMenuItem.Text = "sdfs";
            // 
            // sdfsdfToolStripMenuItem
            // 
            this.sdfsdfToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sdfsdfToolStripMenuItem1});
            this.sdfsdfToolStripMenuItem.Name = "sdfsdfToolStripMenuItem";
            this.sdfsdfToolStripMenuItem.Size = new System.Drawing.Size(132, 26);
            this.sdfsdfToolStripMenuItem.Text = "sdfsdf";
            // 
            // sdfsdfToolStripMenuItem1
            // 
            this.sdfsdfToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sdfsdfToolStripMenuItem2});
            this.sdfsdfToolStripMenuItem1.Name = "sdfsdfToolStripMenuItem1";
            this.sdfsdfToolStripMenuItem1.Size = new System.Drawing.Size(132, 26);
            this.sdfsdfToolStripMenuItem1.Text = "sdfsdf";
            // 
            // sdfsdfToolStripMenuItem2
            // 
            this.sdfsdfToolStripMenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sdfsdfsdfToolStripMenuItem,
            this.sdfdsfdsToolStripMenuItem});
            this.sdfsdfToolStripMenuItem2.Name = "sdfsdfToolStripMenuItem2";
            this.sdfsdfToolStripMenuItem2.Size = new System.Drawing.Size(132, 26);
            this.sdfsdfToolStripMenuItem2.Text = "sdfsdf";
            // 
            // sdfsdfsdfToolStripMenuItem
            // 
            this.sdfsdfsdfToolStripMenuItem.Name = "sdfsdfsdfToolStripMenuItem";
            this.sdfsdfsdfToolStripMenuItem.Size = new System.Drawing.Size(152, 26);
            this.sdfsdfsdfToolStripMenuItem.Text = "sdfsdfsdf";
            // 
            // sdfdsfdsToolStripMenuItem
            // 
            this.sdfdsfdsToolStripMenuItem.Name = "sdfdsfdsToolStripMenuItem";
            this.sdfdsfdsToolStripMenuItem.Size = new System.Drawing.Size(152, 26);
            this.sdfdsfdsToolStripMenuItem.Text = "sdfdsfds";
            // 
            // добавитьПапкуToolStripMenuItem1
            // 
            this.добавитьПапкуToolStripMenuItem1.Name = "добавитьПапкуToolStripMenuItem1";
            this.добавитьПапкуToolStripMenuItem1.Size = new System.Drawing.Size(204, 24);
            this.добавитьПапкуToolStripMenuItem1.Text = "добавить папку";
            // 
            // добавитьНепапкуToolStripMenuItem
            // 
            this.добавитьНепапкуToolStripMenuItem.Name = "добавитьНепапкуToolStripMenuItem";
            this.добавитьНепапкуToolStripMenuItem.Size = new System.Drawing.Size(204, 24);
            this.добавитьНепапкуToolStripMenuItem.Text = "добавить непапку";
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel3.BackColor = System.Drawing.Color.LightCoral;
            this.panel3.Location = new System.Drawing.Point(182, 85);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(170, 454);
            this.panel3.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel2.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.panel2.Location = new System.Drawing.Point(3, 85);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(170, 451);
            this.panel2.TabIndex = 0;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(549, 166);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(125, 27);
            this.textBox1.TabIndex = 1;
            this.textBox1.Text = "Привет мир";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 20;
            this.listBox1.Items.AddRange(new object[] {
            "павпап",
            "рарпар",
            "орпо",
            "орпор",
            "орп"});
            this.listBox1.Location = new System.Drawing.Point(415, 268);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(150, 104);
            this.listBox1.TabIndex = 3;
            // 
            // listView1
            // 
            this.listView1.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.listView1.HideSelection = false;
            this.listView1.HotTracking = true;
            this.listView1.HoverSelection = true;
            this.listView1.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3,
            listViewItem4,
            listViewItem5});
            this.listView1.Location = new System.Drawing.Point(591, 221);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(100, 173);
            this.listView1.TabIndex = 2;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(769, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(146, 20);
            this.label2.TabIndex = 7;
            this.label2.Text = "Содержимое папки";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1011, 573);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.addElement.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ContextMenuStrip addElement;
        private System.Windows.Forms.ToolStripMenuItem добавитьПапкуToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sdfsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sdfsdfToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sdfsdfToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem sdfsdfToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem sdfsdfsdfToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sdfdsfdsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem добавитьПапкуToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem добавитьНепапкуToolStripMenuItem;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

