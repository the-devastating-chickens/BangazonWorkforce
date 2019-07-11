// Author:  Jonathan
// The Get  Employees Methods gets all employees as well as the department they are in





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
                                        JOIN Department d ON e.DepartmentId = d.Id";

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
            return View();
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
            EmployeeEditViewModel viewModel = new EmployeeEditViewModel();

            viewModel.AvailableDepartments = GetDepartments();


            if(GetCurrentComputer(id) != null)
            {
            viewModel.CurrentComputerId = GetCurrentComputer(id).Id;

            }

            viewModel.Employee = GetEmployee(id);
            viewModel.AvailableComputers = GetComputers(id);
            viewModel.Computer = GetCurrentComputer(id);

            return View(viewModel);
        }

        //POST: Employees/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, EmployeeEditViewModel viewModel)
        {
            Employee employee = viewModel.Employee;
            viewModel.Computer = GetCurrentComputer(id);
            try
            {
                // TODO: Add update logic here
                using (SqlConnection conn = Connection)
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"UPDATE Employee SET LastName = @LastName, 
                                            DepartmentId = @DepartmentId
                                            WHERE Id = @id";

                        cmd.Parameters.Add(new SqlParameter("@LastName", employee.LastName));
                        cmd.Parameters.Add(new SqlParameter("@DepartmentId", employee.DepartmentId));
                        cmd.Parameters.Add(new SqlParameter("@id", id));

                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();

                        if(viewModel.Computer != null)
                        {
                        cmd.CommandText = @"UPDATE ComputerEmployee SET 
                                                EmployeeId = @EmployeeId, 
                                                ComputerId = @ComputerId,
                                                UnassignDate = @UnassignDate
                                                WHERE EmployeeId = @id";

                        cmd.Parameters.Add(new SqlParameter("@EmployeeId", id));
                        cmd.Parameters.Add(new SqlParameter("@ComputerId", viewModel.Computer.Id));
                        cmd.Parameters.Add(new SqlParameter("@UnassignDate", DateTime.Now.ToString()));
                        cmd.Parameters.Add(new SqlParameter("@id", id));

                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();

                        }

                        if (viewModel.CurrentComputerId != null)
                        {
                            cmd.CommandText = "INSERT INTO ComputerEmployee (EmployeeId, ComputerId, AssignDate) VALUES (@EmployeeId, @ComputerId, @AssignDate)";

                        cmd.Parameters.Add(new SqlParameter("@EmployeeId", id));
                        cmd.Parameters.Add(new SqlParameter("@ComputerId", viewModel.CurrentComputerId));
                        cmd.Parameters.Add(new SqlParameter("@AssignDate", DateTime.Now.ToString()));

                        cmd.ExecuteNonQuery();

                        }


                    }
                        return RedirectToAction(nameof(Index));
                }
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

        private List<Department> GetDepartments ()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
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
                }
            }
        }

        private List<Computer> GetComputers (int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                 

                    cmd.CommandText = @"select c.Id, 
                                        c.Make from Computer c
                                        left JOIN ComputerEmployee ce on ce.ComputerId = c.Id
                                        where ce.ComputerId is null and c.DecomissionDate 
                                        is null or (ce.ComputerId not in (SELECT ComputerId from ComputerEmployee where UnassignDate is null) and c.DecomissionDate is null) 
                                        or (ce.EmployeeId = @id and ce.UnassignDate is null)";

                    cmd.Parameters.Add(new SqlParameter("@id", id));
                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Computer> computers = new List<Computer>();

                    var dictionary = new Dictionary<int, Computer>();

                    while (reader.Read())
                    {
                        if (!dictionary.ContainsKey(reader.GetInt32(reader.GetOrdinal("Id"))))
                        {
                            Computer computer = new Computer()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Make = reader.GetString(reader.GetOrdinal("Make"))
                            };
                            dictionary.Add(computer.Id, computer);
                            computers.Add(dictionary[reader.GetInt32(reader.GetOrdinal("Id"))]);
                        }

                    }

                    reader.Close();
                    return computers;
                }
            }
        }

        private Computer GetCurrentComputer (int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"select e.Id, 
                                            e.FirstName, 
                                            c.Id as computerId,
                                            c.Make
                                            FROM Employee e 
                                            JOIN ComputerEmployee ce on e.Id = ce.EmployeeId
                                            join Computer c on c.Id = ce.ComputerId
                                            WHERE e.Id = @id and ce.UnassignDate is null";

                    cmd.Parameters.Add(new SqlParameter("@id", id));
                    SqlDataReader reader = cmd.ExecuteReader();

                    Computer computer = null;

                    if (reader.Read())
                    {
                        computer = new Computer()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("computerId")),
                            Make = reader.GetString(reader.GetOrdinal("Make"))

                        };
                        
                    }

                    reader.Close();

                   
                    return computer;

                }
            }
        }

        private Employee GetEmployee (int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"select FirstName, LastName, DepartmentId from Employee where Id = @id";

                    cmd.Parameters.Add(new SqlParameter("@id", id));
                    SqlDataReader reader = cmd.ExecuteReader();

                    Employee employee = null;

                    if (reader.Read())
                    {
                        employee = new Employee()
                        {
                            FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                            LastName = reader.GetString(reader.GetOrdinal("LastName")),
                            DepartmentId = reader.GetInt32(reader.GetOrdinal("DepartmentId"))
                        };
                    }

                    return employee;


                }
            }
        }
    }
}