using eTicketing.Data.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eTicketing.Models
{
    public class Actor :IEntityBase
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Image url required")]
        public string ProfilePictureUrl { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [StringLength(30,MinimumLength =3,ErrorMessage ="Name need to have 3 to 30 char")]
        public string ActorName { get; set; }
        public string Bio { get; set; }
        //relationship
        public List<Actor_Movie> Actors_Movies { get; set; }
    }
}
