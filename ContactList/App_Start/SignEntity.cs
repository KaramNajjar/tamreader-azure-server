using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContactList.App_Start
{
    public class SignEntity : TableEntity
    {
            public SignEntity(string Name, int ObeyTimes, int DisobeyTimes)
            {
                this.PartitionKey = Name;
                this.RowKey = Name;
                this.Name = Name;
                this.ObeyTimes = ObeyTimes;
                this.DisobeyTimes = DisobeyTimes;

                // calculate sign rating.
                this.Rating = CaculateRating(Name,ObeyTimes,DisobeyTimes);
                
            }

        private double CaculateRating(string name, int obeyTimes, int disobeyTimes)
        {
            double rating = 0;
            switch (name) {

                case "stop":
                    double all = obeyTimes + disobeyTimes;
                     rating = (obeyTimes/all)*100.0;
                    break;

                default:
                    if (disobeyTimes > 100)
                         disobeyTimes = 100;

                    rating =  100 - disobeyTimes;
                    break;
            }

            return rating;

        }

        public SignEntity() { }

            public string Name { get; set; }

            public int ObeyTimes { get; set; }

            public int DisobeyTimes { get; set; }

            public double Rating { get; set; }
        }
}