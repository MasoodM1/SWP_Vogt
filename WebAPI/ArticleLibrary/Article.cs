using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleLibrary
{
    public class Article
    {
        public int ArticleId { get; set; }
        public string Name { get; set; }
        public double Preis { get; set; }

        public override string ToString()
        {
            return this.ArticleId + " " + this.Name + " " + this.Preis + "Euro";
        }
    }
}
