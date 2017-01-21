using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContactList.App_Start
{
    public class SignEntity :TableEntity
    {
            public SignEntity(string Name, int ObeyTimes, int DisobeyTimes)
            {
                this.PartitionKey = Name;
                this.RowKey = Name;
                this.Name = Name;
                this.ObeyTimes = ObeyTimes;
                this.DisobeyTimes = DisobeyTimes;
            }

            public SignEntity() { }

            public string Name { get; set; }

            public int ObeyTimes { get; set; }

            public int DisobeyTimes { get; set; }
        }
}