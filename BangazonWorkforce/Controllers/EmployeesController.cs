/* Authors: Jonathan Schaffer, Billy Mathison
 * Purpose: Creating a controller for the Employee model. 
 * Methods: Index, Details, Create, Edit, Delete, and GetEmployeeById. The Get Employees Methods gets all employees as well as the department they are in.
  */

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using BangazonWorkforce.Models;
using BangazonWorkforce.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace BangazonWorkforce.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IConfiguration _config;

        public EmployeesController(IConfiguration config)
        {
            _config = config;
        }

        public SqlConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }
        // GET: Employees
        public ActionResult Index()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT e.Id, e.FirstName, e.LastName, d.[Name]
                                        FROM Employee e 
                                        LEFT JOIN Department d ON e.DepartmentId = d.Id";

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Employee> employees = new List<Employee>();

                    while (reader.Read())
                    {
                        Employee employee = new Employee
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                            LastName = reader.GetString(reader.GetOrdinal("LastName")),
                            Department = new Department
                            {
                                Name = reader.GetString(reader.GetOrdinal("Name"))
                            }
                        };
                        employees.Add(employee);

                    }
                    reader.Close();

                    return View(employees);
                }
            }
        }
        // GET: Employees/Details/5
        public ActionResult Details(int id)
        {
            EmployeeDetailsViewModel employee = GetEmployeeById(id);
            return View(employee);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Employees/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Employees/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private EmployeeDetailsViewModel GetEmployeeById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT e.Id AS EmployeeId, 
                                        e.FirstName, 
                                        e.LastName, 
                                        d.[Name] AS DepartmentName, 
                                        c.Id AS ComputerId, 
                                        c.Make, 
                                        c.Manufacturer,
                                        ce.Id AS ComputerEmployeeId,
                                        ce.AssignDate,
                                        ce.UnassignDate,
                                        tp.Id AS TrainingProgramId, 
                                        tp.Name AS TrainingProgramName
                                        FROM Employee e 
                                        LEFT JOIN Department d ON e.DepartmentId = d.Id
                                        LEFT JOIN ComputerEmployee ce ON ce.EmployeeId = e.id 
                                        LEFT JOIN Computer c ON c.id = ce.ComputerId 
                                        LEFT JOIN EmployeeTraining et ON et.EmployeeId = e.Id
                                        LEFT JOIN TrainingProgram tp ON tp.Id = et.TrainingProgramId 
                                        WHERE e.Id = @id";
                    cmd.Parameters.Add(new SqlParameter("@id", id));
                    SqlDataReader reader = cmd.ExecuteReader();

                    EmployeeDetailsViewModel model = null;

                    while (reader.Read())
                    {
                        if (model == null)
                        {
                            model = new EmployeeDetailsViewModel();
                            model.Employee = new Employee
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("EmployeeId")),
                                FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                LastName = reader.GetString(reader.GetOrdinal("LastName")),
                                Department = new Department
                                {
                                    Name = reader.GetString(reader.GetOrdinal("DepartmentName"))
                                }
                            };
                        }

                        if (!reader.IsDBNull(reader.GetOrdinal("ComputerId")))
                        {
                            model.AssignedComputer = new Computer
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("ComputerId")),
                                Make = reader.GetString(reader.GetOrdinal("Make")),
                                Manufacturer = reader.GetString(reader.GetOrdinal("Manufacturer"))
                            };
                        }

                        if (!reader.IsDBNull(reader.GetOrdinal("ComputerEmployeeId")) && (reader.IsDBNull(reader.GetOrdinal("UnassignDate"))))
                        {
                            model.ComputerEmployee = new ComputerEmployee
                            {
                                AssignDate = reader.GetDateTime(reader.GetOrdinal("AssignDate"))
                            };
                        }

                        if (!reader.IsDBNull(reader.GetOrdinal("TrainingProgramId")))
                        {
                            TrainingProgram trainingProgram = new TrainingProgram
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("TrainingProgramId")),
                                Name = reader.GetString(reader.GetOrdinal("TrainingProgramName"))
                            };

                            model.TrainingPrograms.Add(trainingProgram);
                        }
                    }
                    reader.Close();
                    return model;
                }
            }
        }
    }
}