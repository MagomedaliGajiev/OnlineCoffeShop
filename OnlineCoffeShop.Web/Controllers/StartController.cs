using Microsoft.AspNetCore.Mvc;

namespace OnlineCoffeShop.Web.Controllers;

public class StartController : Controller
{
    public string Hello()
    {
        var hour = DateTime.Now.Hour;

        string greeting = hour switch
        {
            >= 6 and <= 11 => "Доброе утро",
            >= 12 and <= 17 => "Добрый день",
            >= 18 and <= 23 => "Добрый вечер",
            _ => "Доброй ночи" // 0–5
        };
        
        return  greeting;
    }
}