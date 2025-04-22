using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
   public class Customer
    {
        [Key]
            
        public string CustomerID { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public string Proof { get; set; }
        public string PhoneNumber { get; set; }
        public bool CheckIn { get; set; } = true;
        public bool CheckOut { get; set; } = false;
        public DateTime CheckInDate { get; set; } = DateTime.UtcNow;
        public DateTime DefualtCheckOutDate { get; set; }
        public int TotalDays { get; set; }
        public DateTime ExtendDays { get; set; }
        public DateTime CheckedOutDate { get; set; }
        public string RoomID { get; set; }
        public string EmployeeIDs { get; set; }
        public Room Room { get; set; } 




    }
}
