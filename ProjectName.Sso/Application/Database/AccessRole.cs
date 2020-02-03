using System.ComponentModel.DataAnnotations;

namespace ProjectName.Sso.Application.Database
{
    public class AccessRole
    {
        public int AccessRoleId { get; set; }
        
        [StringLength(2048)]
        [Required]
        public string Name { get; set; }
    }
}