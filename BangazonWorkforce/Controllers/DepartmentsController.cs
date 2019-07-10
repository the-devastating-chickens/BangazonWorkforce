using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using BangazonWorkforce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace BangazonWorkforce.Controllers
{
    public class DepartmentsController : Controller
    {

        private readonly IConfiguration _config;

        public DepartmentsController(IConfiguration config)
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

        //////////////////////////////////////////CREATED BY ALEX -- will display departments with Name, Budget, and number of employees. had to use group by in sql query to perform a count on employees. had to add d.Budget to make it an aggregate function so i can include it inside the select statement. 

        // GET: Departments
        public ActionResult Index()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    //cmd.CommandText = @"SELECT Id, 
                    //                    Name, 
                    //                    Budget 
                    //                    FROM Department";

                    cmd.CommandText = @"SELECT 
                                        COUNT(e.FirstName) as NumberOfEmployees, 
                                        d.Name, 
                                        d.Budget 
                                        FROM Department d 
                                        JOIN Employee e ON e.DepartmentId = d.Id
                                        GROUP by d.Name, d.Budget";

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Department> departments = new List<Department>();

                    while (reader.Read())
                    {
                        Department department = new Department()
                        {
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            Budget = reader.GetInt32(reader.GetOrdinal("Budget")),
                            NumberOfEmployees = reader.GetInt32(reader.GetOrdinal("NumberOfEmployees"))
                        };
                        departments.Add(department);
                    }
                    reader.Close();
                    return View(departments);
                }

            }
        }

        private Department GetDepartmentById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"Select
                    d.Name,
                    d.Budget
                    FROM Department d
                    WHERE Id = @Id";

                    cmd.Parameters.Add(new SqlParameter("@Id", id));
                    SqlDataReader reader = cmd.ExecuteReader();

                    Department department = null;
                    if (reader.Read())
                    {
                        department = new Department
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            Budget = reader.GetInt32(reader.GetOrdinal("Budget"))
                        };
                    }
                    reader.Close();

                    return department;
                }
            }
        }

        private List<Employee> GetMatchingEmployees(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"Select Id, FirstName,
               LastName,
               DepartmentId
               FROM Employee
               WHERE DepartmentId = @Id;
             ";
                    cmd.Parameters.Add(new SqlParameter("@Id", id));
                    SqlDataReader reader = cmd.ExecuteReader();
                    List<Employee> employeeList = new List<Employee>();
                    while (reader.Read())
                    {
                        Employee employee = new Employee
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                            LastName = reader.GetString(reader.GetOrdinal("LastName")),
                            DepartmentId = reader.GetInt32(reader.GetOrdinal("DepartmentId"))
                        };
                        employeeList.Add(employee);
                    }
                    reader.Close();
                    return employeeList;
                }
            }
        }

        // GET: Departments/Details/5
        public ActionResult Details(int id)
        {
            Department department = GetDepartmentById(id);
            department.DepartmentEmployees = GetMatchingEmployees(id);
            return View(department);
        }

        // GET: Departments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Departments/Create
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

        // GET: Departments/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Departments/Edit/5
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

        // GET: Departments/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Departments/Delete/5
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
    }
}