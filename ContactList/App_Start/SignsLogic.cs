using ContactList.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace ContactList.App_Start
{
    public class SignsLogic
    {
        private static CloudTable GetTableClient()
        {
            // Parse the connection string and return a reference to the storage account.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationManager.ConnectionStrings["StorageConnectionString"].ConnectionString);
            // Create the table client.
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            // Retrieve a reference to the table.
            CloudTable table = tableClient.GetTableReference("Signs");
            table.CreateIfNotExists();

            return table;
        }

        public static void DeleteTable()
        {
            // Parse the connection string and return a reference to the storage account.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationManager.ConnectionStrings["StorageConnectionString"].ConnectionString);
            // Create the table client.
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            // Retrieve a reference to the table.
            CloudTable table = tableClient.GetTableReference("Signs");

            table.Delete();
        }

        public static void CreateSignsTable()
        {
            // Retrieve a reference to the table.
            CloudTable table = GetTableClient();

            // Create the table if it doesn't exist.
            table.CreateIfNotExists();
        }

        public static List<Sign> GetSigns()
        {
            List<Sign> results = new List<Sign>();
            // Retrieve a reference to the table.
            CloudTable table = GetTableClient();

            // Construct the query operation for all customer entities where PartitionKey="Smith".
            TableQuery<SignEntity> query = new TableQuery<SignEntity>();

            // Print the fields for each customer.
            foreach (SignEntity entity in table.ExecuteQuery(query))
            {
                results.Add(new Sign { Name = entity.Name, ObeyTimes = entity.ObeyTimes, DisobeyTimes = entity.DisobeyTimes });
            }

            return results;
        }
        public static void AddSign(Sign s)
        {
            // Create the CloudTable object that represents the "people" table.
            CloudTable table = GetTableClient();

            // Create a new sign entity.
            SignEntity sign = new SignEntity(s.Name, s.ObeyTimes, s.DisobeyTimes);

            // Create the TableOperation object that inserts the customer entity.
            TableOperation insertOperation = TableOperation.InsertOrReplace(sign);

            //return sign.Name + " " + sign.ObeyTimes + " " + sign.DisobeyTimes;
            // Execute the insert operation.

            table.Execute(insertOperation);
            
        }

    }
}