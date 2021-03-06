//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ForumProject1.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class QuestionTable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public QuestionTable()
        {
            this.AnswerTables = new HashSet<AnswerTable>();
        }
    
        public int QuestionId { get; set; }
        public int UserId { get; set; }
        public string QuestionTitle { get; set; }

        [AllowHtml]
        [UIHint("tinymce_full")]
        public string QuestionDesc { get; set; }
        public int LangId { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AnswerTable> AnswerTables { get; set; }
        public virtual LangTable LangTable { get; set; }
        public virtual LangTable LangTable1 { get; set; }
        public virtual UserRegisteration UserRegisteration { get; set; }
    }
}
