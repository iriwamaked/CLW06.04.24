namespace ASP1.Services.Hash
{
    public class Md5HashService : IHashService
    {
        public string Digest(string input)=>
        Convert.ToHexString(System.Security.Cryptography.MD5.Create()
                .ComputeHash(System.Text.Encoding.UTF8.GetBytes(input)));
        
    }
}
