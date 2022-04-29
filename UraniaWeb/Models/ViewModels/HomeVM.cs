using System;
using System.Collections.Generic;
using System.Text;

namespace UraniaWeb.Models.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<Slider> Slider { get; set; }
        public IEnumerable<Article> ListaArticulos { get; set; }
    }
}
