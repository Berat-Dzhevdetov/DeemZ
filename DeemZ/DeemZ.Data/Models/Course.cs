﻿namespace DeemZ.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Course : BaseModel
    {
        [Required]
        [MaxLength(DataConstants.Course.MaxNameLength)]
        public string Name { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public decimal Price { get; set; } = DataConstants.Course.DefaultPrice;

        public int Credits { get; set; }

        public DateTime SignUpStartDate { get; set; } = DateTime.UtcNow;
        public DateTime SignUpEndDate { get; set; } = DateTime.UtcNow.AddDays(14);

        public ICollection<Exam> Exams { get; set; } = new HashSet<Exam>();
        public ICollection<Lecture> Lectures { get; set; } = new HashSet<Lecture>();
        public ICollection<Survey> Surveys { get; set; } = new HashSet<Survey>();
        public ICollection<UserCourse> UserCourses { get; set; } = new HashSet<UserCourse>();
    }
}