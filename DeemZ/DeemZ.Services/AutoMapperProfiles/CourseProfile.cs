﻿namespace DeemZ.Services.AutoMapperProfiles
{
    using AutoMapper;
    using DeemZ.Data.Models;
    using DeemZ.Models.ViewModels.Course;

    public class CourseProfile : Profile
    {
        public CourseProfile()
        {
            CreateMap<Course, IndexCourseViewModel>()
                .ForMember(x => x.Name, o => o.MapFrom(src => src.Name))
                .ForMember(x => x.Id, o => o.MapFrom(src => src.Id));

            CreateMap<Course, DetailsCourseViewModel>()
                .ForMember(x => x.Lectures, o => o.MapFrom(src => src.Lectures))
                .ForMember(x => x.Credits, o => o.MapFrom(src => src.Credits));

            CreateMap<Course, IndexSignUpForCourseViewModel>()
                .ForMember(x => x.SignUpStartDate, o => o.MapFrom(src => src.SignUpStartDate.ToString("dd/MM/yyyy")))
                .ForMember(x => x.SignUpEndDate, o => o.MapFrom(src => src.SignUpEndDate.ToString("dd/MM/yyyy")));
        }
    }
}
