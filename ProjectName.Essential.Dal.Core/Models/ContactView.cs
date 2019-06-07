using System.ComponentModel.DataAnnotations;
using ProjectName.Dal.Core;

namespace ProjectName.Essential.Dal.Core.Models
{
    public partial class ContactView : IView
    {
        [Key]
        public int PersonId { get; set; }

        public int? AddressId { get; set; }

        public string FullName { get; set; }
        
        public string FullAddress { get; set; }
    }
}
