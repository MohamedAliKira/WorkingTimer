using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkingTimer.Shared;

namespace WorkingTimer.Server.Services
{
    public interface IUserManagerService
    {
        Task<UserManagerResponse> RegisterUseAsync(RegisterRequest model);
    }

    public class UserManagerService : IUserManagerService
    {
        public async Task<UserManagerResponse> RegisterUseAsync(RegisterRequest model)
        {
            if(model ==null)
                throw new NullReferenceException("Register Model is null");
        }
    }
}
