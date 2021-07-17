﻿namespace DeemZ.Services.CourseServices
{
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DeemZ.Data;
    using DeemZ.Data.Models;

    public class CourseService : ICourseService
    {
        private readonly DeemZDbContext context;
        private readonly IMapper mapper;

        public CourseService(DeemZDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public int GetUserCredits(string id)
            => context
               .Users
               .Include(x => x.Exams)
               .FirstOrDefault(x => x.Id == id)
               .Exams.Sum(x => x.EarnedCredits);


        //Gets user's given id courses
        public IEnumerable<T> GetUserCurrentCourses<T>(string uid, bool isPaid = true)
         => context.UserCourses
                .Where(
                    x => x.User.Id == uid
                    && x.IsPaid == isPaid
                    && x.Course.EndDate > DateTime.UtcNow
                )
                .Include(x => x.Course)
                .Select(x => mapper.Map<T>(x.Course))
                .ToList();



        public T GetCourseById<T>(string uid)
            => context.Courses
                .Where(x => x.Id == uid)
                .Include(x => x.Lectures)
                .ThenInclude(x => x.Descriptions)
                .Include(c => c.Lectures)
                .ThenInclude(x => x.Resources)
                .ThenInclude(x => x.ResourceType)
                .Select(x => mapper.Map<T>(x))
                .FirstOrDefault();

        public bool IsUserSignUpForThisCourse(string uid, string cid)
            => context.UserCourses
                .Any(x => x.UserId == uid && x.CourseId == cid && x.IsPaid == true);

        public IEnumerable<T> GetCoursesResources<T>(string uid)
            => context.Resources
                .Include(x => x.Lecture)
                .ThenInclude(x => x.Course)
                .ThenInclude(x => x.UserCourses)
                .OrderBy(x => x.CreatedOn)
                .Where(x =>
                    x.Lecture.Course.UserCourses.Any(c =>
                        c.UserId == uid
                        && c.CourseId == x.Lecture.CourseId
                        && c.Course.StartDate <= DateTime.UtcNow
                        && c.Course.EndDate >= DateTime.UtcNow
                        && c.IsPaid == true))
                .Select(x => mapper.Map<T>(x))
                .ToList();

        public IEnumerable<T> GetCoursesForSignUp<T>()
            => context.Courses
            .Where(x =>
                x.SignUpStartDate <= DateTime.UtcNow
                && x.SignUpEndDate > DateTime.UtcNow)
            .Select(x => mapper.Map<T>(x))
            .ToList();

        public void SignUserToCourse(string uid, string cid)
        {
            var userCourse = new UserCourse()
            {
                CourseId = cid,
                UserId = uid,
                IsPaid = true,
                PaidOn = DateTime.UtcNow
            };

            context.UserCourses.Add(userCourse);
            context.SaveChanges();
        }

        public IEnumerable<T> GetCourses<T>()
            => context.UserCourses
                .Include(x => x.User)
                .Include(x => x.Course)
                .Select(x => mapper.Map<T>(x))
                .ToList();
    }
}