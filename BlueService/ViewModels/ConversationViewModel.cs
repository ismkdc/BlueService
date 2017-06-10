using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlueService.ViewModels
{
    public class ConversationViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ProfileImgUrl { get; set; }
        public string Message { get; set; }
    }
}