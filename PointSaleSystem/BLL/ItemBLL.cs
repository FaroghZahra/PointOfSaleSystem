using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAL;
namespace BLL
{
    public class ItemBLL
    {
        public int SaveItem(ItemDTO item)
        {
            item.date = DateTime.Now;
            ItemDAL itemdal = new ItemDAL();
            int count = itemdal.AddItem(item);
            return count;
        }

        public ItemDTO DisplayItem(int ID)
        {
            ItemDAL display = new ItemDAL();
            ItemDTO item = display.Display(ID);
            return item;
        }

        public int ModifyItem(ItemDTO item)
        {
            ItemDAL modify = new ItemDAL();
            int count = modify.ModifyItem(item);
            return count;
        }
        public List<ItemDTO> findItems(ItemDTO item){
            ItemDAL find = new ItemDAL();
            List<ItemDTO> foundItems = new List<ItemDTO>();
            foundItems = find.FindItems(item);
            return foundItems;
        }
        public int DeleteItem(int ID)
        {
            ItemDAL delete = new ItemDAL();
            int count = delete.DeleteItem(ID);
            return count;
        }
    }
}
