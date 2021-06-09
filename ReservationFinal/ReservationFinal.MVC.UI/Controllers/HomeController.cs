using ReservationFinal.MVC.UI.Models;
using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;

namespace ReservationFinal.MVC.UI.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(ContactViewModel cvm)
        {
            if (!ModelState.IsValid)
            {
                //Model state was invalid. Send the user back to the view to try again.
                return View(cvm);
            }

            //Build the message
            string message = $"From: {cvm.Name}<br />Subject: {cvm.Subject}<br />From Email: {cvm.Email}<br /><br />{cvm.Message}";

            //MailMessage - what sends the email
            //Arguments for this method were defined in the Web.config at the project level.
            MailMessage mm = new MailMessage(ConfigurationManager.AppSettings["EmailUser"].ToString(), ConfigurationManager.AppSettings["EmailTo"].ToString(), cvm.Subject, message);

            //MailMessage Properties
            //Allow HTML formatting
            mm.IsBodyHtml = true;
            mm.Priority = MailPriority.High;

            //Respond to the sender and not the address the message was sent from
            mm.ReplyToList.Add(cvm.Email);

            SmtpClient client = new SmtpClient(ConfigurationManager.AppSettings["EmailClient"].ToString());

            //Client credentials
            client.Credentials = new NetworkCredential(
                ConfigurationManager.AppSettings["EmailUser"].ToString(),
                ConfigurationManager.AppSettings["EmailPass"].ToString()
                );

            //Try to send the email (POTENTIALLY DANGEROUS)
            try
            {
                //Attempt to send email
                client.Send(mm);
            }
            catch (Exception ex)
            {
                ViewBag.CustomerMessage = $"<p class='text-center'>Your email was unable to be sent. Please try again later or contact support.</p><br /><br />" +
                    $"<h2 class='text-center'>Error Message:</h2><br /> {ex.StackTrace}</p>";
                return View(cvm);
            }

            //Email was sent successfully, route the user to a confirmation view.
            return View("EmailConfirmation", cvm);
        }
    }
}
