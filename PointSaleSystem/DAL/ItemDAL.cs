using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using DTO;

namespace DAL
{
    public class ItemDAL
    {
        public int AddItem(ItemDTO item)
        {
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Assignment1;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();

            string query = "insert into Item values (@id, @d, @p, @q, @ct)";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlParameter p1 = new SqlParameter("id", item.ID);
            SqlParameter p2 = new SqlParameter("d", item.Description);
            SqlParameter p3 = new SqlParameter("p", item.Price);
            SqlParameter p4 = new SqlParameter("q", item.Quantity);
            SqlParameter p5 = new SqlParameter("ct", item.date);
            cmd.Parameters.Add(p1);
            cmd.Parameters.Add(p2);
            cmd.Parameters.Add(p3);
            cmd.Parameters.Add(p4);
            cmd.Parameters.Add(p5);
            int count = cmd.ExecuteNonQuery();

            con.Close();
            return count;
        }

        public ItemDTO Display(int id)
        {
            ItemDTO item = new ItemDTO();
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Assignment1;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();

            string query = "select * From Item Where ItemId = '" + id + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                //saving value in itemDTO
                while (dr.Read()) {
                    item.ID = (int)dr[0];
                    item.Description = (string)dr[1];
                    item.Price = (int)dr[2];
                    item.Quantity = (int)dr[3];
                    item.date = (DateTime)dr[4];
                }
            }
            else
            {
                item.ID = -1;
            }
            return item;
        }

        public int ModifyItem(ItemDTO item)
        {
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Assignment1;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            //updating item
            string query = "UPDATE Item SET ItemId = @id, Description = @d, Price = @p, Quantity = @q, CreationDate = @cd WHERE ItemId = @i;";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlParameter p1 = new SqlParameter("id", item.ID);
            SqlParameter p2 = new SqlParameter("d", item.Description);
            SqlParameter p3 = new SqlParameter("p", item.Price);
            SqlParameter p4 = new SqlParameter("q", item.Quantity);
            SqlParameter p5 = new SqlParameter("cd", item.date);
            SqlParameter p6 = new SqlParameter("i", item.ID);
            cmd.Parameters.Add(p1);
            cmd.Parameters.Add(p2);
            cmd.Parameters.Add(p3);
            cmd.Parameters.Add(p4);
            cmd.Parameters.Add(p5);
            cmd.Parameters.Add(p6);
            int count = cmd.ExecuteNonQuery();

            con.Close();
            return count;
        }

        public List<ItemDTO> FindItems(ItemDTO item)
        {
            List<ItemDTO> foundItems = new List<ItemDTO>();
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Assignment1;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            string query = "SELECT * FROM Item WHERE ItemId = @i OR Description = @d OR Price = @p OR Quantity = @q OR CreationDate = @c";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlParameter p1 = new SqlParameter("i", item.ID);
            SqlParameter p2 = new SqlParameter("d", item.Description);
            SqlParameter p3 = new SqlParameter("p", item.Price);
            SqlParameter p4 = new SqlParameter("q", item.Quantity);
            SqlParameter p5 = new SqlParameter("c", item.date);
            cmd.Parameters.Add(p1);
            cmd.Parameters.Add(p2);
            cmd.Parameters.Add(p3);
            cmd.Parameters.Add(p4);
            cmd.Parameters.Add(p5);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    //saving all items of find result in list
                    ItemDTO newitem = new ItemDTO();
                    newitem.ID = (int)dr[0];
                    newitem.Description = (string)dr[1];
                    newitem.Price = (int)dr[2];
                    newitem.Quantity = (int)dr[3];
                    newitem.date = (DateTime)dr[4];
                    foundItems.Add(newitem);
                }
            }
                return foundItems;
        }

        public int DeleteItem(int ID)
        {
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Assignment1;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            int count = -1;
            //checking if some sale has been made
            string query1 = "Select * From SaleLineItem Where ItemId = '" + ID + "';";
            SqlCommand cmd = new SqlCommand(query1, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                //delete item
                string query = "DELETE FROM Item WHERE ItemId = '" + ID + "';";
                SqlCommand cmd2 = new SqlCommand(query, con);
                count = cmd2.ExecuteNonQuery();
                
            }
            else
            {
                count = -1;
            }
            con.Close();
            return count;
        }
    }
}
