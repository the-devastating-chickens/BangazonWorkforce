/* Author: Billy Mathison
 * Purpose: Creating a controller for the TrainingProgram model. 
 * Methods: Index, Details, Create, Edit, Delete, DeleteConfirmed, and GetTrainingProgramById
 */

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
    public class TrainingProgramsController : Controller
    {
        private readonly IConfiguration _config;

        public TrainingProgramsController(IConfiguration config)
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

        // GET: TrainingProgram
        public ActionResult Index()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT t.Id,
                                        t.Name,
                                        t.StartDate,
                                        t.EndDate,
                                        t.MaxAttendees
                                        FROM TrainingProgram t
                                        WHERE t.EndDate > CURRENT_TIMESTAMP";
                    SqlDataReader reader = cmd.ExecuteReader();
                    List<TrainingProgram> trainingPrograms = new List<TrainingProgram>();

                    while (reader.Read())
                    {
                        TrainingProgram trainingProgram = new TrainingProgram
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            StartDate = reader.GetDateTime(reader.GetOrdinal("StartDate")),
                            EndDate = reader.GetDateTime(reader.GetOrdinal("EndDate")),
                            MaxAttendees = reader.GetInt32(reader.GetOrdinal("MaxAttendees"))
                        };
                        trainingPrograms.Add(trainingProgram);
                    }
                    reader.Close();
                    return View(trainingPrograms);
                }
            }
        }

        //GET: TrainingProgram (Past Training Programs)
        public ActionResult ViewPastPrograms()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT t.Id,
                                        t.Name,
                                        t.StartDate,
                                        t.EndDate,
                                        t.MaxAttendees
                                        FROM TrainingProgram t
                                        WHERE t.EndDate < CURRENT_TIMESTAMP";
                    SqlDataReader reader = cmd.ExecuteReader();
                    List<TrainingProgram> trainingPrograms = new List<TrainingProgram>();

                    while (reader.Read())
                    {
                        TrainingProgram trainingProgram = new TrainingProgram
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            StartDate = reader.GetDateTime(reader.GetOrdinal("StartDate")),
                            EndDate = reader.GetDateTime(reader.GetOrdinal("EndDate")),
                            MaxAttendees = reader.GetInt32(reader.GetOrdinal("MaxAttendees"))
                        };
                        trainingPrograms.Add(trainingProgram);
                    }
                    reader.Close();
                    return View(trainingPrograms);
                }
            }
        }

        // GET: TrainingProgram/Details/5
        public ActionResult Details(int id)
        {
            TrainingProgram trainingProgram = GetTrainingProgramById(id);
            return View(trainingProgram);
        }


        // GET: TrainingProgram/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TrainingProgram/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TrainingProgram trainingProgram)
        {
            try
            {
                using (SqlConnection conn = Connection)
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"INSERT INTO TrainingProgram 
                                            ( Name, StartDate, EndDate, MaxAttendees ) 
                                            VALUES ( @name, @startDate, @endDate, @maxAttendees )";
                        cmd.Parameters.Add(new SqlParameter("@name", trainingProgram.Name));
                        cmd.Parameters.Add(new SqlParameter("@startDate", trainingProgram.StartDate));
                        cmd.Parameters.Add(new SqlParameter("@endDate", trainingProgram.EndDate));
                        cmd.Parameters.Add(new SqlParameter("@maxAttendees", trainingProgram.MaxAttendees));

                        cmd.ExecuteNonQuery();
                    }
                }
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TrainingProgram/Edit/5
        public ActionResult Edit(int id)
        {
            TrainingProgram trainingProgram = GetTrainingProgramById(id);
            if (trainingProgram.EndDate > DateTime.Now)
            {
                return View(trainingProgram);
            }
            else
            {
                return StatusCode(403);
            }
        }

        // POST: TrainingProgram/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, TrainingProgram trainingProgram)
        {
            try
            {
                using (SqlConnection conn = Connection)
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"UPDATE TrainingProgram
                                            SET Name = @name,
                                            StartDate = @startDate,
                                            EndDate = @endDate,
                                            MaxAttendees = @maxAttendees
                                            WHERE Id = @id";
                        cmd.Parameters.Add(new SqlParameter("@name", trainingProgram.Name));
                        cmd.Parameters.Add(new SqlParameter("@startDate", trainingProgram.StartDate));
                        cmd.Parameters.Add(new SqlParameter("@endDate", trainingProgram.EndDate));
                        cmd.Parameters.Add(new SqlParameter("@maxAttendees", trainingProgram.MaxAttendees));
                        cmd.Parameters.Add(new SqlParameter("@id", id));

                        cmd.ExecuteNonQuery();
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TrainingProgram/Delete/5
        public ActionResult Delete(int id)
        {
            TrainingProgram trainingProgram = GetTrainingProgramById(id);
            if (trainingProgram.StartDate > DateTime.Now)
            {
                return View(trainingProgram);
            }
            else
            {
                return StatusCode(403);
            }
        }

        // POST: TrainingProgram/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                using (SqlConnection conn = Connection)
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "DELETE TrainingProgram WHERE Id = @id";
                        cmd.Parameters.Add(new SqlParameter("@id", id));

                        cmd.ExecuteNonQuery();
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private TrainingProgram GetTrainingProgramById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT tp.Id AS TrainingProgramId, tp.Name, tp.StartDate, tp.EndDate, tp.MaxAttendees, e.FirstName, e.LastName 
                                        FROM TrainingProgram tp 
                                        LEFT JOIN EmployeeTraining et on et.TrainingProgramId = tp.Id
                                        LEFT JOIN Employee e on e.Id = et.EmployeeId
                                        WHERE tp.Id = @id";
                    cmd.Parameters.Add(new SqlParameter("@id", id));

                    SqlDataReader reader = cmd.ExecuteReader();
                    TrainingProgram trainingProgram = null;

                    while (reader.Read())
                    {
                        if (trainingProgram == null)
                        {
                            trainingProgram = new TrainingProgram
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("TrainingProgramId")),
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                                StartDate = reader.GetDateTime(reader.GetOrdinal("StartDate")),
                                EndDate = reader.GetDateTime(reader.GetOrdinal("EndDate")),
                                MaxAttendees = reader.GetInt32(reader.GetOrdinal("MaxAttendees")),
                            };
                        }

                        if (!reader.IsDBNull(reader.GetOrdinal("FirstName")))
                        {
                            Employee employee = new Employee
                            {
                                FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                LastName = reader.GetString(reader.GetOrdinal("LastName"))
                            };

                            trainingProgram.EmployeesInTrainingProgram.Add(employee);
                        }
                    }
                    reader.Close();
                    return trainingProgram;
                }
            }
        }
    }
}