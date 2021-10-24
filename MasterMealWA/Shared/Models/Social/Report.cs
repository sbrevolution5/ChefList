using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterMealWA.Shared.Models.Social
{
    public class Report
    {
        public int Id { get; set; }
        public int RecipeId { get; set; }
        public string Reason { get; set; }
        public DateTime Submitted { get; set; }
        public string SubmitterId { get; set; }
        public virtual Chef Submitter { get; set; }
        public virtual Recipe Recipe { get; set; }
        public bool Reviewed { get; set; }
        public DateTime ReviewDate { get; set; }
    }
}
