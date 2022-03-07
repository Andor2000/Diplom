using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _4_этап
{
    class LocalDB
    {
        private  static string strConnect = "";
        SqlConnection connection = new(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = " + strConnect + "Integrated Security = True;");
        public void openBook()
        {
            pathDB();
            openBookPapki(0);
            openBookListi();
        }
        private void pathDB()
        {
            string path = Application.ExecutablePath;

            for (int i = 0; i < 4; i++)
            {
                while (path[path.Length - 1] != '\\')
                {
                    path = path.Remove(path.Length - 1, 1);
                }
                path = path.Remove(path.Length - 1, 1);

            }
            strConnect = path + @"\DataBase.mdf;";
        }
        private void openBookPapki(int id_roditel)
        {
            LocalDB db = new();
            SqlDataReader sqlReader = null;
            SqlCommand command = new("SELECT * FROM CONTENT WHERE ID_RODITEL=@id", db.getConnection()); // какие папки при загрузке
            command.Parameters.AddWithValue("id", id_roditel); // типа чтобы взломать было тяжелей
            try
            {
                db.closeConnection();
                db.openConnection();
                if (sqlReader != null)
                {
                    sqlReader.Close();
                }            
                List<int> list_id_potomki = new(); // запоминаем нужные id
                sqlReader = command.ExecuteReader(); // выполняем запрос
                while (sqlReader.Read())
                {
                    list_id_potomki.Add(Convert.ToInt32(sqlReader["id_potomok"])); // записываю id папок
                }
                sqlReader.Close();
                for (int i = 0; i < list_id_potomki.Count; i++)
                {
                    command = new("SELECT * FROM PAPKA WHERE ID_PAPKI=@id", db.getConnection());
                    command.Parameters.AddWithValue("id", list_id_potomki[i]); // типа чтобы взломать было тяжелей                
                    sqlReader = command.ExecuteReader(); // выполняем запрос

                    while (sqlReader.Read())
                    {
                        // нужно создать папку
                        int id_db = Convert.ToInt32(sqlReader["id_papki"]);
                        string name_db = Convert.ToString(sqlReader["name_papki"]);
                        int mesto_db = Convert.ToInt32(sqlReader["place_papki"]);

                        PanelNavigatii papka = new();
                        papka.sozdanie_papki_DB(papka, id_db, name_db, mesto_db, id_roditel);
                        
                        openBookPapki(id_db);
                    }
                    sqlReader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                db.closeConnection();
                sqlReader.Close();
            }
        }
        private void openBookListi()
        {
            LocalDB db = new();
            SqlDataReader sqlReader;
            SqlCommand command = new("SELECT * FROM LIST", db.getConnection());
            db.openConnection();
            sqlReader = command.ExecuteReader(); // выполняем запрос
            while (sqlReader.Read())
            {
                int id_db = Convert.ToInt32(sqlReader["id_lista"]);
                string name_db = Convert.ToString(sqlReader["name_lista"]);
                int mesto_db = Convert.ToInt32(sqlReader["place_lista"]);
                int roditel_db = Convert.ToInt32(sqlReader["roditel_lista"]);

                PanelNavigatii list = new();

                list.sozdanie_lista_DB(list, id_db, name_db, mesto_db, roditel_db);

            }
            db.closeConnection();
        }
        private void openConnection()
        {
            if (connection.State == System.Data.ConnectionState.Closed) // если бд закрытое (не подключены)
            {
                connection.Open(); // открываем бд
            }
        }
        private void closeConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open) // если бд открыто (подключены)
            {
                connection.Close(); // открываем бд
            }
        }
        private SqlConnection getConnection() // проверяем подключение 
        {
            return connection;
        }
        public static int id_new_papki(LocalDB db, String name_pap, int mesto_pap)
        {
            SqlCommand command = new("INSERT INTO PAPKA (name_papki,place_papki) VALUES (@name_pap, @mesto)", db.getConnection());

            command.Parameters.AddWithValue("name_pap", name_pap);
            command.Parameters.AddWithValue("mesto", mesto_pap); // типа чтобы взломать было тяжелей
            //////
            db.openConnection();
            command.ExecuteNonQuery(); // заполняем в бд 
            command = new("SELECT MAX(ID_PAPKI) FROM PAPKA", db.getConnection()); // какие папки при загрузке
            int id = Convert.ToInt32(command.ExecuteScalar());
            db.closeConnection();
            //////
            return id;
        }
        public static int id_new_lista(LocalDB db, String name_pap, int mesto_pap, int roditel)
        {
            SqlCommand command = new("INSERT INTO LIST (name_lista, place_lista, roditel_lista) VALUES (@name_pap, @mesto,@roditel)", db.getConnection());

            command.Parameters.AddWithValue("name_pap", name_pap);
            command.Parameters.AddWithValue("mesto", mesto_pap); // типа чтобы взломать было тяжелей
            command.Parameters.AddWithValue("roditel", roditel); // типа чтобы взломать было тяжелей
            //////
            db.openConnection();
            command.ExecuteNonQuery(); // заполняем в бд 
            command = new("SELECT MAX(ID_LISTA) FROM LIST", db.getConnection()); // какие папки при загрузке
            int id = Convert.ToInt32(command.ExecuteScalar());
            db.closeConnection();
            //////
            return id;
        }
        public async void new_content(LocalDB db, int id_potom, int id_rod)
        {
            SqlCommand command = new("INSERT INTO CONTENT (ID_roditel,ID_potomok) VALUES (@rod, @pot)", db.getConnection());
            command.Parameters.Add("@rod", SqlDbType.VarChar).Value = id_rod; // типа чтобы взломать было тяжелей
            command.Parameters.Add("@pot", SqlDbType.VarChar).Value = id_potom; // типа чтобы взломать было тяжелей

            db.openConnection();
            await command.ExecuteNonQueryAsync(); // заполняем в бд             
            db.closeConnection();
        }
        public void perestavlenie_v_kolonke(LocalDB db, bool is_papka, int navel_id, int navel_mesto)
        {
            SqlCommand command;
            if (is_papka)
            {
                command = new("UPDATE [PAPKA] SET [PLACE_PAPKI] = @mesto WHERE [ID_PAPKI] = @id", db.getConnection());
            }
            else
            {
                command = new("UPDATE [LIST] SET [PLACE_LISTA] = @mesto WHERE [PLACE_LISTA] = @id", db.getConnection());
            }

            command.Parameters.AddWithValue("id", navel_id); // типа чтобы взломать было тяжелей
            command.Parameters.AddWithValue("mesto", navel_mesto); // типа чтобы взломать было тяжелей

            db.openConnection();
            command.ExecuteNonQuery();
            db.closeConnection();
        }
        public async void Peretaskivanie_mejdy_kolonkami_papki(LocalDB db, int roditel, int id, int potomok, int mesto)
        {
            SqlCommand command = new("UPDATE [CONTENT] SET [ID_RODITEL] = @rod WHERE [ID_potomok] = @pot", db.getConnection());
            command.Parameters.AddWithValue("rod", roditel); // типа чтобы взломать было тяжелей
            command.Parameters.AddWithValue("pot", potomok); // типа чтобы взломать было тяжелей
            SqlCommand command2 = new("UPDATE [PAPKA] SET [PLACE_PAPKI] = @MESTO WHERE [ID_PAPKI] = @id", db.getConnection());
            command2.Parameters.AddWithValue("id", id); // типа чтобы взломать было тяжелей
            command2.Parameters.AddWithValue("mesto", mesto); // типа чтобы взломать было тяжелей

            db.openConnection();
            await command.ExecuteNonQueryAsync();
            await command2.ExecuteNonQueryAsync();
            db.closeConnection();
        }
        public async void Peretaskivanie_mejdy_kolonkami_list(LocalDB db, int roditel, int id, int mesto)
        {
            SqlCommand command = new("UPDATE [LIST] SET [RODITEL_LISTA] = @rod, [PLACE_LISTA] = @mesto WHERE [ID_LISTA] = @ID", db.getConnection());
            command.Parameters.AddWithValue("rod", roditel); // типа чтобы взломать было тяжелей
            command.Parameters.AddWithValue("id", id); // типа чтобы взломать было тяжелей
            command.Parameters.AddWithValue("mesto", mesto); // типа чтобы взломать было тяжелей
            db.openConnection();
            await command.ExecuteNonQueryAsync();
            db.closeConnection();
        }
        public async void rename(LocalDB db, bool this_papka, int id, string name)
        {
            SqlCommand command;
            if (this_papka)
            {
                command = new("UPDATE [PAPKA] SET [NAME_PAPKI] = @name WHERE [ID_PAPKI] = @id", db.getConnection());
            }
            else
            {
                command = new("UPDATE [LIST] SET [NAME_LISTA] = @name WHERE [ID_LISTA] = @id", db.getConnection());
            }
            command.Parameters.AddWithValue("id", id); // типа чтобы взломать было тяжелей
            command.Parameters.AddWithValue("name", name); // типа чтобы взломать было тяжелей
            db.openConnection();
            await command.ExecuteNonQueryAsync();
            db.closeConnection();
        }
        public string vivod_text_lista(LocalDB db, int id_lista)
        {
            SqlCommand command;
            command = new("SELECT TEXT FROM LIST WHERE ID_LISTA=@id", db.getConnection()); // какие папки при загрузке
            command.Parameters.AddWithValue("id", id_lista); // типа чтобы взломать было тяжелей

            string text = "";
            db.openConnection();
            SqlDataReader sqlReader = command.ExecuteReader(); // выполняем запрос
            while (sqlReader.Read())
            {
                text = Convert.ToString(sqlReader["text"]);
            }
            sqlReader.Close();
            db.closeConnection();

            return text;
        }
        public void save_list(LocalDB db, int id_lista, string text)
        {
            SqlCommand command = new("UPDATE LIST SET TEXT = @text WHERE ID_LISTA = @id", db.getConnection());
            command.Parameters.AddWithValue("id", id_lista); // типа чтобы взломать было тяжелей
            command.Parameters.AddWithValue("text", text); // типа чтобы взломать было тяжелей
            
            db.openConnection();
            command.ExecuteNonQuery();
            db.closeConnection();
        }
    }
}
           