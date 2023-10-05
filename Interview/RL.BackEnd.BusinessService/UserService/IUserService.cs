using RL.BackEnd.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RL.BackEnd.BusinessService.UserService
{
    public interface IUserService
    {
        ApiResponse<UserModel> GetById(int id);
    }
}
