namespace ASP1.Services.Hash
{
    public class ShaHashService:IHashService
    {
        public string Digest(string input) =>
        Convert.ToHexString(System.Security.Cryptography.SHA1.Create()
                .ComputeHash(System.Text.Encoding.UTF8.GetBytes(input)));
    }
}
