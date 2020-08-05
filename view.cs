using System;
using DBConection;

namespace ViewMenu
{
    class Principal
    {
        char key;
        Conection start_db = new Conection();
        public void MenuPrinpal()
        {   
        
            while(true)
            {
                Console.WriteLine("lendo database...");
                Console.Clear();

                int a = start_db.readAllDB("pass_log"); //mostrar todos os dados
                Console.WriteLine("\nNumero de itens da tabela pass_log: " + a +"\n");

                Console.WriteLine("1 - Novo Login_Senha | 3 - Alterar | 4 - excluir | 5 - Exportar para TXT |");
                Console.WriteLine();

                key = Console.ReadKey(true).KeyChar;
                //Console.WriteLine(key); 

                //Console.Clear();
                switch (key)
                {
                    case '1': 
                        Novo_Login_Senha();
                    break;

                    case '3': 
                        Editar();
                    break;

                    case '4':
                        Deletar(); 
                    break;

                    default: Console.WriteLine("opcao invalida");
                    break;
                }
            }
        }

        public void Novo_Login_Senha()
        {
            char key;
            String login = null;
            String senha = null;
            String servico = null;

            Console.Write("Login: "); login = Console.ReadLine();      
            Console.Write("Senha: "); senha = Console.ReadLine();
            Console.Write("Serviço: "); servico = Console.ReadLine();

            Console.WriteLine("-----------------------------------");

            Console.WriteLine();
            Console.WriteLine("Login: " + login + "\nSenha: " + senha + "\nServiço: " + servico);
            Console.WriteLine();

            Console.Write("Salvar ? S (OK) N (Descartar): ");
            key = Console.ReadKey(true).KeyChar;

            if(key == 'S' || key == 's')
            {
                start_db.insertAllDB(login, senha, servico);
                Console.WriteLine("Salvo !!"); Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Descartado");
            }
        }

        public void Editar()
        {
            int id;
            Console.Write("Id para ser editar: "); id = Convert.ToInt32(Console.ReadLine());
            bool ok = start_db.selectOneItem(id);

            if(ok)
            {
                char key;
                String login = null;
                String senha = null;
                String servico = null;

                Console.Write("Login: "); login = Console.ReadLine();      
                Console.Write("Senha: "); senha = Console.ReadLine();
                Console.Write("Serviço: "); servico = Console.ReadLine();
                
                Console.WriteLine("-----------------------------------");

                Console.WriteLine();
                Console.WriteLine("Login: " + login + "\nSenha: " + senha + "\nServiço: " + servico);
                Console.WriteLine();

                Console.Write("Salvar ? S (OK) N (Descartar): ");
                key = Console.ReadKey(true).KeyChar;

                if(key == 'S' || key == 's')
                {
                    start_db.edit(id, login, senha, servico);
                }
                else
                {
                    Console.WriteLine("Descartado");
                }
            }
            
        }
        public void Deletar()
        {
            int id;
            Console.Write("Id para ser deletado: "); id = Convert.ToInt32(Console.ReadLine());
            bool ok = start_db.selectOneItem(id); // mostrar item  a ser deletado

            start_db.delete(id, ok); // deletar
            Console.ReadKey();
        }

    }
}