using MediatR;
using RL.Backend.Exceptions;
using RL.BackEnd.BusinessService.Commands;
using RL.BackEnd.Common.Model;
using RL.BackEnd.Data;
using RL.Data;
using RL.Data.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RL.BackEnd.BusinessService.UserService
{
    public class UserServiceCommonHandler : IRequestHandler<UserCommand, ApiResponse<Unit>>,IUserService
    {
        private readonly RLContext _context;
        private readonly IRepository<User> _repository;

        public UserServiceCommonHandler(RLContext context, IRepository<User> repository)
        {
            _context = context;
            _repository = repository;
        }

        public ApiResponse<UserModel> GetById(int id)
        {
            try
            {

                UserModel userModel = new UserModel();
                var dbUser = _repository.GetById(id);
                if (dbUser == null)
                    return ApiResponse<UserModel>.Fail(new NotFoundException());
                return ApiResponse<UserModel>.Succeed(userModel);

            }
            catch (Exception ex)
            {
                return ApiResponse<UserModel>.Fail(ex);
            } 
            
        }

        public async Task<ApiResponse<Unit>> Handle(UserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                //Validate request
                //if (request.UserId < 1)
                //    return ApiResponse<Unit>.Fail(new Exception("Invalid UserId")); 

                var user = _repository.GetById(request.UserId);


                if (user != null)
                {
                    user = new User() { Name = request.Name };
                    _repository.Add(user); 

                }
                else 
                {
                    user.Name = request.Name;
                    _repository.Update(user);
                } 

                return ApiResponse<Unit>.Succeed(new Unit());
            }
            catch (Exception e)
            {
                return ApiResponse<Unit>.Fail(e);
            }
        }

    }

}
