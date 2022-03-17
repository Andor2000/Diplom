using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Data.SqlClient;
using System.Data;
using System.IO;

namespace _4_этап
{
    class PanelNavigatii
    {
        private static int papka_praroditel_objey_programmi_id = 0;

        MouseButtons mouseButtons = new();
        private static Form form;
        private static PictureBox pictureBoxBack = new();
        private static Panel panel_left = new();
        private static Panel panel_right = new();
        private static RichTextBox list_vivoda = new();
        private static bool mojno_vernutsa_nazad = false;
        private static Label label;

        private static PanelNavigatii navel_na_object = new();
        private static PanelNavigatii aktivnaya_papka = new();
        private static PanelNavigatii aktivniy_list = new();
        private static bool est_aktivnaya_papka = false;
        private static bool est_asktivniy_list = false;
        private bool najata_left_button = false;

        private FlowLayoutPanel flpanel = new();
        private PictureBox picBox = new();
        private TextBox textBox = new();
        private int id;
        private int kolonka;
        private int mesto_v_spiske;
        private int papka_roditel;
        private bool this_papka;
        private List<PanelNavigatii> papki_potomki = new();
        private ContextMenuStrip contextMS = new();

        private static List<PanelNavigatii> list_object = new();
        private static List<PanelNavigatii> list_object_left = new();
        private static List<PanelNavigatii> list_object_right = new();

        private Timer timer = new();
        private int timer_value = 1;

        private int mouse_x;
        private int mouse_y;
        private int textBox_y;
        private string pereimenovanie;
        private bool vozmojno_peretaskivanie = false;
        private bool idet_peretaskivanie = false;
        private bool bilo_rename = false;

        private Color color_text_obichniy = Color.White;
        private Color color_text_rename = Color.Black;
        private Color color_fona_object = Color.FromArgb(51, 51, 76);
        private Color color_fona_navel_na_object = ColorTranslator.FromHtml("#3d3d5a");
        private Color color_fona_aktivniy_obj = ColorTranslator.FromHtml("#6B90B8");
        private Color color_fona_peretaskivaniya = ColorTranslator.FromHtml("#738497");
        private Color color_fona_rename = Color.White;

        public void object_kotorie_prigodatsa(Form form1, Panel panel_left1, Panel panel_right1, RichTextBox list_vivoda1, PictureBox picBox_nazad, Label label1)
        {
            form = form1;
            panel_left = panel_left1;
            panel_right = panel_right1;
            list_vivoda = list_vivoda1;
            pictureBoxBack = picBox_nazad;
            label = label1;
        }
        public void sozdanie_papki_DB(PanelNavigatii papka, int id_db, string name_db, int mesto_db, int id_roditel)
        {
            id = id_db;
            textBox.Text = name_db;
            kolonka = 1;
            mesto_v_spiske = mesto_db;
            papka_roditel = id_roditel;
            this_papka = true;

            if (papka_roditel != 0)
            {
                foreach (PanelNavigatii pap in list_object)
                {
                    if (pap.id == papka_roditel)
                    {
                        pap.papki_potomki.Add(papka);
                        break;
                    }
                }
            }

            objie_metodi_pri_sozdanii_obj(papka);
        }
        public void sozdanie_lista_DB(PanelNavigatii list, int id_db, string name_db, int mesto_db, int id_roditel)
        {
            id = id_db;
            mesto_v_spiske = mesto_db;
            papka_roditel = id_roditel;
            this_papka = false;

            textBox.Text = name_db;
            if (papka_roditel != 0)
            {
                foreach (PanelNavigatii obj in list_object)
                {
                    if (obj.id == papka_roditel && obj.this_papka)
                    {
                        obj.papki_potomki.Add(list);
                        break;
                    }
                }
            }

            objie_metodi_pri_sozdanii_obj(list);
        }
        public void sozdanie_papki(PanelNavigatii papka)
        {

            kolonka = 1;
            mesto_v_spiske = list_object_left.Count + 1;
            papka_roditel = papka_praroditel_objey_programmi_id;
            textBox.Text = "Папка";
            this_papka = true;

            LocalDB db = new();
            id = LocalDB.id_new_papki(db, textBox.Text, mesto_v_spiske);
            db.new_content(db, id, papka_roditel);

            if (papka_roditel != 0)
            {
                foreach (PanelNavigatii pap in list_object)
                {
                    if (pap.id == papka_roditel)
                    {
                        pap.papki_potomki.Add(papka);
                        break;
                    }
                }
            }
            objie_metodi_pri_sozdanii_obj(papka);
        }
        public void sozdanie_lista(PanelNavigatii list)
        {
            kolonka = 1;
            mesto_v_spiske = list_object_left.Count + 1;
            papka_roditel = papka_praroditel_objey_programmi_id;
            this_papka = false;
            textBox.Text = "Лист";

            LocalDB db = new();
            id = LocalDB.id_new_lista(db, textBox.Text, mesto_v_spiske, papka_roditel);
            //db.new_content(db, id, papka_roditel);

            if (papka_roditel != 0)
            {
                foreach (PanelNavigatii pap in list_object)
                {
                    if (pap.id == papka_roditel)
                    {
                        pap.papki_potomki.Add(list);
                        break;
                    }
                }
            }
            objie_metodi_pri_sozdanii_obj(list);
        }
        private void objie_metodi_pri_sozdanii_obj(PanelNavigatii obj)
        {
            contextMenuSozdanie(contextMS);
            sozd_FLPanel(mesto_v_spiske);
            sozd_PictureBox();                
            sozd_TextBox();

            //flpanel.ContextMenuStrip = contextMS;
            //flpanel.MouseUp += perehod_v_object_MouseUp;            // клинкул по папке (отпустил мышь)
            //flpanel.MouseMove += navedenie_na_object_MouseMove;     // курсор на элементе / ПЕРЕТАСКИВАНИЕ
            //flpanel.MouseLeave += ubral_cursor_s_object_MouseLeave; // покинул папку (не кликая) 
            //flpanel.MouseDown += najal_ne_otpustil_MouseDown;       // кликнул по папке (не отпустил мышь)  перетаскивание
            //flpanel.Leave += pokinul_obj_Leave;                     // переименовал и покинул текстбокс
            //flpanel.KeyDown += najali_enter_KeyDown;                // при вводе TextBox нажали Enter (pokinul_obj_Leave)

            textBox.ContextMenuStrip = contextMS;
            textBox.MouseUp += perehod_v_object_MouseUp;            // клинкул по папке (отпустил мышь)
            textBox.MouseMove += navedenie_na_object_MouseMove;     // курсор на элементе / ПЕРЕТАСКИВАНИЕ
            textBox.MouseLeave += ubral_cursor_s_object_MouseLeave; // покинул папку (не кликая) 
            textBox.MouseDown += najal_ne_otpustil_MouseDown;       // кликнул по папке (не отпустил мышь)  перетаскивание
            textBox.Leave += pokinul_obj_Leave;                     // переименовал и покинул текстбокс
            textBox.KeyDown += najali_enter_KeyDown;                // при вводе TextBox нажали Enter (pokinul_obj_Leave)

            //picBox.ContextMenuStrip = contextMS;
            //picBox.MouseUp += perehod_v_object_MouseUp;            // клинкул по папке (отпустил мышь)
            //picBox.MouseMove += navedenie_na_object_MouseMove;     // курсор на элементе / ПЕРЕТАСКИВАНИЕ
            //picBox.MouseLeave += ubral_cursor_s_object_MouseLeave; // покинул папку (не кликая) 
            //picBox.MouseDown += najal_ne_otpustil_MouseDown;       // кликнул по папке (не отпустил мышь)  перетаскивание
            //picBox.Leave += pokinul_obj_Leave;                     // переименовал и покинул текстбокс
            //picBox.KeyDown += najali_enter_KeyDown;                // при вводе TextBox нажали Enter (pokinul_obj_Leave)


            list_object.Add(obj);            // добавление в общий список папок
            if (obj.papka_roditel == papka_praroditel_objey_programmi_id)
            {
                list_object_left.Add(obj);       // спиок папок в левой колонке
                panel_left.Controls.Add(flpanel); // добавляем на панель
                kolonka = 1;
            }

            timer.Interval = 1;
            timer.Enabled = false;
            timer.Tick += timer_tick;
        }
        public void vernutsa_nazad()
        {
            if (mojno_vernutsa_nazad)
            {
                aktivnaya_papka.flpanel.BackColor = color_fona_object;
                aktivnaya_papka.textBox.BackColor = color_fona_object;

                panel_left.Controls.Clear(); // очищаем экран
                panel_right.Controls.Clear();// очищаем экран
                list_object_right.Clear();

                foreach (PanelNavigatii pap in list_object_left) // переносим левую колонку вправо
                {
                    pap.kolonka = 2;
                    panel_right.Controls.Add(pap.flpanel); // добавляем в правую колонку 
                    list_object_right.Add(pap);    // добавляем в лист, ответчстввенный за правую колонку
                }
                list_object_left.Clear();

                // находим родителя-родителя
                foreach (PanelNavigatii pap in list_object)
                {
                    if (aktivnaya_papka.papka_roditel == pap.id && pap.this_papka)
                    {
                        papka_praroditel_objey_programmi_id = pap.papka_roditel;
                    }
                }
                // делаем левую колонку через родителя-родителя
                int kolvo_pred_papok = 0;
                foreach (PanelNavigatii pap in list_object)
                {
                    if (pap.papka_roditel == papka_praroditel_objey_programmi_id)
                    {
                        kolvo_pred_papok++;
                    }
                }
                // 
                foreach (PanelNavigatii pap in list_object) // смотрим каждую папку 
                {
                    if (pap.papka_roditel == papka_praroditel_objey_programmi_id)
                    {
                        if (pap.id == aktivnaya_papka.papka_roditel)
                        {
                            aktivnaya_papka = pap;
                            aktivnaya_papka.flpanel.BackColor = color_fona_aktivniy_obj;
                            aktivnaya_papka.textBox.BackColor = color_fona_aktivniy_obj;
                        }
                        pap.kolonka = 1;
                        panel_left.Controls.Add(pap.flpanel); // добавляем в правую колонку 
                        list_object_left.Add(pap);    // добавляем в лист, ответстввенный за правую колонку

                    }
                }
                if (papka_praroditel_objey_programmi_id != 0) // если не 0, то сделать активной
                {
                    pictureBoxBack.Image = Image.FromFile(@"image\nazad_open.png");
                    mojno_vernutsa_nazad = true;
                }
                else
                {
                    pictureBoxBack.Image = Image.FromFile(@"image\nazad_close.png");
                    mojno_vernutsa_nazad = false;
                }
                aktivnaya_papka.flpanel.Focus(); // там какая-то фигня, и текст выделяется, поэтому вот

            }
        }
        public void close_book()
        {
            if (est_asktivniy_list)
            {
                LocalDB db1 = new();
                list_vivoda.SaveFile("save.rtf");
                string text_save = File.ReadAllText("save.rtf");
                db1.save_list(db1, aktivniy_list.id, text_save);
                File.Delete("save.rtf");
            }
        }
        private void sozd_FLPanel(int mesto)
        {
            flpanel.BackColor = color_fona_object;// цвет фона 
            flpanel.Size = new Size(panel_left.Width, 35); // размер
            flpanel.Padding = new Padding(5, 0, 0, 0);
            flpanel.Location = new Point(0, (mesto - 1) * flpanel.Size.Height);
        }
        private void sozd_PictureBox()
        {
            picBox.SizeMode = PictureBoxSizeMode.Zoom;  // картинка во весь pictureBox
            picBox.Size = new Size(31, 31);
            if (this_papka)
                picBox.Image = Image.FromFile(@"image\blue_papka.png");
            else
                picBox.Image = Image.FromFile(@"image\list_bumagi.png");

            picBox.Left = 0;
            picBox.Top = 0;

            picBox.Margin = new Padding(0);
            flpanel.Controls.Add(picBox);               // добавление на панель
        }        
        private void sozd_TextBox()
        {
            textBox.ForeColor = color_text_obichniy; // цвет текста
            textBox.BackColor = color_fona_object; // цвет фона 
            textBox.Font = new Font("Segoe UI", 11);
            textBox.Width = flpanel.Width - 11 - picBox.Width; // ширина текстбокса           минимум 11   
            //textBox.TextAlign = HorizontalAlignment.Center; // выровнять по центру
            textBox.ShortcutsEnabled = false; // убрать контекстное меню
            textBox.BorderStyle = BorderStyle.None;
            textBox.ReadOnly = true;

            textBox.Cursor = System.Windows.Forms.Cursors.Arrow; // вид курсора всегда одинаковый
            flpanel.Controls.Add(textBox); // лобавляем в левую колонку
        }
        private void najal_ne_otpustil_MouseDown(object sender, MouseEventArgs e)            // нажал на объект (не отпустил мышь)  перетаскивание       
        {
            if (e.Button == MouseButtons.Left)
            {
                najata_left_button = true;
                if (((TextBox)sender).ReadOnly) // если не идет переименование
                {
                    ((TextBox)sender).Parent.Focus(); // убираем каретку

                    foreach (PanelNavigatii pap in list_object) // перетаскивание
                    {
                        if (pap.textBox == ((TextBox)sender))
                        {
                            vozmojno_peretaskivanie = true; // не факт, что идет перетаскивание, возможно просто 
                            timer.Enabled = true;

                            navel_na_object = pap; // запоминаем выбранную папку 

                            mouse_x = Cursor.Position.X; // запоминаем координаты 
                            mouse_y = Cursor.Position.Y;
                            textBox_y = navel_na_object.flpanel.Location.Y;

                            navel_na_object.flpanel.BringToFront();  // будет поверх остальных textBox

                            break;
                        }
                    }
                }
            }
            else
            {
                najata_left_button = false;
            }
        }      
        private void perehod_v_object_MouseUp(object sender, EventArgs e)               // клинкул по объекту (отпустил мышь)                       
        {
            if (najata_left_button)
            {
                najata_left_button = false;
                timer.Enabled = false; // отключаем на всякий случай таймер
                timer_value = 1;
                vozmojno_peretaskivanie = false;

                if (idet_peretaskivanie) // если идет перетаскивание
                {
                    navel_na_object.textBox.ForeColor = color_text_obichniy;
                    idet_peretaskivanie = false;
                    if (navel_na_object == aktivnaya_papka)
                    {
                        aktivnaya_papka.flpanel.BackColor = color_fona_aktivniy_obj;
                        aktivnaya_papka.textBox.BackColor = color_fona_aktivniy_obj;
                    }
                    else
                    {
                        navel_na_object.flpanel.BackColor = color_fona_object;
                        navel_na_object.textBox.BackColor = color_fona_object;
                    }

                    navel_na_object.flpanel.Location = new Point(navel_na_object.flpanel.Location.X, (navel_na_object.mesto_v_spiske - 1) * navel_na_object.flpanel.Size.Height);
                }
                else // кликнули по объекту 
                {
                    foreach (PanelNavigatii obj in list_object) // делаем папку активной
                    {
                        if (obj.textBox == ((TextBox)sender))
                        {
                            if (obj.this_papka)
                            {
                                if (obj != aktivnaya_papka) // папка не активная 
                                {
                                    est_aktivnaya_papka = true;
                                    aktivnaya_papka.flpanel.BackColor = color_fona_object;
                                    aktivnaya_papka.textBox.BackColor = color_fona_object;
                                    textBox.ForeColor = color_text_obichniy; // цвет текста
                                    aktivnaya_papka = obj;
                                    aktivnaya_papka.flpanel.BackColor = color_fona_aktivniy_obj;
                                    aktivnaya_papka.textBox.BackColor = color_fona_aktivniy_obj;

                                    if (obj.kolonka == 2)
                                    {
                                        panel_left.Controls.Clear(); // очищаем левую колонку                
                                        list_object_left.Clear();// очищаем лист с левой колонки
                                        foreach (PanelNavigatii papka in list_object_right)
                                        {
                                            papka.kolonka = 1;
                                            panel_left.Controls.Add(papka.flpanel);
                                            list_object_left.Add(papka);
                                        }
                                    }
                                    panel_right.Controls.Clear(); // очищаем правую колонку
                                    list_object_right.Clear(); // очищаем лист с правой колонки

                                    foreach (PanelNavigatii papka in aktivnaya_papka.papki_potomki) // добавляем дочерние папки
                                    {
                                        papka.kolonka = 2;
                                        panel_right.Controls.Add(papka.flpanel); // добавляем в правую колонку 
                                        list_object_right.Add(papka);    // добавляем в лист, ответчстввенный за правую колонку
                                    }
                                    papka_praroditel_objey_programmi_id = aktivnaya_papka.papka_roditel;


                                    if (papka_praroditel_objey_programmi_id != 0) // если не 0, то сделать активной
                                    {
                                        pictureBoxBack.Image = Image.FromFile(@"image\nazad_open.png");
                                        mojno_vernutsa_nazad = true;
                                    }
                                    else
                                    {
                                        pictureBoxBack.Image = Image.FromFile(@"image\nazad_close.png");
                                        mojno_vernutsa_nazad = false;
                                    }
                                }
                                else // нужно переименовать 
                                {
                                    if (textBox.ReadOnly)
                                    {
                                        timer.Enabled = true;
                                        mouse_x = Cursor.Position.X;
                                        mouse_y = Cursor.Position.Y;
                                    }
                                }
                            }
                            else // это лист 
                            {
                                if (obj != aktivniy_list) // лист не активный 
                                {
                                    if (est_asktivniy_list)
                                    {
                                        // нужно сохранить старый лист 
                                        aktivniy_list.flpanel.BackColor = color_fona_object;
                                        aktivniy_list.textBox.BackColor = color_fona_object;
                                        aktivniy_list.textBox.ForeColor = color_text_obichniy; // цвет текста

                                        LocalDB db1 = new();
                                        list_vivoda.SaveFile("save.rtf");
                                        string text_save = File.ReadAllText("save.rtf");
                                        db1.save_list(db1, aktivniy_list.id, text_save);
                                        File.Delete("save.rtf");
                                    }
                                    est_asktivniy_list = true;
                                    aktivniy_list = obj;
                                    aktivniy_list.flpanel.BackColor = color_fona_aktivniy_obj;
                                    aktivniy_list.textBox.BackColor = color_fona_aktivniy_obj;


                                    LocalDB db = new();
                                    string text_open = db.vivod_text_lista(db, aktivniy_list.id);
                                    if (text_open != String.Empty)
                                    {
                                        File.WriteAllText("open_file.rtf", text_open);
                                        list_vivoda.LoadFile("open_file.rtf");
                                    }
                                    else
                                    {
                                        list_vivoda.Text = "";
                                    }

                                    File.Delete("open_file.rtf");

                                }
                                else // нужно переименовать 
                                {
                                    if (textBox.ReadOnly)
                                    {
                                        timer.Enabled = true;
                                        mouse_x = Cursor.Position.X;
                                        mouse_y = Cursor.Position.Y;
                                    }
                                }
                            }
                            break;
                        }
                    }
                }
            }
        }
        private void navedenie_na_object_MouseMove(object sender, EventArgs e)          // курсор на объекте  / ПЕРЕТАСКИВАНИЕ                      
        {

            if (vozmojno_peretaskivanie) // идет перетаскивание
            {
                int y = Cursor.Position.Y - mouse_y;
                navel_na_object.flpanel.Location = new Point(navel_na_object.flpanel.Location.X, textBox_y + y);
                peretaskivanie_v_kolonke(navel_na_object.this_papka);
                Peretaskivanie_mejdy_kolonkami(navel_na_object.this_papka);

            }
            else // делаем фон "при наведении на папку"
            {
                foreach (PanelNavigatii obj in list_object)
                {
                    if ((TextBox)sender == aktivnaya_papka.textBox || ((TextBox)sender) == aktivniy_list.textBox)
                    {
                        break; // чтобы лишних действий не делать
                    }
                    if (obj.textBox == ((TextBox)sender))
                    {
                        navel_na_object = obj;
                        navel_na_object.flpanel.BackColor = color_fona_navel_na_object;
                        navel_na_object.textBox.BackColor = color_fona_navel_na_object;
                        break;
                    }
                }
            }
        }
        private void peretaskivanie_v_kolonke(bool eto_papka)                                                                                       
        {
            List<PanelNavigatii> list = navel_na_object.kolonka == 1 ? list_object_left : list_object_right;

            foreach(PanelNavigatii obj in list)
            {
                if (obj != navel_na_object && 
                    navel_na_object.flpanel.Location.Y >= obj.flpanel.Location.Y - 7 && 
                    navel_na_object.flpanel.Location.Y <= obj.flpanel.Location.Y + 7)
                {

                    int help = obj.mesto_v_spiske;
                    obj.mesto_v_spiske = navel_na_object.mesto_v_spiske;
                    navel_na_object.mesto_v_spiske = help;

                    LocalDB db = new();
                    db.perestavlenie_v_kolonke(db, eto_papka,           navel_na_object.id,     navel_na_object.mesto_v_spiske);
                    db.perestavlenie_v_kolonke(db, obj.this_papka,      obj.id,                 obj.mesto_v_spiske);

                    obj.flpanel.Location = new Point(obj.flpanel.Location.X, (obj.mesto_v_spiske - 1) * obj.flpanel.Size.Height);
                    break;
                }
            }
        }
        private void Peretaskivanie_mejdy_kolonkami(bool eto_papka)                                                                                 
        {
            bool nujen_perenos;
            List<PanelNavigatii> list;
            List<PanelNavigatii> new_list;
            Panel pn;
            if (kolonka == 1)
            {                
                nujen_perenos = Cursor.Position.X - form.Location.X > panel_left.Width + 49;
                list = list_object_left;
                new_list = list_object_right;
                pn = panel_right;
            }
            else
            {
                nujen_perenos = Cursor.Position.X - form.Location.X < panel_left.Width + 49;
                list = list_object_right;
                new_list = list_object_left;
                pn = panel_left;
            }
            ////////////////
            if (nujen_perenos
                && list.Contains(navel_na_object) 
                && navel_na_object != aktivnaya_papka
                && est_aktivnaya_papka)
            {
                foreach (PanelNavigatii pap in list) // поправление всех папок в прошлой колонке которые ниже
                {
                    if (pap.mesto_v_spiske > navel_na_object.mesto_v_spiske)
                    {
                        pap.flpanel.Location = new Point(pap.flpanel.Location.X, pap.flpanel.Location.Y - pap.flpanel.Size.Height);
                        pap.mesto_v_spiske--;
                    }
                }



                pn.Controls.Add(navel_na_object.flpanel); // добавление на новую панель
                list.Remove(navel_na_object); // убираем из прошлой коолонки
                new_list.Add(navel_na_object); // добавляем в новую колонку
                navel_na_object.kolonka = navel_na_object.kolonka == 1 ? 2 : 1; // показываем, в какой теперь колонке
                navel_na_object.mesto_v_spiske = new_list.Count;
                navel_na_object.flpanel.Location = new Point(navel_na_object.flpanel.Location.X, navel_na_object.mesto_v_spiske * navel_na_object.flpanel.Size.Width);

                if (pn == panel_right)   // если добавляем из левой колонки в правую
                {
                    if (aktivnaya_papka.papka_roditel != 0)
                    {
                        foreach (PanelNavigatii pap in list_object)
                        {
                            if (pap.id == aktivnaya_papka.papka_roditel) // удаляем из предыдущей папки перенесенного потомка
                            {
                                pap.papki_potomki.Remove(navel_na_object);
                            }
                        }
                    }

                    aktivnaya_papka.papki_potomki.Add(navel_na_object);
                    navel_na_object.papka_roditel = aktivnaya_papka.id;
                }
                else
                {
                    aktivnaya_papka.papki_potomki.Remove(navel_na_object);// удаляем из потомка
                    navel_na_object.papka_roditel = aktivnaya_papka.papka_roditel;
                }
                LocalDB db = new();
                if (eto_papka)
                {
                    db.Peretaskivanie_mejdy_kolonkami_papki(db, navel_na_object.papka_roditel, navel_na_object.id, navel_na_object.id, navel_na_object.mesto_v_spiske);
                }
                else
                {
                    db.Peretaskivanie_mejdy_kolonkami_list(db, navel_na_object.papka_roditel, navel_na_object.id, navel_na_object.mesto_v_spiske);
                }
            }
        }
        private void ubral_cursor_s_object_MouseLeave(object sender, EventArgs e)       // покинул папку (не кликая)                                
        {
            if (navel_na_object != aktivnaya_papka && navel_na_object != aktivniy_list)
            {
                navel_na_object.flpanel.BackColor = color_fona_object;
                navel_na_object.textBox.BackColor = color_fona_object;
            }
        }
        private void timer_tick(object sender, EventArgs e)                             // таймер перетаскивание или переименовать                  
        {
            timer_value++;

            if ((mouse_y != Cursor.Position.Y || timer_value == 20) && vozmojno_peretaskivanie) // если идет перетаскивание 
            {
                timer.Enabled = false;
                idet_peretaskivanie = true;
                timer_value = 1;
                navel_na_object.flpanel.BackColor = color_fona_peretaskivaniya;
                navel_na_object.textBox.BackColor = color_fona_peretaskivaniya;
                //navel_na_object.textBox.ForeColor = color_text_rename;
            }
            else // идет переименование 
            {
                if (mouse_x == Cursor.Position.X && mouse_y == Cursor.Position.Y && timer_value == 20)
                {
                    rename_func();
                }
            }
        }
        private void rename_func()                                                                                                                  
        {
            pereimenovanie = textBox.Text;
            timer.Enabled = false;
            timer_value = 1;
            textBox.ReadOnly = false; // разрешается ввод
            textBox.Focus();
            textBox.SelectionStart = 0;
            textBox.SelectionLength = textBox.Text.Length; // выделяется весь текст
            textBox.BackColor = color_fona_rename;  // цвет фона
            textBox.ForeColor = color_text_rename;          // цвет текста
            bilo_rename = true; // для бд

        }
        private void pokinul_obj_Leave(object sender, EventArgs e)                      // переименовал и покинул текстбокс                         
        {
            if (bilo_rename) 
            {
                if (textBox.Text == String.Empty) // имя стало пустым
                {
                    textBox.Text = pereimenovanie;
                }
                else // у нас нормальное имя
                {
                    LocalDB db = new();
                    db.rename(db, this_papka, id, textBox.Text);
                }
                bilo_rename = false;
            }
            textBox.BackColor = textBox.Parent.BackColor;
            textBox.ForeColor = color_text_obichniy;
            textBox.SelectionStart = 0;
            textBox.ReadOnly = true; // запрет на ввод
        }
        private void najali_enter_KeyDown(object sender, KeyEventArgs e)                // при вводе TextBox нажали Enter (pokinul_obj_Leave)       
        {
            // Проверям нажата ли именно клавиша Enter
            if (e.KeyCode == Keys.Enter) 
            {
                if (bilo_rename)
                {
                    if (textBox.Text == String.Empty)
                    {
                        textBox.Text = pereimenovanie;
                    }
                    else
                    {
                            LocalDB db = new();
                            db.rename(db,this_papka, id, textBox.Text);                        
                    }
                    bilo_rename = false;
                }
                textBox.BackColor = textBox.Parent.BackColor;
                textBox.ForeColor = color_text_obichniy;
                textBox.SelectionStart = 0;
                textBox.ReadOnly = true; // запрет на ввод
                textBox.Parent.Focus();
            }
        }        
        private void contextMenuSozdanie(ContextMenuStrip cms)
        {
            ToolStripMenuItem deleteMenuItem = new("Удалить");
            ToolStripMenuItem renameMenuItem = new("Переименовать");

            cms.Items.AddRange(new[]
            {
                deleteMenuItem,
                renameMenuItem
            });
            renameMenuItem.Click += rename_Menu;
            deleteMenuItem.Click += Delete_Menu;
        }
        private void rename_Menu(object sender, EventArgs e)
        {
            rename_func();
        }
        private void Delete_Menu(object sender, EventArgs e)
        {
            foreach (PanelNavigatii obj in list_object)
            {
                if (obj.textBox == contextMS.SourceControl)
                {
                    List<PanelNavigatii> list = obj.kolonka == 1 ? list_object_left : list_object_right;

                    foreach (PanelNavigatii obj2 in list) // поправление всех элементов в прошлой колонке которые ниже
                    {
                        if (obj2.mesto_v_spiske > obj.mesto_v_spiske)
                        {
                            obj2.flpanel.Location = new Point(obj2.flpanel.Location.X, obj2.flpanel.Location.Y - obj2.flpanel.Size.Height);
                            obj2.mesto_v_spiske--;
                            LocalDB db = new();
                            db.perestavlenie_v_kolonke(db, obj2.this_papka, obj2.id, obj2.mesto_v_spiske);
                        }
                    }

                    foreach (PanelNavigatii papka in list) // удаление из родителя ссылку на объект
                    {
                        if (papka.id == obj.papka_roditel && papka.this_papka)
                        {
                            papka.papki_potomki.Remove(obj);
                            break;
                        }
                    }
                    Delete_rekursiya(obj);                    
                    break;
                }
            }
        }
        private void Delete_rekursiya(PanelNavigatii obj)
        {
            if (obj.this_papka)
            {
                if (obj.papki_potomki.Count > 0) // есть подпапки
                {
                    foreach (PanelNavigatii obj_doch in obj.papki_potomki)
                    {
                        Delete_rekursiya(obj_doch);
                    }
                }
            }
            LocalDB db = new();
            db.delete_obj(db, obj.this_papka, obj.id);

            if (list_object_left.Contains(obj))
            {
                list_object_left.Remove(obj);
                panel_left.Controls.Remove(obj.flpanel);
                if (list_object_left.Count == 0)
                {
                    vernutsa_nazad();
                }
            }

            if (list_object_right.Contains(obj))
            {
                list_object_right.Remove(obj);
                panel_right.Controls.Remove(obj.flpanel);
            }

            list_object.Remove(obj);

            obj = null;

        }
    }
}