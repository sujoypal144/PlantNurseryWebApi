using System;
using System.ComponentModel.DataAnnotations;

namespace PlantNurseryWebApi
{
    public class Plants
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(5)]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        [EnumDataType(typeof(SaleStatus))]
        public SaleStatus SaleStatus { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }

    public enum SaleStatus
    {
        AVAILABLE,
        SOLD
    }
}
