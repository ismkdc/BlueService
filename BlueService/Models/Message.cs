using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BlueService.Models
{
    public class Message
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int UserId { get; set; }
        public Conversation Conversation { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public Message()
        {
            var timezone = TimeZoneInfo.FindSystemTimeZoneById("Arabic Standard Time");
            Date = TimeZoneInfo.ConvertTime(DateTime.Now, timezone);
        }
    }
}