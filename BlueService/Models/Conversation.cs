using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BlueService.Models
{
    public class Conversation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int User1Id { get; set; }
        public int User2Id { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public Conversation()
        {
            Messages = new List<Message>();
        }
    }
}