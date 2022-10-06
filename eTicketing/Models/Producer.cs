using eTicketing.Data.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eTicketing.Models
{
    public class Producer : IEntityBase
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="The profile pix is required")]
        public string ProfilePictureUrl { get; set; }
        [Required(ErrorMessage = "The Name is required")]
        public string ProducerName { get; set; }
        public string Bio { get; set; }
        
        //relationship
        public List<Movie> Movies { get; set; }
    }
}
