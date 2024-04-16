namespace ASP1.Services.Kdf
{
    //key derivation function by RFC 2898 (sec 5.1)
    public interface IKdfService
    {
        void Config(int iterationCount, int dkLength);
        String GetDerivedKey(String password, String salt);
    }
    
}


