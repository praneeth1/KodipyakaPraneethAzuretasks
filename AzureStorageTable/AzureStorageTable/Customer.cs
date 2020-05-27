using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;
namespace AzureStorageTable
{
    class Customer : TableEntity
    {
        private int customerId;
        private string customerName;
        private string customerDetails;
        private string customerType;
        public void AssignPartitionKey()
        {
            this.PartitionKey = customerType;
        }
        public void AssignRowKey()
        {
            this.RowKey = customerId.ToString();
        }
        public int CustomerId
        {
            get { return customerId; }
            set { customerId = value; }
        }
        public string CustomerName
        {
            get { return customerName; }
            set { customerName = value; }
        }
        public string CustomerDetails
        {
            get { return customerDetails; }
            set { customerDetails = value; }
        }
        public string CustomerType
        {
            get { return customerType; }
            set { customerType = value; }
        }
    }
}

