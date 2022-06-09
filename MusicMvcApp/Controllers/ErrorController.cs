using Microsoft.AspNetCore.Mvc;

namespace MusicMvcApp.Controllers
{
    //Hata Sayfası için controller
    public class ErrorController : Controller
    {
        //Status code un path variable olarak kullanılması
        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            switch (statusCode)
            {
                //Status code a göre caselerin hazırlanması
                case 404:
                    ViewBag.ErrorMessage = "Hata,İstediğiniz kaynağa ulaşılamadı";
                        break; 
            }
            return View("Notfound");
        }
    }
}
