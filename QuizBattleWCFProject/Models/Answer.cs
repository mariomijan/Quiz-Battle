using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [DataContract]
    public class Answer
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string description { get; set; }
        [DataMember]
        public Question Question { get; set; }
        [DataMember]
        public bool isCorrect { get; set; }
        [DataMember]
        public int pointAmount { get; set; }

    }
}
