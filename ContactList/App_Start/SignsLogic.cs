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
        private static CloudTable GetTableClient(string tableName)
        {
            // Parse the connection string and return a reference to the storage account.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationManager.ConnectionStrings["StorageConnectionString"].ConnectionString);
            // Create the table client.
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            // Retrieve a reference to the table.
            CloudTable table = tableClient.GetTableReference(tableName);
            table.CreateIfNotExists();

            return table;
        }

        public static void DeleteTable(string tableName)
        {
            // Parse the connection string and return a reference to the storage account.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationManager.ConnectionStrings["StorageConnectionString"].ConnectionString);
            // Create the table client.
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            // Retrieve a reference to the table.
            CloudTable table = tableClient.GetTableReference(tableName);

            table.Delete();
        }

        public static void CreateSignsTable(string tableName)
        {
            // Retrieve a reference to the table.
            CloudTable table = GetTableClient(tableName);

            // Create the table if it doesn't exist.
            table.CreateIfNotExists();
        }

        public static List<Sign> GetSigns()
        {
            List<Sign> results = new List<Sign>();
            // Retrieve a reference to the table.
            CloudTable table = GetTableClient("Signs");

            // Construct the query operation for all customer entities where PartitionKey="Smith".
            TableQuery<SignEntity> query = new TableQuery<SignEntity>();
            
            // Print the fields for each customer.
            foreach (SignEntity entity in table.ExecuteQuery(query))
            {
                results.Add(new Sign { Name = entity.Name, ObeyTimes = entity.ObeyTimes, DisobeyTimes = entity.DisobeyTimes });
            }

            return results;
        }

        public static UserEntity GetUserSigns(int userId)
        {
            List<Sign> results = new List<Sign>();
            // Retrieve a reference to the table.
            CloudTable table = GetTableClient("Users");

            // Construct the query operation for all customer entities where PartitionKey="Smith".
            TableQuery<UserEntity> query = new TableQuery<UserEntity>();

            // Print the fields for each customer.
            foreach (UserEntity entity in table.ExecuteQuery(query))
            {
                if (entity.Id == userId) {
                    return new UserEntity(entity.Id,entity.StopDisobey,entity.EntryDisobey,entity.SpeedLowDisobey,entity.SpeedHighDisobey);
                }
            }

            return null;
        }

        public static string AddUserInfo(userSign usign)
        {

            // Create the CloudTable object that represents the "signs" table.
            CloudTable table = GetTableClient("Users");
            // Create a new user entity.
            Sign s = usign.Us;
            UserEntity uEntity = GetUserSigns(usign.Id);
            UserEntity newUE = uEntity;

            if (uEntity == null)
            {
                uEntity = new UserEntity(usign.Id,0,0,0,0);
            }


                switch (usign.Us.Name)
                {

                    case "stop":
                        newUE = new UserEntity(usign.Id, usign.Us.DisobeyTimes,
                            uEntity.EntryDisobey, uEntity.SpeedLowDisobey, uEntity.SpeedHighDisobey);
                        break;

                    case "speedLow":
                        newUE = new UserEntity(usign.Id, uEntity.StopDisobey,
                            uEntity.EntryDisobey, usign.Us.DisobeyTimes, uEntity.SpeedHighDisobey);
                        break;

                    case "speedHigh":
                        newUE = new UserEntity(usign.Id, uEntity.StopDisobey,
                            uEntity.EntryDisobey, uEntity.SpeedLowDisobey, usign.Us.DisobeyTimes);
                        break;

                    case "noEntry":
                        newUE = new UserEntity(usign.Id, uEntity.StopDisobey,
                            usign.Us.DisobeyTimes, uEntity.SpeedLowDisobey, uEntity.SpeedHighDisobey);
                        break;

                }
            
            TableOperation insertOperation = TableOperation.InsertOrReplace(newUE);
            // Execute the insert operation.
            table.Execute(insertOperation);
            return "sbaba";

        }

        public static void AddSign(Sign s)
        {
            // Create the CloudTable object that represents the "signs" table.
            CloudTable table = GetTableClient("Signs");

            // Create a new sign entity.
            SignEntity sign = new SignEntity(s.Name, s.ObeyTimes, s.DisobeyTimes);

            // Create the TableOperation object that inserts the customer entity.
            TableOperation insertOperation = TableOperation.InsertOrReplace(sign);

            // Execute the insert operation.

            table.Execute(insertOperation);
            
        }

    }
}