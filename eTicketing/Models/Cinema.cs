using eTicketing.Data.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eTicketing.Models
{
    public class Cinema : IEntityBase
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "The logo is required")]
        public string Logo { get; set; }
        [Required(ErrorMessage = "The Name is required")]
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Movie> Movies { get; set; }
        
    }
}
