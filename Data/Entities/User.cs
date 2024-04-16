namespace ASP1.Data.Entities
{
    public class User
    {
        public Guid       Id    { get; set; }
        public string     Name { get; set; } = null!;
        public string     Email { get; set; }=null!;
        public string?    AvatarUrl { get; set; }  //работа с файлами (изображениями)
        public DateTime?  Birthdate { get; set; } //делаем не обязательной
        public DateTime   Register { get; set; } //дата регистрации
        public String     Salt { get; set; } = null!;      //RFC 2898 (для пароля) ссылка на Kdf service          
        public String     DerivedKey { get; set; }= null!; //RFC 2898 (для пароля) ссылка на Kdf service

    }
}
