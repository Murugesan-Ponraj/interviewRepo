using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RL.BackEnd.Common.Model
{
    public class UserModel
    {
        public int UserId { get; set; }
        public string Name { get; set; } 
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
