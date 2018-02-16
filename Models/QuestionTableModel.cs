using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ForumProject1.Models
{
    public class QuestionTableModel
    {
        [AllowHtml]
        [UIHint("tinymce_full")]
        [Display(Name = "Question Description")]
        public string QuestionDesc { get; set; }

        public int QuestionId { get; set; }

        public int? UserId { get; set; }

        public string QuestionTitle { get; set; }

        public int? LangId { get; set; }
        public string LangName;

        public string DatePosted { get; set; }

        public UserRegistrationModel regModelClassObj = new UserRegistrationModel();
        public string name;
    }
}