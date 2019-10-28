using System;
using System.Collections.Generic;

namespace StudentExercisesNET.Models
{
    public class Student
    {

        // You must define a type for representing a student in code. A student can only be in one cohort at a time. A student can be working on many exercises at a time.

        // Properties
        // First name
        // Last name
        // Slack handle
        // The student's cohort
        // The collection of exercises that the student is currently working on

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SlackHandle { get; set; }
        public Cohort Cohort { get; set; }
        public List<Exercise> Exercises { get; set; }

        public Student(string firstName, string lastName, string slackHandle, Cohort cohort)
        {
            FirstName = firstName;
            LastName = lastName;
            SlackHandle = slackHandle;
            Cohort = cohort;
            Exercises = new List<Exercise>();
        }

    }
}