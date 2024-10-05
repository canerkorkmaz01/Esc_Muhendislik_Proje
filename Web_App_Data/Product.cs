using Web_App_Data.Infrastructure;
using Web_App_Data.Interface;

namespace Web_App_Data
{
 
        public class Product : BaseEntity, IEntity
        {
            public string? ProductCode { get; set; }
            public string? ProductName { get; set; }
            public int Quantity { get; set; }
            public decimal UnitPrice { get; set; }
        }
    }

