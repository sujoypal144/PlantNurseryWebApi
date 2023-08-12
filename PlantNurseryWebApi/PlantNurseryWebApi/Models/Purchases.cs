using System;
using System.ComponentModel.DataAnnotations;

namespace PlantNurseryWebApi
{
    public class Purchases
    {
        public int Id { get; set; }

        [Required]
        public int PlantId { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        public bool IsActive { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
