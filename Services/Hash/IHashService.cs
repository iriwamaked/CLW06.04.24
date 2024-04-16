namespace ASP1.Services.Hash
{
    //Просто декларує (І в SOLID)
    public interface IHashService
    {
        //Digest - слово синонім від hash (як правило назва функція, а Digest - результат його работи)
        String Digest(String input);
    }
}
