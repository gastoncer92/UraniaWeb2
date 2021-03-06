using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace UraniaWeb.Models
{
    public class Article
    {
        [Key]
        public int IdAticle { get; set; }

        [StringLength(60, MinimumLength = 3)]
        public string TitleArticle { get; set; }
        public string DescritionArticle { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha de Alta")]
        public DateTime DateCreation { get; set; }
        [DisplayFormat(NullDisplayText = "")]

        [DataType(DataType.Url)]
        public string UrlImagen1 { get; set; }
        [DisplayFormat(NullDisplayText = "")]
        [DataType(DataType.Url)]
        public string UrlImagen2 { get; set; }
        [DisplayFormat(NullDisplayText = "")]
        [DataType(DataType.Url)]
        public string UrlSound1 { get; set; }

        //public IEnumerable<Article> Articles { get; set; }

        public Article article { get; set; }



        
    }
    

}