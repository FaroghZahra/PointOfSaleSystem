using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using DTO;
namespace DAL
{
    public class SalesDAL
    {
        public void Save(SalesDTO sales)
        {
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Assignment1;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            //insert Query in Sale DB
            string query = "insert into Sale values (@id, @cid, @d, @s)";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlParameter p1 = new SqlParameter("id", sales.ID);
            SqlParameter p2 = new SqlParameter("cid", sales.CustomerID);
            SqlParameter p3 = new SqlParameter("d", sales.Date);
            SqlParameter p4 = new SqlParameter("s", sales.status);
            cmd.Parameters.Add(p1);
            cmd.Parameters.Add(p2);
            cmd.Parameters.Add(p3);
            cmd.Parameters.Add(p4);

            cmd.ExecuteNonQuery();

            con.Close();
        }

        public void saveitem(int lineid, int orderid, int itemID, int quantity, int total)
        {
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Assignment1;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            //insert in SaleLineItem DBTable
            string query = "insert into SaleLineItem values (@lid, @oid, @iid, @q, @t)";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlParameter p1 = new SqlParameter("lid", lineid);
            SqlParameter p2 = new SqlParameter("oid", orderid);
            SqlParameter p3 = new SqlParameter("iid", itemID);
            SqlParameter p4 = new SqlParameter("q", quantity);
            SqlParameter p5 = new SqlParameter("t", total);
            cmd.Parameters.Add(p1);
            cmd.Parameters.Add(p2);
            cmd.Parameters.Add(p3);
            cmd.Parameters.Add(p4);
            cmd.Parameters.Add(p5);
            cmd.ExecuteNonQuery();

            con.Close();
        }

        public SalesDTO GetSales(int orderid)
        {
            SalesDTO sale = new SalesDTO();
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Assignment1;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            //select columns with given orderid
            string query = "select * From Sale Where OrderId = '" + orderid + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    sale.ID = (int)dr[0];
                    sale.CustomerID = (int)dr[1];
                    sale.Date = (DateTime)dr[2];
                    sale.status = (string)dr[3];
                   
                }
            }
            else
            {
                sale.ID = -1;
            }
            con.Close();
            return sale;
        }
        public List<SaleLineItemDTO> GetItems(int orderid)
        {
            List<SaleLineItemDTO> foundItems = new List<SaleLineItemDTO>();
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Assignment1;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            //select columns with given orderid
            string query = "SELECT * FROM SaleLineItem WHERE OrderId = '"+orderid+"'";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    //saving it in list
                    SaleLineItemDTO newitem = new SaleLineItemDTO();
                    newitem.LineNo = (int)dr[0];
                    newitem.OrderID = (int)dr[1];
                    newitem.ItemID = (int)dr[2];
                    newitem.Quantity = (int)dr[3];
                    newitem.Amount = (int)dr[4];
                    foundItems.Add(newitem);
                }
            }
            con.Close();
            return foundItems;
        }
        public int CheckID(int id)
        {
            //checking if itemID exists
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Assignment1;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            int count = -1;
            string query = "SELECT * FROM SaleLineItem WHERE ItemId = '" + id + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader dr = cmd.ExecuteReader();
            
            if (dr.HasRows)
                count = 1;
            con.Close();
            return count;
        }
        public int RemoveItem(int id)
        {
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Assignment1;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();

            string query = "DELETE FROM SaleLineItem WHERE ItemId = '" + id + "';";
            SqlCommand cmd = new SqlCommand(query, con);
            int count = cmd.ExecuteNonQuery();

            con.Close();
            return count;
        }
        public void CancelSale(int orderid)
        {
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Assignment1;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();

            string query = "DELETE FROM Sale WHERE OrderId = '" + orderid + "';";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();

            string query2 = "DELETE FROM SaleLineItem WHERE OrderId = '" + orderid + "';";
            SqlCommand cmd2 = new SqlCommand(query2, con);
            cmd2.ExecuteNonQuery();

            con.Close();
        }
        public int CheckSaleExists(int id)
        {
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Assignment1;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            int count = -1;
            string query = "Select * FROM Sale WHERE OrderId = '" + id + "';";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
                count = 1;

            con.Close();
            return count;
        }

        public void SaveReciept(int rnd, DateTime date, int id, int paid)
        {
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Assignment1;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();

            string query = "insert into Receipt values (@rn, @rd, @oid, @a)";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlParameter p1 = new SqlParameter("rn", rnd);
            SqlParameter p2 = new SqlParameter("rd", date);
            SqlParameter p3 = new SqlParameter("oid", id);
            SqlParameter p4 = new SqlParameter("a", paid);
            
            cmd.Parameters.Add(p1);
            cmd.Parameters.Add(p2);
            cmd.Parameters.Add(p3);
            cmd.Parameters.Add(p4);
            
            cmd.ExecuteNonQuery();

            con.Close();
        }
    }
}
