using Application.Features.Dimplomas.Commands.Create;
using Application.Features.Dimplomas.Commands.Update;
using Application.Features.Dimplomas.Queries.GetById;
using Application.Features.Dimplomas.Queries.GetPagedList;
using Application.Features.Graduates.Commands.Create;
using Application.Features.Graduates.Commands.Update;
using Application.Features.Graduates.Queries.GetById;
using Application.Features.Graduates.Queries.GetPagedList;
using Application.Features.Students.Commands.Create;
using Application.Features.Students.Commands.Update;
using Application.Features.Students.Queries.GetById;
using Application.Features.Students.Queries.GetPagedList;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Dimploma, CreateDimplomaCommand>().ReverseMap();
            CreateMap<Dimploma, DimplomaViewModel>().ReverseMap();
            CreateMap<Dimploma, DimplomasViewModel>().ReverseMap();
            CreateMap<Dimploma, UpdateDimplomaCommand>().ReverseMap();

            CreateMap<Graduate, CreateGraduateCommand>().ReverseMap();
            CreateMap<Graduate, GraduateViewModel>().ReverseMap();
            CreateMap<Graduate, GraduatesViewModel>().ReverseMap();
            CreateMap<Graduate, UpdateGraduateCommand>().ReverseMap();
            
            CreateMap<Student, CreateStudentCommand>().ReverseMap();
            CreateMap<Student, StudentViewModel>().ReverseMap();
            CreateMap<Student, StudentsViewModel>().ReverseMap();
            CreateMap<Student, UpdateStudentCommand>().ReverseMap();

        }
    }
}
