using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace YoanaChatApp.Controllers
{
    [Route("api/ChatRoom")]
    [ApiController]
    public class ChatRoom : ControllerBase
    {
        private static List<User> registredUsers = new List<User>();
        private static List<User> joinedUsers = new List<User>();
        private static List<Message> messages = new List<Message>();

        [HttpGet, Route("SendMessage")]
        public ActionResult<IEnumerable<Message>> Get(Message message)
        {
            messages.Add(message);

            return messages;
        }

        [HttpGet, Route("GetMessages")]
        public ActionResult<List<Message>> Get()
        {
            return messages;
        }

        [HttpGet, Route("GetAll")]
        public ActionResult<Message> Get(string IdSender)
        {
            return messages.FirstOrDefault(x => x.IdSender.Equals(IdSender));
        }

        [HttpPost, Route("Join")]
        public ActionResult Post([FromBody] string userId)
        {
            if (string.IsNullOrEmpty(userId)) return BadRequest();
            if (joinedUsers.FirstOrDefault(x => x.Id.Equals(userId))!= null) return BadRequest();
            try
            {
                joinedUsers.Add((registredUsers.FirstOrDefault(x => x.Id.Equals(userId))));
            }
            catch(NullReferenceException)
            {
                return BadRequest();
            }
            return BadRequest();
        }

        [HttpPost, Route("CreateUser")]
        public ActionResult Post([FromBody] UserfromChat newuser)
        {
            string id = Guid.NewGuid().ToString();
            if (newuser != null) registredUsers.Add(new User(id,newuser.Email,newuser.Nickname));
            return Ok(id);
        }



        public class Message
        {
            public string IdSender { get; private set; }
            public string Body { get; set; }
        }
    }

    public class User
    {
        

        public User(string id, string email, string nickname)
        {
            Id = id;
            Email = email;
            Nickname = nickname;
        }

        public string Id { get; private set; }
        public string Email { get; private set; }
        public string Nickname { get; private set; }
    }


    public class UserfromChat
    {
        public UserfromChat(string email, string nickname)
        {
            Email = email;
            Nickname = nickname;
        }

        public string Email { get; private set; }
        public string Nickname { get; private set; }
    }
}
