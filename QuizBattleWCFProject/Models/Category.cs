using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Models
{
    [DataContract(IsReference = true)]
    public class Category
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public List<Question> question { get; set; }

        public Category()
        { 
            question = new List<Question>();
        }
    }
}