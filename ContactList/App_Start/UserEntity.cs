using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContactList.App_Start
{
    public class UserEntity : TableEntity
    {

        public UserEntity(int id, int stopDisobey, int entryDisobey,
            int speedLowDisobey,int speedHighDisobey)
        {
            this.PartitionKey = id.ToString();
            this.RowKey = id.ToString() ;
            this.Id = id;
            this.EntryDisobey = entryDisobey;
            this.SpeedLowDisobey = speedLowDisobey;
            this.SpeedHighDisobey = speedHighDisobey;
            this.StopDisobey = stopDisobey;

            // calculate sign rating.
            this.Rating = stopDisobey + speedHighDisobey + speedLowDisobey + entryDisobey;

        }

        public UserEntity() { }

        public int Id { get; set; }

        public int StopDisobey { get; set; }

        public int EntryDisobey { get; set; }

        public int SpeedLowDisobey { get; set; }

        public int SpeedHighDisobey { get; set; }

        public double Rating { get; set; }
    }

}