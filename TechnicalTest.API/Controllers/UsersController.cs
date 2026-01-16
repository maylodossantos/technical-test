using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TechnicalTest.Application.UseCases.UCUser.CreateUser;
using TechnicalTest.Application.UseCases.UCUser.CreateUser;
using TechnicalTest.Application.UseCases.UCUser.DeleteUser;
using TechnicalTest.Application.UseCases.UCUser.GetAllUser;
using TechnicalTest.Application.UseCases.UCUser.UpdateUser;

namespace TechnicalTest.API.UsersControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<GetAllUserResponse>>> GetAll(CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetAllUserRequest(), cancellationToken);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserRequest request)
        {
            var userId = await _mediator.Send(request);
            return Ok(userId);
        }

        [HttpPatch("{id:guid}")]
        [Authorize]
        public async Task<ActionResult<UpdateUserResponse>> Update([FromRoute] Guid id, 
            [FromBody] UpdateUserRequest request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new UpdateUserCommand(id, request), cancellationToken);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> Delete(Guid? id, 
            CancellationToken cancellationToken)
        {
            if (id is null)
                return BadRequest();

            var deleteUserRequest = new DeleteUserRequest(id.Value);

            var response = await _mediator.Send(deleteUserRequest, cancellationToken);
            return Ok(response);
        }
    }
}
