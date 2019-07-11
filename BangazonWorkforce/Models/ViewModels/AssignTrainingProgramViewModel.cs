// Author Jonathan Schaffer 
// the view model is used for handling trainging programs that an employee can be assigned to
// Only will show programs that are active and have room for them




using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace BangazonWorkforce.Models.ViewModels
{
    public class AssignTrainingProgramViewModel
    {
        private string _connectionString;

        private SqlConnection Connection
        {
            get
            {
                return new SqlConnection(_connectionString);
            }
        }

        public Employee Employee { get; set; }

        public int SelectedValue { get; set; }

        public List<SelectListItem> TrainingProgramsSelectItems { get; set; }

        public AssignTrainingProgramViewModel(int id, string connectionString)
        {
            _connectionString = connectionString;

            TrainingProgramsSelectItems = GetAvailableTrainingPrograms(id)
                                                .Select(t => new SelectListItem(t.Name, t.Id.ToString()))
                                                .ToList();

            TrainingProgramsSelectItems.Insert(0, new SelectListItem("Choose a training program to assign this employee too", "0"));

        }

        public void AssignExercise()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO EmployeeTraining (EmployeeId, TrainingProgramId) VALUES (@EmployeeId, @TrainingProgramId)";
                    cmd.Parameters.Add(new SqlParameter("@EmployeeId", Employee.Id));
                    cmd.Parameters.Add(new SqlParameter("@TrainingProgramId", SelectedValue));

                    cmd.ExecuteNonQuery();



                }
            }
        }

        private List<TrainingProgram> GetAvailableTrainingPrograms(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT  tp.Id,
                                                tp.Name,
                                                tp.StartDate,
                                                tp.EndDate,
                                                tp.MaxAttendees
                                                FROM TrainingProgram tp
                                                JOIN EmployeeTraining et ON et.TrainingProgramId = tp.Id
                                                WHERE tp.StartDate > GETDATE()
                                                AND (et.EmployeeId != @id
                                                AND tp.Id NOT IN 
                                                (SELECT tp.Id FROM TrainingProgram tp
                                                JOIN EmployeeTraining et ON et.TrainingProgramId = tp.Id
                                                WHERE et.EmployeeId = @id));";

                    cmd.Parameters.Add(new SqlParameter("@id", id));


                    List<TrainingProgram> trainingPrograms = new List<TrainingProgram>();

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        if (!trainingPrograms.Any(t => t.Id == reader.GetInt32(reader.GetOrdinal("Id"))))
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
                    }

                    reader.Close();

                    return trainingPrograms;
                }
            }
        }
    }
}
