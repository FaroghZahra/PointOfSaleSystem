using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL;
using DTO;
namespace PL
{
    class ItemPL
    {
        public void ItemMenu()
        {
            string option = "";
            do
            {
                Console.WriteLine("1 - Add new Item");
                Console.WriteLine("2 - Update Item details");
                Console.WriteLine("3 - Find Items");
                Console.WriteLine("4 - Remove Existing Item");
                Console.WriteLine("5 - Back to Main Menu");
                Console.Write("Press 1 to 5 to select an option: ");
                option = Console.ReadLine();

                if (option == "1")
                    AddNewItem();
                else if (option == "2")
                    ModifyItem();
                else if (option == "3")
                    FindItems();
                else if (option == "4")
                    RemoveItem();

            } while (option != "5");
        }

        public void AddNewItem()
        {
            //generating ItemID
            Random rnd = new Random();
            int id = rnd.Next(1, 35000);
            Console.WriteLine("ID of Item: " + id);
            //taking input from user
            Console.WriteLine("Enter Description of item");
            string desc = Console.ReadLine();
            Console.WriteLine("Enter Price of Item");
            int price = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter Quantity of Item");
            int quant = int.Parse(Console.ReadLine());
            //confirming 
            Console.WriteLine("Press 1 to save info");
            string save = Console.ReadLine();
            if (save == "1")
            {
                //passing DTO to Business Logic Layer to Save Data
                ItemDTO item = new ItemDTO { ID = id, Description = desc, Price = price, Quantity = quant };
                ItemBLL bllitem = new ItemBLL();
                int count = bllitem.SaveItem(item);
                if (count == 1)
                    Console.WriteLine("Item Information successfully saved");
            }
        }

        public void ModifyItem()
        {
            Console.WriteLine("Enter Item ID to Modify");
            int ID = int.Parse(Console.ReadLine());
            ItemBLL display = new ItemBLL();
            ItemDTO item = display.DisplayItem(ID);
            if (item.ID == -1)
            {
                Console.WriteLine("ID Not Found");
            }
            else
            {
               
                Console.WriteLine("Item ID: " + ID);
                Console.WriteLine("Item Description: " + item.Description);
                Console.WriteLine("Item Price: " + item.Price);
                Console.WriteLine("Item Quantity: " + item.Quantity);
                Console.WriteLine("Item Creation item:" + item.date);

                Console.WriteLine("Enter Item Details to Modify, leave Blank otherwise");
                Console.WriteLine("Enter Description of item");
                string desc = Console.ReadLine();
                Console.WriteLine("Enter Price of Item");
                string price = Console.ReadLine();
                Console.WriteLine("Enter Quantity of Item");
                string quant = Console.ReadLine();
                Console.WriteLine("Press 1 to save info");
                string save = Console.ReadLine();
                if (save == "1")
                {
                    //handling null fields
                    if (desc != "")
                        item.Description = desc;
                    if (price != "")
                        item.Price = int.Parse(price);
                    if (quant != "")
                        item.Quantity = int.Parse(quant);

                    ItemBLL modify = new ItemBLL();
                    int count = modify.ModifyItem(item);
                    if (count == 1)
                        Console.WriteLine("Item Information successfully saved");
                }
            }
        }

        public void FindItems()
        {
            ItemDTO item = new ItemDTO();
            Console.WriteLine("Please specify atleast one of the following to find the item. Leave all fields blank to return to Items Menu: ");
            Console.WriteLine("Item ID: ");
            string id = Console.ReadLine();
            Console.WriteLine("Item Description: ");
            string desc = Console.ReadLine();
            Console.WriteLine("Item Price: ");
            string price = Console.ReadLine();
            Console.WriteLine("Item Quantity: ");
            string quant = Console.ReadLine();
            Console.WriteLine("Item Creation item: ");
            string date = Console.ReadLine();

            //handling null fields
            if (id == "")
                item.ID = -1;
            else
                item.ID = int.Parse(id);
            if (desc == "")
                item.Description = "";
            else
                item.Description = desc;
            if (price == "")
                item.Price = -1;
            else
                item.Price = int.Parse(price);
            if (quant == "")
                item.Quantity = -1;
            else
                item.Quantity = int.Parse(quant);
            if (date == "")
            {
                DateTime dt2 = new DateTime(2000, 01, 01);
                item.date = dt2;
            }
            else
            {
                item.date = DateTime.Parse(date);
            }
            //returning if all fields empty
            if (id == "" && desc == "" && price == "" && quant == "" && date == "") ;
            else
            {
                ItemBLL find = new ItemBLL();
                List<ItemDTO> foundItems = new List<ItemDTO>();
                foundItems = find.findItems(item);

                Console.WriteLine("------------------------------------");
                Console.WriteLine("ItemID        Description         Price        Quantity");
                Console.WriteLine("------------------------------------");
                foreach (ItemDTO items in foundItems)
                {
                    Console.Write(items.ID + "        ");
                    Console.Write(items.Description + "        ");
                    Console.Write(items.Price + "        ");
                    Console.Write(items.Quantity + "        ");
                    Console.WriteLine("");
                }
                Console.WriteLine("------------------------------------");
            }
        }

        public void RemoveItem()
        {

            Console.WriteLine("Enter Item ID to Remove");
            int ID = int.Parse(Console.ReadLine());
            ItemBLL display = new ItemBLL();
            ItemDTO item = display.DisplayItem(ID);
            if (item.ID == -1)
            {
                Console.WriteLine("ID Not Found");
            }
            else
            {
                Console.WriteLine("Press 1 to delete item");
                string save = Console.ReadLine();
                if (save == "1")
                {
                    ItemBLL delete = new ItemBLL();
                    int count = delete.DeleteItem(ID);
                    if (count == 1)
                        Console.WriteLine("Item Information successfully deleted");
                    else if (count == -1)
                        Console.WriteLine("No Recorded Sales, Can not Delete Item");
                }
            }
        }
    }
}
