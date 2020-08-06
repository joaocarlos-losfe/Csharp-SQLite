using System;
using Microsoft.Data.Sqlite;
using System.IO;

namespace DBConection
{
    class Conection
    {
        string query = null;
        string key_init_aplication;
        string key = "";
        public void StartDB()
        {
            if(!File.Exists("db\\db.db"))
            {
                Console.WriteLine("erro ao carregar o database...");
            }
            else
            {
                var startDB = new SqliteConnection("Data Source = db\\db.db;");
                startDB.Open();

                if(!File.Exists("db\\config.dat"))
                {
                    File.Create("db\\config.dat");
                }
                else
                {
                    key_init_aplication = System.IO.File.ReadAllText("db\\config.dat");

                    while( key.Equals(key_init_aplication) == false )
                    {
                        Console.Clear();
                        Console.Write("key init: "); key = Console.ReadLine();

                        if(key.Equals(key_init_aplication))
                        {
                            Console.WriteLine("ok...");
                        }
                    }

                }
            }
        }

        public void insertAllDB(string usuario, string senha, string servico)
        {
            var startDB = new SqliteConnection("Data Source = db\\db.db;");
            startDB.Open();

            query = "INSERT INTO pass_log (usuario, senha, serviço) values (" + "'" + usuario + "'" + "," + "'" + senha + "'" + "," + "'" + servico + "'" + ")";
            
            var command = new SqliteCommand(query, startDB);
            command.ExecuteNonQuery();
            Console.WriteLine("database save...");

            startDB.Close();
        }

        public int readAllDB(string table)
        {
            
            var startDB = new SqliteConnection("Data Source = db\\db.db;");
            startDB.Open();

            query = "Select * from " + table;
            var command = new SqliteCommand(query, startDB); 

            SqliteDataReader result = command.ExecuteReader();

            int n = 0;
            while(result.Read())
            {
                Console.WriteLine(" id > " + result["id"] +  " | serviço: " + result["serviço"] + " | usuario: " + result["usuario"] + " | senha: " + result["senha"]);
                Console.WriteLine("_____________________________________________________________________________________________________________");
                n++;
            }

            startDB.Close();

            return n;
        }

        public bool selectOneItem(int id) // exibir os itens encontrados
        {
            bool ok = false;
            var startDB = new SqliteConnection("Data Source = db\\db.db;");
            startDB.Open();

            query = "SELECT id, usuario, senha, serviço FROM pass_log WHERE id = " + id;

            var command = new SqliteCommand(query, startDB);
             

            SqliteDataReader result = command.ExecuteReader();


            if(result.Read())
            {
                while(result.Read())
                {
                    Console.WriteLine("serviço: " + result["serviço"] + " usuario: " + result["usuario"] + " senha: " + result["senha"]);
                }
                ok = true;
            }
            else
            {
                Console.WriteLine("item não encontrado no registro....");
            }

            startDB.Close();

            return ok;
        }

        public void delete(int id, bool existe_na_tabela)
        {
            var startDB = new SqliteConnection("Data Source = db\\db.db;");
            startDB.Open();

            query = "DELETE FROM pass_log WHERE id = " + id;
            var command = new SqliteCommand(query, startDB); 

            command.ExecuteNonQuery();

            if(existe_na_tabela)
            {
                Console.WriteLine("deletado com sucesso...");
            }
            
            startDB.Close();
        }

        public void edit(int id, String login, String senha, String servico)
        {
            var startDB = new SqliteConnection("Data Source = db\\db.db;");
            startDB.Open();

            query = "UPDATE pass_log SET usuario = " + "'" + login + "'" + "," + "senha = " + "'" + senha + "'" + "," + "serviço = " + "'" + servico + "'" + "WHERE id = " + id;
            var command = new SqliteCommand(query, startDB); 

            command.ExecuteNonQuery();

            Console.WriteLine("Alterado com sucesso...");
            startDB.Close();
        }

        public void export_to_txt(int id, bool all)
        {
            if(all == true)
            {
                
                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop); // obter o caminho da pasta desktop
                
                var startDB = new SqliteConnection("Data Source = db\\db.db;");
                startDB.Open();

                query = "Select * from pass_log";
                var command = new SqliteCommand(query, startDB); 
                SqliteDataReader result = command.ExecuteReader();

                string finalPath = desktopPath + "\\keyapp" + DateTime.Now.ToString("D") + ".txt";

                using (StreamWriter file = new StreamWriter(@finalPath))
                {
                    while(result.Read())
                    {
                        file.WriteLine("serviço: " + result["serviço"] + " usuario: " + result["usuario"] + " senha: " + result["senha"]);
                    }
                }
                
                Console.WriteLine("Exportado com sucesso para > " + finalPath + " <");
                Console.ReadKey();

                System.Diagnostics.Process.Start("notepad", finalPath); //abrindo o app com o caminho do arquivo
                startDB.Close();
            }
            else
            {

            }
        }
    }
}