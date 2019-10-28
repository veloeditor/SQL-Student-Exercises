using System;
using System.Collections.Generic;
using System.Text;

namespace StudentExercisesNET.Models
{
    public class Exercise
    {

        // You must define a type for representing an exercise in code. An exercise can be assigned to many students.
        // Name of exercise
        // Language of exercise (JavaScript, Python, CSharp, etc.)

        public int Id { get; set; }
        
        public string Name { get; set; }
        public string CodeLanguage { get; set; }

        public Exercise(int id, string exerciseName, string codeLanguage)
        {
            Id = id;
            Name = exerciseName;
            CodeLanguage = codeLanguage;
        }
    }
}
