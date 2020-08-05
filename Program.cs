using System;
using Microsoft.Data.Sqlite;
using DBConection;
using ViewMenu;

namespace keyapp
{
    class Program
    {
        static void Main(string[] args)
        {
            var menu = new Principal();
            menu.MenuPrinpal();
        }
    }
}
