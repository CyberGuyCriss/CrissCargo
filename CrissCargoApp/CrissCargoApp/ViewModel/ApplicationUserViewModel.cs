namespace CrissCargoApp.ViewModel
{
    public class ApplicationUserViewModel
    {
        public string? Name { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; } 
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
        public string? Address { get; set; }
        public int ZipCode { get; set; }
        public bool? IsActive { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsDeleted { get; set; }

    }
}
