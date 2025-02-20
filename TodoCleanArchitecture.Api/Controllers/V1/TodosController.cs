using MediatR;
using Microsoft.AspNetCore.Mvc;
using TodoCleanArchitecture.Api.Attributes;
using TodoCleanArchitecture.Api.Base.V1;
using TodoCleanArchitecture.Application.Features.Todos.Commands.Create;
using TodoCleanArchitecture.Application.Features.Todos.Commands.Delete;
using TodoCleanArchitecture.Application.Features.Todos.Commands.Update;
using TodoCleanArchitecture.Application.Features.Todos.Queries.Get;
using TodoCleanArchitecture.Application.Features.Todos.Queries.GetAll;
using TodoCleanArchitecture.Application.Models.Common;

namespace TodoCleanArchitecture.Api.Controllers.V1;

public class TodosController(ISender sender) : ApiController
{
    

    [HttpGet("Get/{id:int}")]
    [ProducesOkApiResponseType<GetQueryResult>]
    public async Task<IActionResult> Get(int id)
    {
        var query = await sender.Send(new GetQuery(id));
        return base.OperationResult(query);
    }


    [HttpGet("GetAll")]
    [ProducesOkApiResponseType<PaginatedList<GetAllQueryResult>>]
    public async Task<IActionResult> GetAll([FromQuery] GetAllQuery query)
    {
        var result = await sender.Send(query);
        return base.OperationResult(result);
    }


    [HttpPost("Create")]
    [ProducesOkApiResponseType]
    public async Task<IActionResult> Create(CreateCommand model)
    {
        var command = await sender.Send(model);
        return base.OperationResult(command);
    }

    [HttpPut("Update")]
    [ProducesOkApiResponseType]
    public async Task<IActionResult> Update(UpdateCommand model)
    {
        var command = await sender.Send(model);
        return base.OperationResult(command);
    }

    [HttpDelete("Delete")]
    [ProducesOkApiResponseType]
    public async Task<IActionResult> Delete(DeleteCommand model)
    {
        var command = await sender.Send(model);
        return base.OperationResult(command);
    }

}
