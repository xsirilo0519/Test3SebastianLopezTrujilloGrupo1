using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace test3.Models
{
    public class UserRequest
    {
        [Required]
        public string document { get; set; }

        [Required]
        public string firstName { get; set; }

        [Required]
        public string lastName { get; set; }

        [Required]
        public string username { get; set; }

        [Required]
        public string phone { get; set; }

        [Required]
        public int group { get; set; }


    }
}
