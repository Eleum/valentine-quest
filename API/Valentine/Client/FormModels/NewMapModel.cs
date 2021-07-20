using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Valentine.Client.FormModels
{
    public class NewMapModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Location { get; set; }

        public string Description { get; set; }
    }
}
