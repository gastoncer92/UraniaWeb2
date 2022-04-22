using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace UraniaWeb.Models
{
    public class Administrador
    {
        [Key]
        public int IdAdmin { get; set; }
        [Required]
        [Display(Name = "Administrador")]
        [StringLength(50)]
        public string NameAdmin { get; set; }
        public int PassAdmin { get; set; }
    }
}
