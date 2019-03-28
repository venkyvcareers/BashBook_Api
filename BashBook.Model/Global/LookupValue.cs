using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BashBook.Model.Global
{
    public class LookupValueModel
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string LogoUrl { get; set; }
        public bool IsDefault { get; set; }
        public bool IsActive { get; set; }
    }

    public class LookupValueJsonModel
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string Name { get; set; }
        public string LogoUrl { get; set; }
        public string Code { get; set; }
    }
}
