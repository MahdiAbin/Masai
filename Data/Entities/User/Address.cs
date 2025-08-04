using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities.User
{
    public class Address
    {
        [Key]
        public int ID { get; set; }
        public string FullAddress { get; set; }
        public string Province { get; set; }
        public string City { get; set; }


        #region Releations
        public User User { get; set; }
        public int UserID { get; set; }

        #endregion
    }
}
