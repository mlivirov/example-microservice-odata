using System.ComponentModel.DataAnnotations;

namespace ProjectName.Sso.Application.Database
{
    public class UserExternalProvider
    {
        public int UserExternalProviderId { get; set; }

        [Required]
        [StringLength(1024)]
        public string Provider { get; set; }
        
        [Required]
        [StringLength(1024)]
        public string ProviderUserId { get; set; }
        
        public int UserId { get; set; }
        
        public User User { get; set; }
    }
}