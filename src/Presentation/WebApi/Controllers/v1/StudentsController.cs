using Application.Features.Students.Commands.Create;
using Application.Features.Students.Commands.Delete;
using Application.Features.Students.Commands.Update;
using Application.Features.Students.Queries.GetById;
using Application.Features.Students.Queries.GetPagedList;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace WebApi.Controllers.v1
{
    [AllowAnonymous]
    [ApiVersion("1.0")]
    public class StudentsController : BaseApiController
    {
        //readonly IDiagnosticContext _diagnosticContext;
        public StudentsController(){}


        // GET: api/<controller>
        /// <summary>
        /// return students that matche the criteria
        /// </summary>
        /// <param name="studentsQuery"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetStudentsQuery studentsQuery)
        {
            var students = await Mediator.Send(studentsQuery);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(students.MetaData));
            return Ok(students.PagedList);
        }


        // GET api/<controller>/5
        /// <summary>
        /// Retreives a specific Student.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await Mediator.Send(new GetStudentByIdQuery { Id = id }));
        }


        // POST api/<controller>
        /// <summary>
        /// Creates a Student.
        /// </summary>
        /// <param name="command"></param>
        /// <returns>A newly created Student</returns>
        /// <response code="201">Returns the newly created command</response>
        /// <response code="400">If the command is null</response>            
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post(CreateStudentCommand command)
        {
            return Ok(await Mediator.Send(command));
        }


        // PUT api/<controller>/5
        /// <summary>
        /// Update a specific Student.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, UpdateStudentCommand command)
        {
            command.Id = id;
            return Ok(await Mediator.Send(command));
        }


        /// <summary>
        /// Deletes a specific Student.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await Mediator.Send(new DeleteStudentCommand { Id = id }));
        }
    }
}
