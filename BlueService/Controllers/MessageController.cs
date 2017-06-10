using BlueService.Models;
using BlueService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace BlueService.Controllers
{
    public class MessageController : ApiController
    {
        BlueServiceDataContext dc;
        public MessageController()
        {
            dc = new BlueServiceDataContext();
        }
        [HttpGet]
        public List<ConversationViewModel> GetConversations(string token)
        {
            if (token.Length > 0)
            {
                var user = dc.Users.SingleOrDefault(u => u.Token == token);
                if (user != null)
                {
                    return user.GetConversations().Select(c => new ConversationViewModel() { Id = c.Id, Name = c.User2.Name, ProfileImgUrl = c.User2.ProfileImage, Message = c.Messages.OrderByDescending(m => m.Date).First().Text.Take(30).ToSystemString() }).ToList();
                }
            }
            return null;

        }
        [HttpGet]
        public List<MessageViewModel> GetMessages(int conId, string token)
        {
            if (token.Length > 0)
            {
                var user = dc.Users.SingleOrDefault(u => u.Token == token);
                if (user != null)
                {
                    var con = dc.Conversations.SingleOrDefault(c => c.Id == conId);
                    var data = con.Messages.OrderBy(m => m.Date).ToList();
                    List<MessageViewModel> result = new List<MessageViewModel>();
                    string username = "";
                    foreach (var item in data)
                    {
                        var myUser = dc.Users.SingleOrDefault(u => u.Id == item.UserId);
                        string myClass = "";
                        if (myUser.Id == user.Id) myClass = "right";
                        result.Add(new MessageViewModel() { Message = item.Text, Photo = myUser.ProfileImage, Class = myClass });
                    }
                    if (con.User1Id == user.Id)
                    {
                        username = dc.Users.SingleOrDefault(u => u.Id == con.User2Id).Name;
                    }
                    else
                    {
                        username = dc.Users.SingleOrDefault(u => u.Id == con.User1Id).Name;
                    }
                    result[0].UserName = username;
                    return result;
                }



            }
            return null;

        }

        [HttpGet]
        public Response SendMessage(string msg, string token, int conId)
        {
            if (token.Length > 0)
            {
                var user = dc.Users.SingleOrDefault(u => u.Token == token);
                if (user != null)
                {
                    var data = dc.Conversations.SingleOrDefault(c => c.Id == conId);
                    if (data == null)
                    {
                        data = new Models.Conversation();
                        dc.Conversations.Add(data);
                        data.User1Id = user.Id;
                        data.User2Id = conId * -1;
                    }
                    data.Messages.Add(new Message() { Text = msg, UserId = user.Id });
                    dc.SaveChanges();
                    return new Response(data.Id.ToString());
                }
            }
            return null;
        }
    }
}