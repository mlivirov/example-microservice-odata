using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProjectName.Dal.Core;

namespace ProjectName.Essential.Dal.Core.Models
{
    public partial class Person : IEntity
    {
        public Person()
        {
            PersonAddress = new HashSet<PersonAddress>();
            UserAccount = new HashSet<UserAccount>();
        }

        public int Id { get; set; }
        [StringLength(255)]
        public string FirstName { get; set; }
        [StringLength(255)]
        public string LastName { get; set; }
        [StringLength(255)]
        public string Email { get; set; }
        [StringLength(255)]
        public string Phone { get; set; }

        [InverseProperty("Person")]
        public virtual ICollection<PersonAddress> PersonAddress { get; set; }
        [InverseProperty("Person")]
        public virtual ICollection<UserAccount> UserAccount { get; set; }
    }
}
