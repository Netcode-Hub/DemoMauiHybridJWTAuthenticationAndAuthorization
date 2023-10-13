using SharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

 namespace ServerAPI.Data.UserServices
{
    public interface IClientService
    {
        Task<ServiceResponse> RegisterUserAsync(Registration model);
        Task<ServiceResponse> LoginUserAsync(Login model);
    }
}
