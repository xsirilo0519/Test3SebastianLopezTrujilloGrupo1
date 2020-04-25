using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace test3.Models
{
    public class SolutionRequest
    {
        [Required]
        public string username { get; set; }

        [Required]
        public string solution { get; set; }

    }
}
