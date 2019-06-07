using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProjectName.Dal.Core;

namespace ProjectName.Essential.Dal.Core.Models
{
    public partial class PersonAddress : IEntity
    {
        public int Id { get; set; }
        public int? PersonId { get; set; }
        public int? AddressId { get; set; }

        [ForeignKey("AddressId")]
        [InverseProperty("PersonAddress")]
        public virtual Address Address { get; set; }
        [ForeignKey("PersonId")]
        [InverseProperty("PersonAddress")]
        public virtual Person Person { get; set; }
    }
}
