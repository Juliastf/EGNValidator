using DTO;
using EGNValidator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EGNValidator.Models.Mapper
{
    public static class ViewModelMapper
    {
        public static RequestDTO MapViewModelToDTO(this RequestViewModel requestVM)
        {
            var requestDTO = new RequestDTO();
            requestDTO.EGN = requestVM.EGN;
            requestDTO.RequestIp = requestVM.Ip;
            return requestDTO;
        }
    }
}
