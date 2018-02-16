using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ForumProject1.Models
{
    public class AnswerTableModel
    {
        [AllowHtml]
        [UIHint("tinymce_full")]
        [Display(Name = "Answer")]
        public string AnswerDesc { get; set; }

        public int AnswerId { get; set; }
        public int QuestionId { get; set; }
        public int UserId { get; set; }
        public int? LangId { get; set; }
        public string AnswerDate { get; set; }

        public string name;
    }
}