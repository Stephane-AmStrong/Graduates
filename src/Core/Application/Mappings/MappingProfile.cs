using Application.Features.Diplomas.Commands.Create;
using Application.Features.Diplomas.Commands.Update;
using Application.Features.Diplomas.Queries.GetById;
using Application.Features.Diplomas.Queries.GetPagedList;
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
            CreateMap<Diploma, CreateDiplomaCommand>().ReverseMap();
            CreateMap<Diploma, DiplomaViewModel>().ReverseMap();
            CreateMap<Diploma, DiplomasViewModel>().ReverseMap();
            CreateMap<Diploma, UpdateDiplomaCommand>().ReverseMap();

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
