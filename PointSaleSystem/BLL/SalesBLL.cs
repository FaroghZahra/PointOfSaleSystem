using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAL;
namespace BLL
{
    public class SalesBLL
    {
        public int calculateTotal( int price, int quantity)
        {
            return (price * quantity);
        }
        public int totalpayable(int pre, int now)
        {
            return pre + now;
        }
        public void Save(SalesDTO sales)
        {
            SalesDAL salesdal = new SalesDAL();
            salesdal.Save(sales);
        }
        public void saveitem(int lineid,int  orderid,int itemID, int quantity,int total)
        {
            SalesDAL sales = new SalesDAL();
            sales.saveitem(lineid, orderid, itemID, quantity, total);
        }

        public SalesDTO GetSale(int orderid)
        {
            SalesDAL getsalesinfo = new SalesDAL();
            SalesDTO sale = getsalesinfo.GetSales(orderid);
            return sale;
        }

        public List<SaleLineItemDTO> GetItems(int orderid)
        {
            SalesDAL find = new SalesDAL();
            List<SaleLineItemDTO> foundItems = new List<SaleLineItemDTO>();
            foundItems = find.GetItems(orderid);
            return foundItems;
        }
        public bool Check(int saleslimit, int total)
        {
            if (saleslimit > total)
                return true;
            return false;
        }
        public int CheckID(int id)
        {
            SalesDAL checkId = new SalesDAL();
            int count = checkId.CheckID(id);
            return count;
        }
        public int RemoveItem(int id)
        {
            SalesDAL checkId = new SalesDAL();
            int count = checkId.RemoveItem(id);
            return count;
        }
        public void CancelSale(int orderid)
        {
            SalesDAL cancel = new SalesDAL();
            cancel.CancelSale(orderid);
        }
        public int CheckSaleExists(int id)
        {
            SalesDAL check = new SalesDAL();
            int count = check.CheckSaleExists(id);
            return count;
        }
        public void SaveReciept(int rnd, DateTime date,int id, int paid)
        {
            SalesDAL rec = new SalesDAL();
            rec.SaveReciept(rnd, date, id, paid);
        }
    }
}
