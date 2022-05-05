using Application.Features.Graduates.Commands.Create;
using Application.Features.Graduates.Commands.Delete;
using Application.Features.Graduates.Commands.Update;
using Application.Features.Graduates.Queries.GetById;
using Application.Features.Graduates.Queries.GetPagedList;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace WebApi.Controllers.v1
{
    [AllowAnonymous]
    [ApiVersion("1.0")]
    public class GraduatesController : BaseApiController
    {
        //readonly IDiagnosticContext _diagnosticContext;
        public GraduatesController(){}


        // GET: api/<controller>
        /// <summary>
        /// return graduates that matche the criteria
        /// </summary>
        /// <param name="graduatesQuery"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetGraduatesQuery graduatesQuery)
        {
            var graduates = await Mediator.Send(graduatesQuery);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(graduates.MetaData));
            return Ok(graduates.PagedList);
        }


        // GET api/<controller>/5
        /// <summary>
        /// Retreives a specific Graduate.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await Mediator.Send(new GetGraduateByIdQuery { Id = id }));
        }


        // POST api/<controller>
        /// <summary>
        /// Creates a Graduate.
        /// </summary>
        /// <param name="command"></param>
        /// <returns>A newly created Graduate</returns>
        /// <response code="201">Returns the newly created command</response>
        /// <response code="400">If the command is null</response>            
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post(CreateGraduateCommand command)
        {
            return Ok(await Mediator.Send(command));
        }


        // PUT api/<controller>/5
        /// <summary>
        /// Update a specific Graduate.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, UpdateGraduateCommand command)
        {
            command.Id = id;
            return Ok(await Mediator.Send(command));
        }


        /// <summary>
        /// Deletes a specific Graduate.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await Mediator.Send(new DeleteGraduateCommand { Id = id }));
        }
    }
}
