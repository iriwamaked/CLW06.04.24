namespace ASP1.Models.Home.SignUp
{
    public class SignUpPageModel
    {
        public String? Message { get; set; } 
        public bool? IsSuccess { get; set; } //удачно/не удачно

        public SignUpFormModel? signUpFormModel { get; set; }

        public Dictionary<String, String> ValidationErrors { get; set; } = null!;

    }
}
