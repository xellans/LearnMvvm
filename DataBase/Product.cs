using Common.Standard.Interfaces.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataBase
{
    public class Product : IProduct
    {
        private string _name = string.Empty;
        private string _description = string.Empty;

        public long Id { get; internal set; }
        public string Name { get => _name; internal set => _name = value ?? string.Empty; }
        public string Description { get => _description; internal set => _description = value ?? string.Empty; }
    }
}
