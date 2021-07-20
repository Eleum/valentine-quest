using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Valentine.Client.FormModels
{
    public class NewMapModel
    {
        [Required(ErrorMessage = "Please provide a map title")]
        public string Title { get; set; }

        [Required]
        public string Location { get; set; }

        public string Description { get; set; }

        public bool IsDefault { get; set; }
    }
}
