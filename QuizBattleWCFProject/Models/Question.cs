using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [DataContract(IsReference = true)]
    public class Question
    {
        
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string description { get; set; }
        [DataMember]
        public Category category { get; set; }
        [DataMember]
        public List<Answer> Answers { get; set; }

        public Question()
        {
            Answers = new List<Answer>();
        }
    }
}
