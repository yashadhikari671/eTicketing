using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eTicketing.Models
{
    public class Producer
    {
        [Key]
        public int ProducerId { get; set; }
        public string ProfilePictureUrl { get; set; }
        public string ProducerName { get; set; }
        public string Bio { get; set; }
        
        //relationship
        public List<Movie> Movies { get; set; }
    }
}
