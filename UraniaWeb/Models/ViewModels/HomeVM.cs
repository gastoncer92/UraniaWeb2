using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;


namespace UraniaWeb.Models.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<Article> ListaDeArticulos { get; set; }
    }
}
