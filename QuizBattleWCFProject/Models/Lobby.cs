using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [DataContract]
    public class Lobby
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public Category category { get; set; }
        [DataMember]
        public bool isStarted { get; set; }

        public Lobby()
        {
            category = new Category();
        }
    }
}
