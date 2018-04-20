using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using skeleton.Models;

namespace skeleton.Models {

	public class AddActivityViewModel: BaseEntity {
        [Required]
        [Display(Name = "Title")]
        [MinLength(2)]
        public string title { get; set; }
        [Required]
        [Display(Name = "Duration")]
        public int duration { get; set; }
        [Required]
        public int durationAnnotation { get; set; }

        [Required]
        [Display(Name = "Description")]
        [MinLength(10)]
        public string description { get; set; }


        [Required]
        [ValidDate]
        [Display(Name = "Date and Time")]
        public DateTime sessionDateTime { get; set; }

        
    }
}