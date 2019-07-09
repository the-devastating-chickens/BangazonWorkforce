/* Authors: Jonathan Schaffer, Billy Mathison
 * Purpose: Creating a controller for the Employee model. 
 * Methods: Index, Details, Create, Edit, and Delete. The Get Employees Methods gets all employees as well as the department they are in.

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
            Employee employee = GetEmployeeById(id);
            return View(employee);
        }

        ///////////////////////////created by alex -- for the GET i pass in a view model that contain a selectlistitem, i do this so i can create the drop down menu based on departments. i then do the post to insert into sql

        // GET: Employees/Create
        public ActionResult Create()
        {
            EmployeeCreateNewViewModel viewModel = new EmployeeCreateNewViewModel();
            viewModel.AvailableDepartments = GetDepartments();
            return View(viewModel);
        }

        // POST: Employees/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EmployeeCreateNewViewModel viewModel)
        {
            Employee employee = viewModel.Employee;
            try
            {
                // TODO: Add insert logic here
                using (SqlConnection conn = Connection)
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"INSERT INTO Employee (FirstName,LastName, IsSuperVisor, DepartmentId) VALUES (@FirstName, @LastName, @IsSuperVisor, @DepartmentId)";                      

                        cmd.Parameters.Add(new SqlParameter("@FirstName", employee.FirstName));
                        cmd.Parameters.Add(new SqlParameter("@LastName", employee.LastName));
                        cmd.Parameters.Add(new SqlParameter("@IsSuperVisor", employee.IsSuperVisor));
                        cmd.Parameters.Add(new SqlParameter("@DepartmentId", employee.DepartmentId));

                        cmd.ExecuteNonQuery();

                        return RedirectToAction(nameof(Index));


                    }
                }


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

<<<<<<< HEAD
        private Employee GetEmployeeById(int id)
=======
        private List<Department> GetDepartments ()
>>>>>>> master
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
<<<<<<< HEAD
                    cmd.CommandText = @"SELECT e.Id, e.FirstName, e.LastName, d.[Name]
                                        FROM Employee e 
                                        JOIN Department d ON e.DepartmentId = d.Id
                                        WHERE e.Id = @id";
                    cmd.Parameters.Add(new SqlParameter("@id", id));
                    SqlDataReader reader = cmd.ExecuteReader();

                    Employee employee = null;

                    if (reader.Read())
                    {
                        employee = new Employee
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                            LastName = reader.GetString(reader.GetOrdinal("LastName")),
                            Department = new Department
                            {
                                Name = reader.GetString(reader.GetOrdinal("Name"))
                            }
                        };
                    }
                    reader.Close();
                    return employee;
=======
                    cmd.CommandText = @"Select Id, Name FROM Department";

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Department> departments = new List<Department>();

                    while (reader.Read())
                    {
                        Department department = new Department()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name"))
                        };
                        departments.Add(department);

                    }
                    reader.Close();
                    return departments;
>>>>>>> master
                }
            }
        }
    }
}