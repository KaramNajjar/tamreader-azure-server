using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.Azure;
using System.Configuration;

namespace ContactList.App_Start
{
    public class ServerLogic
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
            // Retrieve a reference to the table.
            CloudTable table = GetTableClient();

            table.Delete();
        }

        public static void CreateTable()
        {
            // Retrieve a reference to the table.
            CloudTable table = GetTableClient();

            // Create the table if it doesn't exist.
            table.CreateIfNotExists();
        }

    }
}