using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ASP1.Models.Home
{
    public class HomeModelsFormModel
    {
        [FromForm(Name ="signup-username")]
        public String UserName {  get; set; }
        [FromForm(Name="signup-email")]
        public String UserEmail { get; set; }
    }
}
/*Модель форм "відображає дані, що надходять з HTML-форми, тому кількість полів
відповідає полям класу моделі
Для узгодженя імен HTML та C# вживається атрибут
    FromXXX, який додатково регулює канал одержання даних
   FromForm - POST,
   FromQuery-GET,
   FromBody-JSON...
*/
