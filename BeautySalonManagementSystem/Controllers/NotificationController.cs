using BeautySalonManagementSystem.Models;
using BeautySalonManagementSystem.RepositoryServices.EntityFramework;
using Google.Rpc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BeautySalonManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NotificationController : ControllerBase
    {
        private readonly BeautySalonContext dbContext;

        public NotificationController(BeautySalonContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet("read/{userMail}")]
        [Authorize]
        public IActionResult GetRead(string userMail)
        {
            var notifications = dbContext.Notifications
                            .Include(x => x.User)
                            .Where(x => x.User.Email == userMail && x.State == NotificationState.READ)
                            .ToList();

            return new JsonResult(notifications);
        }

        [HttpGet("unread/{userMail}")]
        [Authorize]
        public IActionResult GetUnread(string userMail)
        {
            var notifications = dbContext.Notifications
                            .Include(x => x.User)
                            .Where(x => x.User.Email == userMail && x.State == NotificationState.UNREAD)
                            .ToList();

            foreach(var notification in notifications)
            {
                notification.State = NotificationState.READ;
            }

            dbContext.SaveChanges();

            return new JsonResult(notifications);
        }

        [HttpGet("count/{userMail}")]
        [Authorize]
        public IActionResult CountUnread(string userMail)
        {
            var notifications = dbContext.Notifications
                            .Include(x => x.User)
                            .Where(x => x.User.Email == userMail && x.State == NotificationState.UNREAD)
                            .Count();

            return new JsonResult(notifications);
        }


        [HttpDelete("{notificationId}")]
        [Authorize]
        public IActionResult DeleteAppointment(int notificationId)
        {
            try
            {
                var notification = dbContext.Notifications
                          .Include(x => x.User)
                          .Where(x => x.Id == notificationId)
                          .FirstOrDefault();

                if (notification == null)
                {
                    return BadRequest("Notification does not exist!");
                }

                dbContext.Remove(notification);
                dbContext.SaveChanges();

                return new JsonResult("Deleted");

            }
            catch (Exception)
            {
                return BadRequest("Server failed to delete notification. Please, try again!");
            }

        }



    }
}
