using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [DataContract]
    public class User
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string lastName { get; set; }
        [DataMember]
        public string country { get; set; }
        [DataMember]
        public string phone { get; set; }
        [DataMember]
        public Login login { get; set; }
        [DataMember]
        public int point { get; set; }
        [DataMember]
        public Category category { get; set; }
        [DataMember]
        public byte[] timeStamp { get; set; }
        [DataMember]
        public int joinedLobbyId { get; set; }
        [DataMember]
        public int lobbyOwnedId { get; set; }
    }
}
