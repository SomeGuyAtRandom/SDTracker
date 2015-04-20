using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADOTransfer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Are you sure you want to run this program? (Y/N)");
            Console.WriteLine("Any other key to exit");
            Console.WriteLine("");
            var checkKey = Console.ReadKey();
            if (checkKey.KeyChar.ToString().ToLower() != "y")
            {
                return;
            }

            Console.WriteLine("");
            Console.WriteLine("Do you want to transfer project from MySql to MSSql (Y/N)");
            Console.WriteLine("WARNING: This action delete the current projects existing in the MSSql database.");
            checkKey = Console.ReadKey();

            TransferTable table;
            if (checkKey.KeyChar.ToString().ToLower() == "y")
            {
                Console.WriteLine("");
                table = new TransferTable();
                table.sdn_main_projects();

            }

            Console.WriteLine("");
            Console.WriteLine("Do you want to transfer Places(id= 5-digit code) from MySql to MSSql (Y/N)");
            Console.WriteLine("WARNING: This action delete the current Places existing in the MSSql database.");
            checkKey = Console.ReadKey();

            if (checkKey.KeyChar.ToString().ToLower() == "y")
            {
                Console.WriteLine("");
                table = new TransferTable();
                table.populatePlaces();

            }

            Console.WriteLine("");
            Console.WriteLine("-Finished-");
            Console.WriteLine("Hit any key to exit.");
            Console.ReadKey();
        }
    }
}
