using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using challenge.Models;
using Microsoft.Extensions.Logging;
using challenge.Repositories;

namespace challenge.Services
{
    public class ReportingStructureService : IReportingStructureService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILogger<ReportingStructureService> _logger;
        private List<ReportingStructure> _replist;
       

        public ReportingStructureService(ILogger<ReportingStructureService> logger, 
            IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
            _logger = logger;
        }


        //get all nodes from DB to make a collection in RAM
        private void GetDirectReports(string id, List<Employee> reportList)
        {
            int countReport = 0;
            if (reportList != null)
            {

                foreach (var item in reportList)
                {
                    countReport = reportList.Count;
                    var employee = _employeeRepository.GetById(item.EmployeeId);

                    ReportingStructure reportObj = new ReportingStructure
                    {
                        EmployeeId = employee.EmployeeId,
                        Employee = employee,
                        NumberOfReports = employee.DirectReports == null ? 0 :employee.DirectReports.Count
                    };
                    this._replist.Add(reportObj);
                    GetDirectReports(employee.EmployeeId, employee.DirectReports);

                }

            }
                

        }

        public List<ReportingStructure> GetRepoertingStructureById(string id)
        {
            if (!String.IsNullOrEmpty(id))
            {
                var employee = _employeeRepository.GetById(id);
                int reportCount = 0;
                this._replist = new List<ReportingStructure>();
                ReportingStructure reportObj = new ReportingStructure
                {
                    EmployeeId = employee.EmployeeId,
                    Employee = employee
                };
                this._replist.Add(reportObj);
                GetDirectReports(id, employee.DirectReports);
                this._replist[0].NumberOfReports = this._replist.Count - 1;
                return _replist;
            }

            return null;
        }

     
    }
}
