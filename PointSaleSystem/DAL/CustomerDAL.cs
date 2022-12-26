using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using DTO;
namespace DAL
{
    public class CustomerDAL
    {
        public int AddCustomer(CustomerDTO customer)
        {
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Assignment1;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();

            string query = "insert into Customer values (@id, @n, @a, @p, @e, @sl, @ap)";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlParameter p1 = new SqlParameter("id", customer.ID);
            SqlParameter p2 = new SqlParameter("n", customer.Name);
            SqlParameter p3 = new SqlParameter("a", customer.Address);
            SqlParameter p4 = new SqlParameter("p", customer.Phone);
            SqlParameter p5 = new SqlParameter("e", customer.Email);
            SqlParameter p6 = new SqlParameter("sl", customer.SalesLimit);
            SqlParameter p7 = new SqlParameter("ap", customer.AmountPayable);
            cmd.Parameters.Add(p1);
            cmd.Parameters.Add(p2);
            cmd.Parameters.Add(p3);
            cmd.Parameters.Add(p4);
            cmd.Parameters.Add(p5);
            cmd.Parameters.Add(p6);
            cmd.Parameters.Add(p7);
            int count = cmd.ExecuteNonQuery();

            con.Close();
            return count;
        }

        public CustomerDTO Display(int id)
        {
            CustomerDTO cust = new CustomerDTO();
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Assignment1;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();

            string query = "select * From Customer Where CustomerId = '" + id + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                //saving value in CustomerDTO
                while (dr.Read())
                {
                    cust.ID = (int)dr[0];
                    cust.Name = (string)dr[1];
                    cust.Address = (string)dr[2];
                    cust.Phone = (string)dr[3];
                    cust.Email = (string)dr[4];
                    cust.SalesLimit = (int)dr[5];
                    cust.AmountPayable = (int)dr[6];
                }
            }
            else
            {
                cust.ID = -1;
            }
            return cust;
        }

        public int ModifyCustomer(CustomerDTO customer)
        {
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Assignment1;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            //updating customer
            string query = "UPDATE Customer SET CustomerId = @id, Name = @n, Address = @a, Phone = @p, Email = @e, SalesLimit = @sl, AmountPayable = @ap WHERE CustomerId = @i;";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlParameter p1 = new SqlParameter("id", customer.ID);
            SqlParameter p2 = new SqlParameter("n", customer.Name);
            SqlParameter p3 = new SqlParameter("a", customer.Address);
            SqlParameter p4 = new SqlParameter("p", customer.Phone);
            SqlParameter p5 = new SqlParameter("e", customer.Email);
            SqlParameter p6 = new SqlParameter("sl", customer.SalesLimit);
            SqlParameter p7 = new SqlParameter("ap", customer.AmountPayable);
            SqlParameter p8 = new SqlParameter("i", customer.ID);
            cmd.Parameters.Add(p1);
            cmd.Parameters.Add(p2);
            cmd.Parameters.Add(p3);
            cmd.Parameters.Add(p4);
            cmd.Parameters.Add(p5);
            cmd.Parameters.Add(p6);
            cmd.Parameters.Add(p7);
            cmd.Parameters.Add(p8);
            int count = cmd.ExecuteNonQuery();

            con.Close();
            return count;
        }

        public List<CustomerDTO> FindCustomer(CustomerDTO customer)
        {
            List<CustomerDTO> foundCustomers = new List<CustomerDTO>();
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Assignment1;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            string query = "SELECT * FROM Customer WHERE CustomerId = @i OR Name = @n OR Address = @a OR Phone = @p OR Email = @e OR SalesLimit = @sl";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlParameter p1 = new SqlParameter("i", customer.ID);
            SqlParameter p2 = new SqlParameter("n", customer.Name);
            SqlParameter p3 = new SqlParameter("a", customer.Address);
            SqlParameter p4 = new SqlParameter("p", customer.Phone);
            SqlParameter p5 = new SqlParameter("e", customer.Email);
            SqlParameter p6 = new SqlParameter("sl", customer.SalesLimit);
            cmd.Parameters.Add(p1);
            cmd.Parameters.Add(p2);
            cmd.Parameters.Add(p3);
            cmd.Parameters.Add(p4);
            cmd.Parameters.Add(p5);
            cmd.Parameters.Add(p6);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                //saving all customers of find result in list
                while (dr.Read())
                {
                    CustomerDTO newCustomer = new CustomerDTO();
                    newCustomer.ID = (int)dr[0];
                    newCustomer.Name = (string)dr[1];
                    newCustomer.Email = (string)dr[4];
                    newCustomer.Phone = (string)dr[3];
                    newCustomer.SalesLimit = (int)dr[5];
                    foundCustomers.Add(newCustomer);
                }
            }
            return foundCustomers;
        }

        public int DeleteCustomer(int ID)
        {
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Assignment1;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            int count = -1;
            //checking if customer in salesList
            string query1 = "Select * From Sale Where CustomerId = '" + ID + "';";
            SqlCommand cmd = new SqlCommand(query1, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                count = -1;
            }
            else {
                //delete customer
                string query = "DELETE FROM Customer WHERE CustomerId = '" + ID + "';";
                SqlCommand cmd2 = new SqlCommand(query, con);
                count = cmd2.ExecuteNonQuery();
            }

            con.Close();
            return count;
        }
    }
}
