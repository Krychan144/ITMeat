using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITMeat.DataAccess.Models
{
    public class User : BaseEntity
    {
        public User()
        {
            Tokens = new HashSet<UserToken>();
            UserOrders = new HashSet<UserOrder>();
        }

        public virtual ICollection<UserOrder> UserOrders { get; set; }
        public virtual ICollection<UserToken> Tokens { get; set; }

        [Required]
        [Column(TypeName = "NVARCHAR(256)")]
        public string Email { get; set; }

        [Required]
        [Column(TypeName = "NVARCHAR(256)")]
        public string PasswordHash { get; set; }

        [Required]
        [Column(TypeName = "NVARCHAR(128)")]
        public string PasswordSalt { get; set; }

        public DateTime? EmailConfirmedOn { get; set; }

        [Required]
        [Column(TypeName = "NVARCHAR(100)")]
        public string Name { get; set; }

        public DateTime? LockedOn { get; set; }
    }
}