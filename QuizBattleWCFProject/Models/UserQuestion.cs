using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [DataContract]
    public class UserQuestion
    {
        [DataMember]
        public User user { get; set; }

        [DataMember]
        public Question question  { get; set; }

        [DataMember]
        public Category category { get; set; }

        [DataMember]
        public Lobby lobby { get; set; }

        public UserQuestion()
        {
            user = new User();
            question = new Question();
            category = new Category();
            lobby = new Lobby();
        }

    }
}
