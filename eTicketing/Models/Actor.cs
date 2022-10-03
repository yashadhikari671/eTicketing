using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eTicketing.Models
{
    public class Actor
    {
        [Key]
        public int ActorId { get; set; }
        public string ProfilePictureUrl { get; set; }
        public string ActorName { get; set; }
        public string Bio { get; set; }
        //relationship
        public List<Actor_Movie> Actors_Movies { get; set; }
    }
}
