using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL;
using DTO;
namespace PL
{
    class CustomerPL
    {
        public void CustomerMenu()
        {
            string option = "";
            do
            {
                Console.WriteLine("1 - Add new Customer");
                Console.WriteLine("2 - Update Customer details");
                Console.WriteLine("3 - Find Customer");
                Console.WriteLine("4 - Remove Existing Customer");
                Console.WriteLine("5 - Back to Main Menu");
                Console.Write("Press 1 to 5 to select an option: ");
                option = Console.ReadLine();

                if (option == "1")
                    AddNewCustomer();
                else if (option == "2")
                    ModifyCustomer();
                else if (option == "3")
                    FindCustomer();
                else if (option == "4")
                    RemoveCustomer();
            } while (option != "5");
        }

        public void AddNewCustomer()
        {
            //generating CustomerID
            Random rnd = new Random();
            int id = rnd.Next(1, 35000);
            Console.WriteLine("ID of Customer: " + id);
            //taking input from user
            Console.WriteLine("Enter Name of Customer");
            string name = Console.ReadLine();
            Console.WriteLine("Enter Address of Customer");
            string address = Console.ReadLine();
            Console.WriteLine("Enter Phone number of Customer");
            string phone = Console.ReadLine();
            Console.WriteLine("Enter email of Customer");
            string email = Console.ReadLine();
            Console.WriteLine("Enter SalesLimit of Customer");
            int saleslimit = int.Parse(Console.ReadLine());
            //confirming 
            Console.WriteLine("Press 1 to save info");
            string save = Console.ReadLine();
            if (save == "1")
            {
                //passing DTO to Business Logic Layer to Save Data
                CustomerDTO customer = new CustomerDTO { ID = id, Name = name, Address = address, Phone = phone, Email = email, SalesLimit = saleslimit, AmountPayable = 0 };
                CustomerBLL bllcustomer = new CustomerBLL();
                int count = bllcustomer.SaveCustomer(customer);
                if (count == 1)
                    Console.WriteLine("Customer Information successfully saved");
            }
        }

        public void ModifyCustomer()
        {
            Console.WriteLine("Enter Customer ID to Modify");
            int ID = int.Parse(Console.ReadLine());
            CustomerBLL display = new CustomerBLL();
            CustomerDTO customer = display.DisplayCustomer(ID);

            if (customer.ID == -1)
            {
                Console.WriteLine("ID Not Found");
            }
            else
            {
                Console.WriteLine("Customer ID: " + ID);
                Console.WriteLine("Customer Name: " + customer.Name);
                Console.WriteLine("Customer Address: " + customer.Address);
                Console.WriteLine("Customer Phone: " + customer.Phone);
                Console.WriteLine("Customer Email: " + customer.Email);
                Console.WriteLine("Customer Sales Limit: " + customer.SalesLimit);
                Console.WriteLine("Amount Payable by Customer: " + customer.AmountPayable);

                Console.WriteLine("Enter Customer Details to Modify, leave Blank otherwise");
                Console.WriteLine("Enter Name of Customer");
                string name = Console.ReadLine();
                Console.WriteLine("Enter Address of Customer");
                string address = Console.ReadLine();
                Console.WriteLine("Enter Phone number of Customer");
                string phone = Console.ReadLine();
                Console.WriteLine("Enter email of Customer");
                string email = Console.ReadLine();
                Console.WriteLine("Enter SalesLimit of Customer");
                string saleslimit = Console.ReadLine();
                Console.WriteLine("Press 1 to save info");
                string save = Console.ReadLine();
                if (save == "1")
                {
                    //handling null fields
                    if (name != "")
                        customer.Name = name;
                    if (address != "")
                        customer.Address = address;
                    if (phone != "")
                        customer.Phone = phone;
                    if (email != "")
                        customer.Email = email;
                    if (saleslimit != "")
                        customer.SalesLimit = int.Parse(saleslimit);
                    CustomerBLL modify = new CustomerBLL();
                    int count = modify.ModifyCustomer(customer);
                    if (count == 1)
                        Console.WriteLine("Customer Information successfully saved");
                }
            }
        }

        public void FindCustomer()
        {
            CustomerDTO cust = new CustomerDTO();
            Console.WriteLine("Please specify atleast one of the following to find the Customer. Leave all fields blank to return to Customers Menu: ");
            Console.WriteLine("Enter Customer ID");
            string id = Console.ReadLine();
            Console.WriteLine("Enter Name of Customer");
            string name = Console.ReadLine();
            Console.WriteLine("Enter Address of Customer");
            string address = Console.ReadLine();
            Console.WriteLine("Enter Phone number of Customer");
            string phone = Console.ReadLine();
            Console.WriteLine("Enter email of Customer");
            string email = Console.ReadLine();
            Console.WriteLine("Enter SalesLimit of Customer");
            string saleslimit = Console.ReadLine();
            //handling null fields
            if (id == "")
                cust.ID = -1;
            else
                cust.ID = int.Parse(id);
            if (name == "")
                cust.Name = "";
            else
                cust.Name = name;
            if (address == "")
                cust.Address = "";
            else
                cust.Address = address;
            if (phone == "")
                cust.Phone = "";
            else
                cust.Phone = phone;
            if (email == "")
                cust.Email = "";
            else
                cust.Email = email;
            if (saleslimit == "")
                cust.SalesLimit = -1;
            else
                cust.SalesLimit = int.Parse(saleslimit);
            //returning if all fields empty
            if (id == "" && name == "" && address == "" && phone == "" && email == "" && saleslimit == "") ;
            else
            {
                CustomerBLL find = new CustomerBLL();
                List<CustomerDTO> foundCustomers = new List<CustomerDTO>();
                foundCustomers = find.findCustomer(cust);

                Console.WriteLine("------------------------------------");
                Console.Write(format: "{0, -8} {1, -8} {2, -8}", "CustomerID", "Name", "Email");
                Console.Write(format: "{0,-8} {1,-8}", "Phone", "Sales Limit");
                Console.WriteLine("");
                Console.WriteLine("------------------------------------");
                foreach (CustomerDTO customers in foundCustomers)
                {
                    Console.Write(format: "{0, -8} {1, -8} {2, -8}", arg0: customers.ID.ToString(), customers.Name, customers.Email);
                    Console.Write(format: "{0,-8} {1,-8}", customers.Phone, customers.SalesLimit);
                    Console.WriteLine("");
                }
                Console.WriteLine("------------------------------------");
            }
        }

        public void RemoveCustomer()
        {

            Console.WriteLine("Enter Customer ID to Remove");
            int ID = int.Parse(Console.ReadLine());
            CustomerBLL display = new CustomerBLL();
            CustomerDTO customer = display.DisplayCustomer(ID);
            if (customer.ID == -1)
            {
                Console.WriteLine("ID Not Found");
            }
            else
            {
                Console.WriteLine("Press 1 to delete item");
                string save = Console.ReadLine();
                if (save == "1")
                {
                    CustomerBLL delete = new CustomerBLL();
                    int count = delete.DeleteCustomer(ID);
                    if (count == 1)
                        Console.WriteLine("Customer Information successfully deleted");
                    else if(count == -1)
                        Console.WriteLine("Customer Associated with Sales, Can not Delete");
                }
            }
        }
    }
}
