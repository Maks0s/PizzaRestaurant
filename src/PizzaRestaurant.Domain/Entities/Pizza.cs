using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzaRestaurant.Domain.Entities
{
    public class Pizza
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MinLength(4)]
        [MaxLength(50)]
        public string Name { get; set; } = default!;

        [Required]
        [MinLength(4)]
        [MaxLength(20)]
        public string CrustType { get; set; } = default!;

        [Required]
        [MinLength(20)]
        [MaxLength(255)]
        public string Ingredients { get; set; } = default!;

        [Required]
        [Column(TypeName = "decimal(5,2)")]
        [Range(0, 500)]
        public decimal Price { get; set; }
    }
}
