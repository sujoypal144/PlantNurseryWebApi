using System;
using System.ComponentModel.DataAnnotations;

namespace PlantNurseryWebApi
{
    public class Customers
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public string Phone { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}

