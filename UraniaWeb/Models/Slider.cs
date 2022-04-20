using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UraniaWeb.Models
{
    public class Slider
    {
        [Key]
        public int IdSlider { get; set; }

        [StringLength(30, MinimumLength = 3)]
        public string TitleSlider { get; set; }
        public string UrlSlider1 { get; set; }

    }
}
