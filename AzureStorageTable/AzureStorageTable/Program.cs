using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure;
using System.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace AzureStorageTable
{
    class Program
    {
       
            public static void CreateNewTable(CloudTable table)
            {
                if (!table.CreateIfNotExists())
                {
                    Console.WriteLine("table {0} is already exists", table.Name);
                    return;
                }
                Console.WriteLine("table {0} is created", table.Name);
            }
            public static void InsertRecordToTable(CloudTable table)
            {
                Console.WriteLine("Enter customer type");
                string customerType = Console.ReadLine();
                Console.WriteLine("Enter customer ID");
                string customerID = Console.ReadLine();
                Console.WriteLine("Enter customer name");
                string customerName = Console.ReadLine();
                Console.WriteLine("Enter customer details");
                string customerDetails = Console.ReadLine();
                Customer customerentity = new Customer();
                customerentity.CustomerType = customerType;
                customerentity.CustomerId = Int32.Parse(customerID);
                customerentity.CustomerName = customerName;
                customerentity.CustomerDetails = customerDetails;
                customerentity.AssignPartitionKey();
                customerentity.AssignRowKey();
                Customer custentity = RetrieveRecord(table, customerType, customerID);
                if (custentity == null)
                {
                    TableOperation tableOperation = TableOperation.Insert(customerentity);
                    table.Execute(tableOperation);
                    Console.WriteLine("Record is inserted");
                }
                else
                {
                    Console.WriteLine("Record is already exist");
                }
            }
            public static Customer RetrieveRecord(CloudTable table, string partitionKey, string rowKey)
            {
                TableOperation tableOperation = TableOperation.Retrieve<Customer>(partitionKey, rowKey);
                TableResult tableResult = table.Execute(tableOperation);
                return tableResult.Result as Customer;

            }
            public static void UpdateRecordInTable(CloudTable table)
            {
                Console.WriteLine("Enter customer type");
                string customerType = Console.ReadLine();
                Console.WriteLine("Enter customer ID");
                string customerID = Console.ReadLine();
                Console.WriteLine("Enter customer name");
                string customerName = Console.ReadLine();
                Console.WriteLine("Enter customer details");
                string customerDetails = Console.ReadLine();
                Customer customerEntity = RetrieveRecord(table, customerType, customerID);
                if (customerEntity != null)
                {
                    customerEntity.CustomerDetails = customerDetails;
                    customerEntity.CustomerName = customerName;
                    TableOperation tableOperation = TableOperation.Replace(customerEntity);
                    table.Execute(tableOperation);
                    Console.WriteLine("Record updated");
                }
                else
                {
                    Console.WriteLine("Record does not exists");
                }
            }
            public static void DisplayTableRecords(CloudTable table)
            {
                TableQuery<Customer> tableQuery = new TableQuery<Customer>();
                foreach (Customer customerEntity in table.ExecuteQuery(tableQuery))
                {
                    Console.WriteLine("Customer ID : {0}", customerEntity.CustomerId);
                    Console.WriteLine("Customer Type : {0}", customerEntity.CustomerType);
                    Console.WriteLine("Customer Name : {0}", customerEntity.CustomerName);
                    Console.WriteLine("Customer Details : {0}", customerEntity.CustomerDetails);
                    Console.WriteLine("******************************");
                }
            }
            public static void DeleteRecordinTable(CloudTable table)
            {
                Console.WriteLine("Enter customer type");
                string customerType = Console.ReadLine();
                Console.WriteLine("Enter customer ID");
                string customerID = Console.ReadLine();
                Customer customerEntity = RetrieveRecord(table, customerType, customerID);
                if (customerEntity != null)
                {
                    TableOperation tableOperation = TableOperation.Delete(customerEntity);
                    table.Execute(tableOperation);
                    Console.WriteLine("Record deleted");
                }
                else
                {
                    Console.WriteLine("Record does not exists");
                }
            }
            public static void DropTable(CloudTable table)
            {
                if (!table.DeleteIfExists())
                {
                    Console.WriteLine("Table does not exists");
                }
            }
        static void Main(string[] args)
        {
            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["StorageConnectionString"]);
            CloudTableClient tableClient = cloudStorageAccount.CreateCloudTableClient();
            Console.WriteLine("Enter the table name to create");
            string tablename = Console.ReadLine();
            CloudTable cloudTable = tableClient.GetTableReference(tablename);
            CreateNewTable(cloudTable);
            InsertRecordToTable(cloudTable);
            UpdateRecordInTable(cloudTable);
            DisplayTableRecords(cloudTable);
            DeleteRecordinTable(cloudTable);
            Console.ReadLine();
        }
    }
    }
