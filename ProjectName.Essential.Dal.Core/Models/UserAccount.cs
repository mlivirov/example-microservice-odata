using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProjectName.Dal.Core;

namespace ProjectName.Essential.Dal.Core.Models
{
    public partial class UserAccount : IEntity
    {
        public UserAccount()
        {
            UserAccountGroup = new HashSet<UserAccountGroup>();
        }

        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string UserName { get; set; }
        public int? PersonId { get; set; }

        [ForeignKey("PersonId")]
        [InverseProperty("UserAccount")]
        public virtual Person Person { get; set; }
        [InverseProperty("UserAccount")]
        public virtual ICollection<UserAccountGroup> UserAccountGroup { get; set; }
    }
}
