﻿namespace DeemZ.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System;
    using System.Linq;
    using DeemZ.Services.AdminServices;
    using DeemZ.Models.ViewModels.Administration;
    using DeemZ.Services;
    using DeemZ.Services.CourseServices;
    using DeemZ.Models.ViewModels.Resources;
    using DeemZ.Services.LectureServices;
    using DeemZ.Models.ViewModels.Lectures;
    using DeemZ.Services.UserServices;
    using DeemZ.Models.ViewModels.User;

    using static Constants;

    [Authorize(Roles = AdminRoleName)]
    public class AdministrationController : Controller
    {
        private readonly Guard guard;
        private readonly IAdminService adminService;
        private readonly ICourseService courseService;
        private readonly ILectureService lectureService;
        private readonly IUserService userService;

        public AdministrationController(Guard guard, IAdminService adminService, ICourseService courseService, ILectureService lectureService, IUserService userService)
        {
            this.guard = guard;
            this.adminService = adminService;
            this.courseService = courseService;
            this.lectureService = lectureService;
            this.userService = userService;
        }

        public IActionResult Index(int page = 1, int quantity = 20)
        {
            var viewModel = adminService.GetIndexPageInfo();

            var allPages = (int)Math.Ceiling(adminService.GetUserCoursesCount() / (quantity * 1.0));

            if (page <= 0 || page > allPages) page = 1;

            viewModel.UserCourses = adminService.GetUserCourses<UserCoursesViewModel>(page, quantity);

            viewModel = AdjustPages(viewModel, page, allPages);

            return View(viewModel);
        }

        public IActionResult Courses(int page = 1, int quantity = 20)
        {
            var viewModel = new AdministrationCoursesViewModel();

            var allPages = (int)Math.Ceiling(adminService.GetUserCoursesCount() / (quantity * 1.0));

            if (page <= 0 || page > allPages) page = 1;

            viewModel.Courses = (List<CoursesViewModel>)adminService.GetAllCourses<CoursesViewModel>(page, quantity);

            foreach (var course in viewModel.Courses)
            {
                course.SignedUpUsers = adminService.GetUserSignedUpForCourse(course.Id);
            }

            viewModel = AdjustPages(viewModel, page, allPages);

            return View(viewModel);
        }

        public IActionResult Resources(string lectureId, int page = 1, int quantity = 20)
        {
            if (guard.AgainstNull(lectureId, nameof(lectureId))) return BadRequest();

            if (!lectureService.GetLectureById(lectureId)) return NotFound();

            var resources = lectureService.GetLectureResourcesById<IndexResourceViewModel>(lectureId);

            var allPages = (int)Math.Ceiling(resources.Count() / (quantity * 1.0));

            if (page <= 0 || page > allPages) page = 1;

            var viewModel = new ResourcesForCourseViewModel();

            viewModel.Recourses = resources.Paging(page, quantity).ToList();

            viewModel = AdjustPages(viewModel, page, allPages);

            viewModel.LectureId = lectureId;

            return View(viewModel);
        }

        public IActionResult Lectures(string courseId, int page = 1, int quantity = 20)
        {
            if (guard.AgainstNull(courseId, nameof(courseId))) return BadRequest();

            if (!courseService.GetCourseById(courseId)) return NotFound();

            var lectures = lectureService.GetLecturesByCourseId<LectureBasicInformationViewModel>(courseId);

            var allPages = (int)Math.Ceiling(lectures.Count() / (quantity * 1.0));

            if (page <= 0 || page > allPages) page = 1;

            var viewModel = new IndexLecturesViewModel();

            viewModel.CourseId = courseId;

            viewModel.Lectures = lectures.Paging(page, quantity).ToList();

            viewModel = AdjustPages(viewModel, page, allPages);

            return View(viewModel);
        }

        public IActionResult Users(int page = 1, int quantity = 20)
        {
            var viewModel = new AdmistrationUsersViewModel();

            var users = userService.GetAllUsers<BasicUserInformationViewModel>(page,quantity);

            var allPages = (int)Math.Ceiling(users.Count() / (quantity * 1.0));

            if (page <= 0 || page > allPages) page = 1;

            viewModel.Users = users;

            viewModel = AdjustPages(viewModel, page, allPages);

            foreach(var user in viewModel.Users)
            {
                user.TakenCoursesCount = userService.GetUserTakenCourses(user.Id);
            }

            return View(viewModel);
        }

        private T AdjustPages<T>(T viewModel, int page, int allPages)
            where T : PagingBaseModel
        {
            viewModel.CurrentPage = page;
            viewModel.NextPage = page >= allPages ? null : page + 1;
            viewModel.PreviousPage = page <= 1 ? null : page - 1;
            viewModel.MaxPages = allPages;

            return viewModel;
        }
    }
}