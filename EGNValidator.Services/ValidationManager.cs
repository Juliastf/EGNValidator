using DTO;
using EGNValidator.Data;
using EGNValidator.Services.Contracts;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EGNValidator.Services
{
    public class ValidationManager : IValidationManager
    {
        private readonly EGNValidatorContext _context;

        public ValidationManager(EGNValidatorContext context)
        {
            _context = context ?? throw new InvalidOperationException("Context is null");
        }

        public bool HasRightToCheck(string ip)
        {
            var numberOfChecks = _context.Requests
                    .Where(r => r.RequestIp == ip & r.RequestDate >= DateTime.Now.AddDays(-7))
                    .Count();
            if (numberOfChecks < 5)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> AddRequestToDB(RequestDTO requestDTO)
        {
            if (requestDTO is null)
            {
                throw new InvalidOperationException("Object cannot be null!");
            }
            var newRequest = requestDTO.MapRequestDTOToRequest();
            newRequest.RequestDate = DateTime.Now;
            newRequest.IsValid = IsEGNValid(requestDTO.EGN);
            _context.Add(newRequest);
            await _context.SaveChangesAsync();
            return newRequest.IsValid;
        }

        private bool IsEGNValid(string egn)
        {
            var year = int.Parse(egn.Substring(0, 2));
            var month = int.Parse(egn.Substring(2, 2));
            if (month > 40)
            {
                month = month - 40;
                year = 2000 + year;
            }
            else if (month > 20)
            {
                month = month - 20;
                year = 1800 + year;
            }
            else
            {
                year = 1900 + year;
            }
            var day = int.Parse(egn.Substring(4, 2));
            return IsValidDate(year, month, day);
        }

        private bool IsValidDate(int year, int month, int day)
        {
            if (month < 1 || month > 12)
            {
                return false;
            }
            if (day < 1 || day > 31)
            {
                return false;
            }
            var shortMonths = new int[] { 4, 6, 9, 11 };
            if (shortMonths.Contains(month) && day > 30)
            {
                return false;
            }
            if (month == 2)
            {
                if (day > 29)
                {
                    return false;
                }
                if (day == 29)
                {
                    bool isLeapYear = year % 4 == 0 && (year % 100 != 0 || year % 400 == 0);
                    return isLeapYear;
                }
            }
            return true;
        }

        public async Task RemoveExpiredRequests()
        {
            var requestsToRemove = _context.Requests.Where(r => r.RequestDate < DateTime.Now.AddDays(-60)).ToList();

            _context.Requests.RemoveRange(requestsToRemove);
            await _context.SaveChangesAsync();

        }
    }
}
