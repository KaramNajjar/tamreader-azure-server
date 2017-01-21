using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContactList.Models
{
    public class Sign
    {
        /// <summary>
        /// The Sign's name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The Sign's Obedience times. 
        /// </summary>
        public int ObeyTimes { get; set; }


        /// <summary>
        /// The Sign's DisObedience times. 
        /// </summary>
        public int DisobeyTimes { get; set; }

    }
}