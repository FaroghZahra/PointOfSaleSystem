using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL;
using DTO;
namespace PL
{
    class SalesPL
    {
        public void SalesMenu()
        {
            //generating Sales id
            Random rnd = new Random();
            int id = rnd.Next(1, 35000);
            Console.WriteLine("Sales: " + id);
            DateTime date = DateTime.Now;
            Console.WriteLine("Sales Date: " + date);
            Console.WriteLine("Enter Customer ID:");
            string customerid = Console.ReadLine();
            CustomerBLL custdisplay = new CustomerBLL();
            CustomerDTO cust = custdisplay.DisplayCustomer(int.Parse(customerid));
            //if customer exists in customer database
            if (cust.ID != -1)
            {
                SalesBLL SaveSale = new SalesBLL();
                SalesDTO saledto = new SalesDTO { ID = id, CustomerID = int.Parse(customerid), Date = date, status = "incomplete" };
                SaveSale.Save(saledto);

                int previous = EnterNewItem(0, id);
                SmallMenu(previous, id);
            }
            //if customer does not exists in customer database
            else
            {
                Console.WriteLine("Enter Valid Customer ID");
            }
        }

        public void SmallMenu(int previous, int orderid)
        {
            int option = -1;
            //maintaing total amount
            int pre = previous;
            do
            {
                Console.WriteLine("Press 1 to Enter New Item");
                Console.WriteLine("Press 2 to End Sale");
                Console.WriteLine("Press 3 to Remove an existing Item from the current sale");
                Console.WriteLine("Press 4 to Cancel Sale");
                Console.WriteLine("Choose from option 1 – 4:");
                option = int.Parse(Console.ReadLine());

                if (option == 1)
                    pre = EnterNewItem(pre, orderid);
                else if (option == 3)
                    RemoveItem();
                else if (option == 4) {
                    CancelSale(orderid);
                    break;
                }    
            } while (option != 2);
            if(option == 2)
            {
                EndSale(orderid);
            }
        }
        public int EnterNewItem(int pre, int orderid)
        {
            int totalpayable = pre;
            Console.WriteLine("Enter Item ID:");
            string itemid = Console.ReadLine();

            ItemBLL itemdisplay = new ItemBLL();
            ItemDTO item = itemdisplay.DisplayItem(int.Parse(itemid));
            //if item id exist
            if (item.ID != -1)
            {
                Console.WriteLine("Description: " + item.Description);
                Console.WriteLine("Price: " + item.Price);
                Console.WriteLine("Enter Quantity: ");
                string quantity = Console.ReadLine();
                SalesBLL sales = new SalesBLL();
                int total = sales.calculateTotal(item.Price, int.Parse(quantity));
                Console.WriteLine("Sub-Total:" + total);
                totalpayable = sales.totalpayable(pre , total);
                Console.WriteLine("Total Amount Payable:" + totalpayable);

                SalesBLL newbll = new SalesBLL();
                Random rnd = new Random();
                int lineid = rnd.Next(1, 35000);
                newbll.saveitem(lineid, orderid, item.ID, int.Parse(quantity),total);
            }
            else
            {
                Console.WriteLine("ID not Found");
            }
            return totalpayable;
        }
        public void EndSale(int orderid)
        {
            //displaying info
            SalesBLL getSalesinfo = new SalesBLL();
            SalesDTO saleinfo = getSalesinfo.GetSale(orderid);
            Console.Write("Sales ID: "+ orderid);
            Console.WriteLine("\t\tCustomer ID: "+ saleinfo.CustomerID);
            Console.Write("Sales Date: " + saleinfo.Date);
            CustomerBLL custdisplay = new CustomerBLL();
            CustomerDTO cust = custdisplay.DisplayCustomer(saleinfo.CustomerID);
            Console.WriteLine("\t\tName: " + cust.Name);

            SalesBLL getItemInfos = new SalesBLL();
            List<SaleLineItemDTO> saleitems = getItemInfos.GetItems(orderid);

            Console.WriteLine("--------------------------------------------------------------");
            Console.WriteLine("ItemID        Description          Quantity         Amount");
            Console.WriteLine("---------------------------------------------------------------");
            int sum = 0;
            //display items bought in sales
            foreach (SaleLineItemDTO sitems in saleitems)
            {
                Console.Write(sitems.ItemID + "\t\t");
                ItemBLL itemdisplay = new ItemBLL();
                ItemDTO item = itemdisplay.DisplayItem(sitems.ItemID);
                Console.Write(item.Description + "\t");
                Console.Write(sitems.Quantity + "\t\t");
                Console.Write(sitems.Amount + "\t\t");
                Console.WriteLine("");
                //calculating total amount
                sum = sum + (sitems.Amount* sitems.Quantity);
            }
            //displaying total
            Console.WriteLine("--------------------------------------------------------------------");
            Console.WriteLine("\t\t\tTotal: "+  sum);
            Console.WriteLine("--------------------------------------------------------------------");

            int saleslimit = cust.SalesLimit;
            bool check = getSalesinfo.Check(saleslimit, sum);

            if (check == false)
                Console.WriteLine("Can not Make this sale, Limit Exceeding");
            else
            {
                cust.AmountPayable = sum;
                CustomerBLL modify = new CustomerBLL();
                int count = modify.ModifyCustomer(cust);
            }
            Console.WriteLine("Press any key to Continue");
            Console.ReadLine();

        }

        public void RemoveItem()
        {
            Console.WriteLine("Enter ItemID to Remove from Sale");
            int id = int.Parse(Console.ReadLine());
            SalesBLL remove = new SalesBLL();
            //checking if Sales Id exists
            int i = remove.CheckID(id);
            if(i == 1)
            {
                int count = remove.RemoveItem(id);
                Console.WriteLine("Item Removed from Sales List");
            }
            else
            {
                Console.WriteLine("Enter ItemId of item from Sales List");
            }
        }

        public void CancelSale(int salesid)
        {
            SalesBLL cancel = new SalesBLL();
            cancel.CancelSale(salesid);
            Console.WriteLine("sale Cancelled");
        }

        public void PaymentHandle()
        {

            Console.WriteLine("Enter Sales ID");
            int id = int.Parse(Console.ReadLine());
            SalesBLL check = new SalesBLL();
            //checking if sales id exist
            int i = check.CheckSaleExists(id);
            if(i == 1)
            {
                SalesBLL getSalesinfo = new SalesBLL();
                SalesDTO saleinfo = getSalesinfo.GetSale(id);
                CustomerBLL custdisplay = new CustomerBLL();
                //getting customer name
                CustomerDTO cust = custdisplay.DisplayCustomer(saleinfo.CustomerID);
                Console.WriteLine("Customer Name: " + cust.Name);

                SalesBLL getItemInfos = new SalesBLL();
                List<SaleLineItemDTO> saleitems = getItemInfos.GetItems(id);

                int sum = 0;
                foreach (SaleLineItemDTO sitems in saleitems)
                {
                    //calculating total
                    sum = sum + (sitems.Amount * sitems.Quantity);
                }
                Console.WriteLine("Total Sales Amount: " + sum);
                int amountpayable = cust.AmountPayable;
                Console.WriteLine("Amount Paid: " + (sum - amountpayable));
                Console.WriteLine("Remaining Amount: " + amountpayable);
                Console.WriteLine("Amount to be Paid: ");
                int paid = int.Parse(Console.ReadLine());
                cust.AmountPayable = amountpayable - paid;
                CustomerBLL modify = new CustomerBLL();
                int count = modify.ModifyCustomer(cust);

                Random rnd = new Random();
                int recieptid = rnd.Next(1, 35000);
                DateTime date = DateTime.Now;

                SalesBLL recieptbll = new SalesBLL();
                recieptbll.SaveReciept(recieptid, date, id, paid);
            }
            else
            {
                Console.WriteLine("Enter SalesId of Existing Sale ");
            }
        }
    }
}
