using EGNValidator.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public static class RequestDTOMapper
    {
        public static Request MapRequestDTOToRequest(this RequestDTO requestDTO)
        {
            var request = new Request();
            request.EGN = requestDTO.EGN;
            request.RequestIp = requestDTO.RequestIp;
            return request;

        }
    }
}
