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
            Console.WriteLine("Entry point for ADO transfer");
            TransferTable table = new TransferTable();
            table.sdn_main_projects();
            
            table.populatePlaces();

            Console.WriteLine("-Finished-");
            Console.ReadKey();
        }
    }
}
