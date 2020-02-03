namespace ProjectName.Sso.Application.Database
{
    public class UserRole
    {
        public int UserRoleId { get; set; }
        
        public int UserId { get; set; }
        
        public User User { get; set; }
        
        public int AccessRoleId { get; set; }
        
        public AccessRole AccessRole { get; set; }
    }
}