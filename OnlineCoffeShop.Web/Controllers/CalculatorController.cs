using Microsoft.AspNetCore.Mvc;

namespace OnlineCoffeShop.Web.Controllers;

public class CalculatorController : Controller
{
    [Route("calculator/index/{a?}/{b?}/{op?}")]
    public string Index(double a, double b, string op = "+")
    {
        if (op != "+" && op != "-" && op != "*")
            op = "+";

        double result = op switch
        {
            "-" => a - b,
            "*" => a * b,
            _   => a + b
        };

        return $"{a} {op} {b} = {result}";
    }
}