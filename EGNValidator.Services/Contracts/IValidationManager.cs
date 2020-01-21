using DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EGNValidator.Services.Contracts
{
    public interface IValidationManager
    {
        bool HasRightToCheck(string ip);

        Task<bool> AddRequestToDB(RequestDTO requestDTO);

        Task RemoveExpiredRequests();
    }
}
