using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;
using PersonalPortfolio.Models;

public class ContactController : Controller
{
    [HttpGet]
    public IActionResult Index() => View();

    [HttpPost]
    public IActionResult Index(ContactFormModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var mail = new MailMessage();
        mail.From = new MailAddress("shoeebsaeed97@gmail.com");
        mail.To.Add("sashoeeb4@gmail.com");
        mail.Subject = $"رسالة من: {model.Name}";
        mail.Body = $"البريد: {model.Email}\n\nالرسالة:\n{model.Message}";

        using (var smtp = new SmtpClient("smtp.gmail.com", 587))
        {
            smtp.Credentials = new NetworkCredential("shoeebsaeed97@gmail.com", "zfyt njsw yowl skhw");
            smtp.EnableSsl = true;
            smtp.Send(mail);
        }

        ViewBag.Message = "تم إرسال الرسالة بنجاح.";
        return View();
    }
}
