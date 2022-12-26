using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAL;
namespace BLL
{
    public class CustomerBLL
    {
        public int SaveCustomer(CustomerDTO customer)
        {
            CustomerDAL customerdal = new CustomerDAL();
            int count = customerdal.AddCustomer(customer);
            return count;
        }

        public CustomerDTO DisplayCustomer(int ID)
        {
            CustomerDAL display = new CustomerDAL();
            CustomerDTO cust = display.Display(ID);
            return cust;
        }

        public int ModifyCustomer(CustomerDTO customer)
        {
            CustomerDAL modify = new CustomerDAL();
            int count = modify.ModifyCustomer(customer);
            return count;
        }
        public List<CustomerDTO> findCustomer(CustomerDTO cust)
        {
            CustomerDAL find = new CustomerDAL();
            List<CustomerDTO> foundCustomers = new List<CustomerDTO>();
            foundCustomers = find.FindCustomer(cust);
            return foundCustomers;
        }
        public int DeleteCustomer(int ID)
        {
            CustomerDAL delete = new CustomerDAL();
            int count = delete.DeleteCustomer(ID);
            return count;
        }
    }
}
