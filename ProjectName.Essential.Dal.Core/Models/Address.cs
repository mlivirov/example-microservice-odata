using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProjectName.Dal.Core;

namespace ProjectName.Essential.Dal.Core.Models
{
    public partial class Address : IEntity
    {
        public Address()
        {
            PersonAddress = new HashSet<PersonAddress>();
        }

        public int Id { get; set; }
        [StringLength(255)]
        public string Street { get; set; }
        [StringLength(255)]
        public string City { get; set; }
        [StringLength(255)]
        public string State { get; set; }
        [StringLength(32)]
        public string Zip { get; set; }
        [StringLength(255)]
        public string Apt { get; set; }

        [InverseProperty("Address")]
        public virtual ICollection<PersonAddress> PersonAddress { get; set; }
    }
}
