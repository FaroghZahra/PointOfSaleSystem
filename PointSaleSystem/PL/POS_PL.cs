using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
namespace PL
{
    public class POS_PL
    {
        public void MainMenu() {
            string option = "";
            do
            {
                Console.WriteLine("1 - Manage Items");
                Console.WriteLine("2 - Manage Customers");
                Console.WriteLine("3 - Make New Sale");
                Console.WriteLine("4 - Make Payment");
                Console.WriteLine("5 - Exit");
                Console.Write("Press 1 to 5 to select an option: ");
                option = Console.ReadLine();

                if (option == "1")
                {
                    ItemPL itemMenu = new ItemPL();
                    itemMenu.ItemMenu();
                }

                else if (option == "2")
                {
                    CustomerPL cusMenu = new CustomerPL();
                    cusMenu.CustomerMenu();
                }
                else if (option == "3")
                {
                    SalesPL sales = new SalesPL();
                    sales.SalesMenu();
                }
                else if (option == "4")
                {
                    SalesPL payment = new SalesPL();
                    payment.PaymentHandle();
                }
            } while(option != "5");
        }

        
    }    
}

