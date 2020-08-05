using System;
using Microsoft.Data.Sqlite;
using System.IO;

namespace DBConection
{
    class Conection
    {
        string query = null;
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
                Console.WriteLine("base de dados carregado com sucesso...");
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
                Console.WriteLine(" id: " + result["id"] +  " serviço: " + result["serviço"] + " usuario: " + result["usuario"] + " senha: " + result["senha"]);
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
    }
}