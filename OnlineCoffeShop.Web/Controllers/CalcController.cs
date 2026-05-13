using Microsoft.AspNetCore.Mvc;

namespace OnlineCoffeShop.Web.Controllers;

public class CalcController : Controller
{
    [Route("calc/index")]
    public string Index(double a = 0, double b = 0, string c = "+")
    {
        if (c != "+" && c != "-" && c != "*" && c != "/")
            return $"Некорректный знак операции: '{c}'. Допустимые значения параметра c: +, -, *, / (знак + передавайте как %2B).";

        double result;
        string resultStr;

        if (c == "/" && b == 0)
            return "Ошибка: деление на ноль невозможно.";

        result = c switch
        {
            "-" => a - b,
            "*" => a * b,
            "/" => a / b,
            _   => a + b
        };

        return $"{a} {c} {b} = {result}";
    }
}