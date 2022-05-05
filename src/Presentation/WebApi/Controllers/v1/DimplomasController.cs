using Application.Features.Dimplomas.Commands.Create;
using Application.Features.Dimplomas.Commands.Delete;
using Application.Features.Dimplomas.Commands.Update;
using Application.Features.Dimplomas.Queries.GetById;
using Application.Features.Dimplomas.Queries.GetPagedList;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace WebApi.Controllers.v1
{
    [AllowAnonymous]
    [ApiVersion("1.0")]
    public class DimplomasController : BaseApiController
    {
        //readonly IDiagnosticContext _diagnosticContext;
        public DimplomasController(){}


        // GET: api/<controller>
        /// <summary>
        /// return dimplomas that matche the criteria
        /// </summary>
        /// <param name="dimplomasQuery"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetDimplomasQuery dimplomasQuery)
        {
            var dimplomas = await Mediator.Send(dimplomasQuery);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(dimplomas.MetaData));
            return Ok(dimplomas.PagedList);
        }


        // GET api/<controller>/5
        /// <summary>
        /// Retreives a specific Dimploma.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await Mediator.Send(new GetDimplomaByIdQuery { Id = id }));
        }


        // POST api/<controller>
        /// <summary>
        /// Creates a Dimploma.
        /// </summary>
        /// <param name="command"></param>
        /// <returns>A newly created Dimploma</returns>
        /// <response code="201">Returns the newly created command</response>
        /// <response code="400">If the command is null</response>            
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post(CreateDimplomaCommand command)
        {
            return Ok(await Mediator.Send(command));
        }


        // PUT api/<controller>/5
        /// <summary>
        /// Update a specific Dimploma.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, UpdateDimplomaCommand command)
        {
            command.Id = id;
            return Ok(await Mediator.Send(command));
        }


        /// <summary>
        /// Deletes a specific Dimploma.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await Mediator.Send(new DeleteDimplomaCommand { Id = id }));
        }
    }
}
