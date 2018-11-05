using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dapper;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using BangazonWorkforce.Models;

namespace BangazonWorkforce.Controllers
{
    public class DepartmentController : Controller
    {
        private IConfiguration _config;
        private IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }

        public DepartmentController(IConfiguration config)
        {
            _config = config;
        }

        public async Task<IActionResult> Index()
        {
            using (IDbConnection conn = Connection)
            {
                string sql = "SELECT Id, Name, Budget FROM Department";
                IEnumerable<Department> departments = await conn.QueryAsync<Department>(sql);

                return View(departments);
            }
        }

        public async Task<IActionResult> Details(int? id) 
        {
            if (id == null)
            {
                return NotFound();
            }

            Department department = await GetById(id.Value);
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }

        // GET: Department/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Department/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name, Budget")] Department department)
        {
            if (!ModelState.IsValid)
            {
                return View(department);
            }

            using (IDbConnection conn = Connection)
            {
                string sql = $@"INSERT INTO Department (Name, Budget) 
                                     VALUES ('{department.Name}', {department.Budget});";

                await conn.ExecuteAsync(sql);
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Department/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Department department = await GetById(id.Value);
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }

        // POST: Department/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Department department)
        {
            if (id != department.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(department);
            }

            using (IDbConnection conn = Connection)
            {
                string sql = $@"UPDATE Department 
                                   SET Name = '{department.Name}', 
                                       Budget = {department.Id}
                                 WHERE id = {id}";

                await conn.ExecuteAsync(sql);
                return RedirectToAction(nameof(Index));
            }
        }


        // GET: Department/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Department department = await GetById(id.Value);
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }

        // POST: Department/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            using (IDbConnection conn = Connection)
            {
                string sql = $@"DELETE FROM Department WHERE id = {id}";
                int rowsDeleted = await conn.ExecuteAsync(sql);
                
                if (rowsDeleted > 0)
                {
                    return NotFound();
                }

                return RedirectToAction(nameof(Index));
            }
        }


        private async Task<Department> GetById(int id)
        {
            using (IDbConnection conn = Connection)
            {
                string sql = $@"SELECT Id, Name, Budget 
                                  FROM Department
                                 WHERE id = {id}";

                IEnumerable<Department> departments = await conn.QueryAsync<Department>(sql);
                return departments.SingleOrDefault();
            }
        }
    }
}