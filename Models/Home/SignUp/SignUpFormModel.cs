using Microsoft.AspNetCore.Mvc;

namespace ASP1.Models.Home.SignUp
{
    public class SignUpFormModel
    {
        [FromForm(Name = "signup-username")]
        public String UserName { get; set; }
        [FromForm(Name = "signup-email")]
        public String UserEmail { get; set; }

        [FromForm(Name = "singhup-avatar")]
        public IFormFile AvatarFile { get; set; } = null!;

        [FromForm(Name = "signup-birthdate")]
        public DateTime? Birthdate { get; set; }=null!;

        [FromForm(Name = "signup-confirm")]
        public bool Confirm { get; set; }
    }
}
