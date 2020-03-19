using System.Collections.Generic;


namespace AP.Domain.Models
{
    public class Catalog
    {
        public string Id {get;set;}
        public string Name {get;set;}
        public string FatherId {get;set;}   
        public Catalog Father {get;set;}
        public ICollection<Catalog> InnerCatalogs {get;set;}
        //public ICollection<Item> Items;
    }
}