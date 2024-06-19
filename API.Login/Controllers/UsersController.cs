using System.ComponentModel.DataAnnotations;
using API.Login.Domain.Dtos.Request;
using API.Login.Domain.Entities;
using API.Login.Domain.Interfaces.Email;
using API.Login.Service.Users;
using Microsoft.AspNetCore.Mvc;

namespace API.Login.Controllers.Users;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IEmailService _emailService;
    public UsersController(IUserService userService, IEmailService emailService)
    {
        _emailService = emailService;
        _userService = userService;
    }

    [HttpPost]
    [Route("/AddUser")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AddUserAsync([FromBody][Required] User user)
    {
        var response = await _userService.AddAsync(user);

        return StatusCode(response.StatusCode, response.ResponseObject);
    }

    [HttpGet]
    [Route("/GetUsers")]
    [ProducesResponseType(typeof(List<User>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status503ServiceUnavailable)]
    public async Task<IActionResult> GetUsersAsync([FromQuery] int? id)
    {
        var response = await _userService.GetByIdAsync(id);

        return StatusCode(response.StatusCode, response.ResponseObject);
    }

    [HttpPut]
    [Route("/UpdateUser")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status503ServiceUnavailable)]
    public async Task<IActionResult> UpdateUserAsync(
        [FromQuery] int id,
        [FromForm][Required] UserUpdateDto user)
    {
        var response = await _userService.UpdateAsync(id, user);

        return StatusCode(response.StatusCode, response.ResponseObject);
    }

    [HttpDelete]
    [Route("/DeleteUser")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status503ServiceUnavailable)]
    public async Task<IActionResult> DeleteUserAsync([FromQuery] int? id)
    {
        var response = await _userService.DeleteAsync(id);

        return StatusCode(response.StatusCode, response.ResponseObject);
    }

    [HttpPost]
    [Route("/RegisterUser")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status503ServiceUnavailable)]
    public async Task<IActionResult> RegisterUser([FromBody][Required] UserRegisterDto user)
    {
        var response = await _userService.RegisterUserAsync(user);

        return StatusCode(response.StatusCode, response.ResponseObject);
    }

    [HttpPost]
    [Route("/ConfirmUserRegistration")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status503ServiceUnavailable)]
    public async Task<IActionResult> ConfirmUserRegistration([FromBody][Required] UserRegisterConfirmationDto user)
    {
        var response = await _userService.ConfirmUserRegistrationAsync(user);

        return StatusCode(response.StatusCode, response.ResponseObject);
    }

}
