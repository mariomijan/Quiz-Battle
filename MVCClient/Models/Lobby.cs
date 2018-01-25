using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCClient.Models
{
    public class Lobby
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }

        // this is the lobby owner
        public string LobbyOwnerId { get; set; }
        [ForeignKey("LobbyOwnerId")]
        public ApplicationUser LobbyOwner { get; set; }

        //this is the lobby players
        public List<ApplicationUser> UsersInLobby { get; set; }
        public Lobby()
        {
            UsersInLobby = new List<ApplicationUser>();

        }
    }
}