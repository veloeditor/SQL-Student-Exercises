using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using StudentExercisesNET.Models;
using System.Text;

namespace StudentExercisesNET.Data
{
    class Repository
    {
        ///Initial setup code to connect to the database
        public SqlConnection Connection
        {
            get
            {
                // This is the "address" of the database
                string _connectionString = "Data Source=DESKTOP-1J6LR9L\\SQLEXPRESS;Initial Catalog=StudentExercises;Integrated Security=True";
                return new SqlConnection(_connectionString);
            }
        }

        /************************************************************************************
         * Exercises
         ************************************************************************************/

        //Query the database for all the Exercises
        public List<Exercise> GetAllExercises()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT Id, Name, CodeLanguage FROM Exercise";
                    SqlDataReader reader = cmd.ExecuteReader();

                    Exercise exercise = null;
                    List<Exercise> exercises = new List<Exercise>();

                    while (reader.Read())
                    {
                        exercise = new Exercise(reader.GetInt32(reader.GetOrdinal("Id")),
                                                reader.GetString(reader.GetOrdinal("Name")),
                                                reader.GetString(reader.GetOrdinal("CodeLanguage")));

                        exercises.Add(exercise);
                    }

                    reader.Close();
                    return exercises;
                }
            }
        }

        //Find all the exercises in the database where the language is JavaScript.
        public List<Exercise> GetExercisesByLanguage(string language)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT Id, Name, CodeLanguage FROM Exercise WHERE CodeLanguage = @language";
                    cmd.Parameters.Add(new SqlParameter("@language", language));
                    SqlDataReader reader = cmd.ExecuteReader();

                    Exercise exercise = null;
                    List<Exercise> exercises = new List<Exercise>();

                    while (reader.Read())
                    {
                        exercise = new Exercise(reader.GetInt32(reader.GetOrdinal("Id")),
                                                reader.GetString(reader.GetOrdinal("Name")),
                                                reader.GetString(reader.GetOrdinal("CodeLanguage")));

                        exercises.Add(exercise);
                    }

                    reader.Close();
                    return exercises;
                }
            }
        }

        //Add a new exercise:
        public void AddExercise(Exercise exercise)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using SqlCommand cmd = conn.CreateCommand();
                {
                    cmd.CommandText = "INSERT INTO Exercise (Name, CodeLanguage)" +
                                      "VALUES (@name, @codeLanguage)";
                    cmd.Parameters.Add(new SqlParameter("@name", exercise.Name));
                    cmd.Parameters.Add(new SqlParameter("@CodeLanguage", exercise.CodeLanguage));
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /************************************************************************************
         * Instructor
         ************************************************************************************/

        //Find all instructors in the database. Include each instructor's cohort.
        // SELECT s.FirstName, s.LastName, c.Name
        //--FROM Students s LEFT JOIN Cohorts c on s.CohortId = c.Id;

        public List<Instructor> GetAllInstructors()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using SqlCommand cmd = conn.CreateCommand();
                {
                    cmd.CommandText = @"SELECT i.FirstName, i.LastName, c.Name as CohortName
                                          FROM Instructors i LEFT JOIN Cohorts c on i.CohortId = c.Id";
                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Instructor> instructors = new List<Instructor>();

                    while (reader.Read())
                    {
                        Instructor instructor = new Instructor(
                           reader.GetInt32(reader.GetOrdinal("Id")),
                           reader.GetString(reader.GetOrdinal("FirstName")),
                           reader.GetString(reader.GetOrdinal("LastName")),
                           reader.GetString(reader.GetOrdinal("SlackHandle")),
                           
                           new Cohort(reader.GetInt32(reader.GetOrdinal("CohortId")),
                                       reader.GetString(reader.GetOrdinal("Name"))),
                           
                           reader.GetString(reader.GetOrdinal("Specialty")));

                        instructors.Add(instructor);

                    }

                    reader.Close();
                    return instructors;
                }
            }
        }

       // Insert a new instructor into the database.Assign the instructor to an existing cohort.
       public void AddInstructorToExistingCohort(Instructor instructor)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using SqlCommand cmd = conn.CreateCommand();
                {
                    cmd.CommandText = "INSERT INTO Instructor (Firstname, LastName, SlackHandle, CohortId, Speciality)" +
                                        "VALUES (@firstName, @lastName, @slackHandle, @cohortId, @speciality)";
                    cmd.Parameters.Add(new SqlParameter("@firstName", instructor.FirstName));
                    cmd.Parameters.Add(new SqlParameter("@lastName", instructor.LastName));
                    cmd.Parameters.Add(new SqlParameter("@slackHandle", instructor.SlackHandle));
                    cmd.Parameters.Add(new SqlParameter("@cohortId", instructor.Cohort.Id));
                    cmd.Parameters.Add(new SqlParameter("@speciality", instructor.Specialty));

                    cmd.ExecuteNonQuery();

                }
            }
        }



    }
}