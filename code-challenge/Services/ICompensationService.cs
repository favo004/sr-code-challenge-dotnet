using challenge.Models;
using System;

namespace challenge.Services
{
    public interface ICompensationService
    {
        /// <summary>
        /// Returns Compensation by employee ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Compensation GetByEmployeeId(String employeeId);
        Compensation Create(Compensation compensation);
    }
}
