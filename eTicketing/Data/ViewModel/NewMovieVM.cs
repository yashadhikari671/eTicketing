using eTicketing.Data.Base;
using eTicketing.Data.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eTicketing.Models
{
    public class NewMovieVM
    {
        public int Id { get; set; }
        [Display(Name = "Movie Name")]
        [Required(ErrorMessage = "name is required")]
        public string Name { get; set; }
        [Display(Name = "Movie Description")]
        [Required(ErrorMessage = "Discreption is required")]
        public string Description { get; set; }
        [Display(Name = "Price in $")]
        [Required(ErrorMessage = "Price is required")]
        public double Price { get; set; }
        [Display(Name = "Movie Image")]
        [Required(ErrorMessage = "Image Url is required")]
        public string ImageUrl { get; set; }
        [Display(Name = "Start Date")]
        [Required(ErrorMessage = "Start Date is required")]
        public DateTime StartDate { get; set; }
        [Display(Name = "End Date")]
        [Required(ErrorMessage = "End Date is required")]
        public DateTime EndDate { get; set; }
        [Display(Name = "Select category")]
        [Required(ErrorMessage = "Category is Required")]
        public MovieCategory MovieCategory { get; set; }
        [Display(Name = "Select one or many Actor/Actros")]
        [Required(ErrorMessage = " Actor/Actros is Required")]
        public List<int> AId { get; set; }
        [Display(Name = "Select Cinema")]
        [Required(ErrorMessage = " Cinema is Required")]
        public int CinemaId { get; set; }
        [Display(Name = "Select Producer")]
        [Required(ErrorMessage = " Producer is Required")]
        public int ProducerId { get; set; }
       

    }
}
