using Application.Features.Diplomas.Commands.Create;
using Application.Features.Diplomas.Commands.Delete;
using Application.Features.Diplomas.Commands.Update;
using Application.Features.Diplomas.Queries.GetById;
using Application.Features.Diplomas.Queries.GetPagedList;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace WebApi.Controllers.v1
{
    [AllowAnonymous]
    [ApiVersion("1.0")]
    public class DiplomasController : BaseApiController
    {
        //readonly IDiagnosticContext _diagnosticContext;
        public DiplomasController(){}


        // GET: api/<controller>
        /// <summary>
        /// return diplomas that matche the criteria
        /// </summary>
        /// <param name="diplomasQuery"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetDiplomasQuery diplomasQuery)
        {
            var diplomas = await Mediator.Send(diplomasQuery);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(diplomas.MetaData));
            return Ok(diplomas.PagedList);
        }


        // GET api/<controller>/5
        /// <summary>
        /// Retreives a specific Diploma.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await Mediator.Send(new GetDiplomaByIdQuery { Id = id }));
        }


        // POST api/<controller>
        /// <summary>
        /// Creates a Diploma.
        /// </summary>
        /// <param name="command"></param>
        /// <returns>A newly created Diploma</returns>
        /// <response code="201">Returns the newly created command</response>
        /// <response code="400">If the command is null</response>            
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post(CreateDiplomaCommand command)
        {
            return Ok(await Mediator.Send(command));
        }


        // PUT api/<controller>/5
        /// <summary>
        /// Update a specific Diploma.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, UpdateDiplomaCommand command)
        {
            command.Id = id;
            return Ok(await Mediator.Send(command));
        }


        /// <summary>
        /// Deletes a specific Diploma.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await Mediator.Send(new DeleteDiplomaCommand { Id = id }));
        }
    }
}
