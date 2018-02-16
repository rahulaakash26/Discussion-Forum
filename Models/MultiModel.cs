using System.Collections.Generic;
using PagedList;

namespace ForumProject1.Models
{
    public class MultiModel
    {

        public List<QuestionTableModel> QuestionModelList { get; set; }
        public List<AnswerTableModel> AnswerModelList { get; set; }
        public List<UserRegistrationModel> UserRegModelList { get; set; }
        public List<LangTableModel> LangModelList { get; set; }

        /*database table*/
        public UserRegisteration User { get; set; }

        public UserRegistrationModel UserModel { get; set; }
        public QuestionTableModel QuestionModel { get; set; }
        public AnswerTableModel AnswerModel { get; set; }
        public LangTableModel LangModel { get; set; }

        public IPagedList<QuestionTableModel> QuestionPagedList { get; set; }
        public LoginDetails Login { get; set; }
    }
}