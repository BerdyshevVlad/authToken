using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthToken.DataAccess.Entities
{
    public class Book : BaseEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }

    }
}
