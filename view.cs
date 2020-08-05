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
                Console.WriteLine("\n data number itens: " + a +"\n");

                Console.WriteLine("1 - Novo Login_Senha | 2 - Editar | 3 - Alterar | 4 - excluir | 5 - Exportar para TXT");
                Console.WriteLine();

                key = Console.ReadKey(true).KeyChar;
                Console.WriteLine(key); 

                Console.Clear();
                switch (key)
                {
                    case '1': 
                        novo_Login_Senha();
                    break;

                    case '2':
                        
                    break;

                    case '3': 
                        
                    break;

                    case '4':
                        deletar(); 
                    break;

                    default: Console.WriteLine("opcao invalida");
                    break;
                }
            }
        }

        public void novo_Login_Senha()
        {
            char key;
            String login = null;
            String senha = null;
            String servico = null;

            Console.Write("login: "); login = Console.ReadLine();      
            Console.Write("senha: "); senha = Console.ReadLine();
            Console.Write("serviço: "); servico = Console.ReadLine();

            Console.WriteLine();
            Console.WriteLine("Login: " + login + "\nsenha: " + senha);
            Console.WriteLine();

            Console.WriteLine("Salvar ? Y[OK] N[Descartar]");
            key = Console.ReadKey(true).KeyChar;

            if(key == 'Y' || key == 'y')
            {
                Console.WriteLine("Salvo com sucesso");
                start_db.insertAllDB(login, senha, servico);
            }
            else
            {
                Console.WriteLine("Descartado");
            }

        }

        public void deletar()
        {
            int id;
            Console.Write("Id para ser deletado: "); id = Convert.ToInt32(Console.ReadLine());
            bool ok = start_db.selectOneItem(id); // mostrar item  a ser deletado

            start_db.delete(id, ok); // deletar
            Console.ReadKey();
        }

    }
}