//namespace NewProj.Services
//{
//    using Microsoft.AspNetCore.Identity.UI.Services;
//    using SendGrid;
//    using SendGrid.Helpers.Mail;
//    using System.Net;
//    using System.Net.Mail;
//    using System.Threading.Tasks;

//    namespace AspNetCoreEmailConfirmationSendGrid.Services
//    {
//        //public class EmailSender : IEmailSender
//        //{
//        //    public Task SendEmailAsync(string email, string subject, string htmlMessage)
//        //    {




//        public class EmailSender : IEmailSender
//        {
//            public EmailSender()
//            {

//            }
            
//            public async Task SendEmailAsync(string email, string subject, string htmlMessage)
//            {
                
//                string fromMail = "hossamkhaled327@gmail.com";
//                string fromPassword = "yadosiejsrodqmob";

//                MailMessage message = new MailMessage();
//                message.From = new MailAddress(fromMail);
//                message.Subject = subject;
//                message.To.Add(new MailAddress(email));
//                message.Body = "<html><body> " + htmlMessage + " </body></html>";
//                message.IsBodyHtml = true;

//                var smtpClient = new SmtpClient("smtp.gmail.com")
//                {
//                    Port = 587,
//                    Credentials = new NetworkCredential(fromMail, fromPassword),
//                    EnableSsl = true,
//                };
//                smtpClient.Send(message);
//            }
//        }
//    }
//}












//            //            var sendGridKey = @"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";

//            //            // SG.DOQwS20QS8 - klKSSWeN1qg.gt2e4GY2bpEe8JM - CsZkIJEUcV6_7LIcbkv3dHyJCbA
//            //            //var sendGridKey = @"SG.DOQwS20QS8 - klKSSWeN1qg.gt2e4GY2bpEe8JM - CsZkIJEUcV6_7LIcbkv3dHyJCbA";

//            //            return Execute(sendGridKey, subject, htmlMessage, email);
//            //        }

//            //        public Task Execute(string apiKey, string subject, string message, string email)
//            //        {
//            //            var client = new SendGridClient(apiKey);
//            //            var msg = new SendGridMessage()
//            //            {
//            //                From = new EmailAddress("hossamkhaled327@gmail.com", "Fitrianingrum via SendGrid"),
//            //                Subject = subject,
//            //                PlainTextContent = message,
//            //                HtmlContent = message
//            //            };
//            //            msg.AddTo(new EmailAddress(email));

//            //            // Disable click tracking.
//            //            // See https://sendgrid.com/docs/User_Guide/Settings/tracking.html
//            //            msg.SetClickTracking(false, false);

//            //            return client.SendEmailAsync(msg);
        
        
    

