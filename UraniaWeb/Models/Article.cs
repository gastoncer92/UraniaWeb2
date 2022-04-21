using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

        [DataType(DataType.Upload)]
        public string UrlImagen1 { get; set; }
        [DisplayFormat(NullDisplayText = "")]
        public string UrlImagen2 { get; set; }
        [DisplayFormat(NullDisplayText = "")]
        public string UrlSound1 { get; set; }
    }

}