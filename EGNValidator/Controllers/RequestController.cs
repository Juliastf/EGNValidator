using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using EGNValidator.Models;
using EGNValidator.Models.Mapper;
using EGNValidator.Services.Contracts;

namespace EGNValidator.Controllers
{
    public class RequestController : Controller
    {
        private readonly IValidationManager _manager;

        public RequestController(IValidationManager manager)
        {
            _manager = manager;

        }


        // GET: Requests/Create
        public IActionResult Create()
        {
            var requestVM = new RequestViewModel();
            return View(requestVM);
        }

        // POST: Requests/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RequestViewModel requestVM)
        {
            requestVM.Ip = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            var hasRightToCheck = _manager.HasRightToCheck(requestVM.Ip);
            if (!hasRightToCheck)
            {
                TempData["ErrorMessage"] = "You have already validated 5 EGNs in the last week!";
                return View();
            }

            if (ModelState.IsValid)
            {
                var requestDTO = requestVM.MapViewModelToDTO();
                requestDTO.IsValid = await _manager.AddRequestToDB(requestDTO);
                if (requestDTO.IsValid)
                {
                    TempData["ResultValid"] = $"EGN {requestDTO.EGN} is Valid";

                }
                else
                {
                    TempData["ResultNotValid"] = $"EGN {requestDTO.EGN} is NOT Valid";
                }
            }
            return View();
        }


    }
}
