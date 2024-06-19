using API.Login.Domain.Dtos.Request;
using API.Login.Domain.Dtos.Response;
using API.Login.Domain.Entities;
using API.Login.Domain.Interfaces.Email;
using API.Login.Infra.Users;
using API.Login.Utils;

namespace API.Login.Service.Users;

public class UserService : IUserService
{
    private readonly ControllerMessenger _controllerMessenger = new();
    private readonly IUserRepository _userRepository;
    private readonly IEmailService _emailService;

    public UserService(
        IUserRepository userRepository,
        IEmailService emailService)
    {
        _userRepository = userRepository;
        _emailService = emailService;
    }

    public async Task<ControllerMessenger> ConfirmUserRegistrationAsync(UserRegisterConfirmationDto user)
    {
        try
        {
            var existentUser = await _userRepository.GetAsync(x => x.EmailHash == user.EmailHash);
            if (existentUser is null)
                return _controllerMessenger.ReturnNotFound404("User not found");

            return await SendRegisterConfirmationEmail(existentUser.Email);
        }
        catch (System.Exception ex)
        {
            return _controllerMessenger.ReturnInternalError500(ex.Message);
        }
    }

    public async Task<ControllerMessenger> AddAsync(User user)
    {
        try
        {
            await _userRepository.AddAsync(user);
            var userSaved = await _userRepository.GetByIdAsync(user.UserId);
            if (userSaved is null)
                return _controllerMessenger.ReturnServiceUnavailable503();
            else
                return _controllerMessenger.ReturnSuccess(201, "Object Created");
        }
        catch (System.Exception ex)
        {
            return _controllerMessenger.ReturnInternalError500(ex.Message);
        }
    }

    public async Task<ControllerMessenger> GetByIdAsync(int? id)
    {
        try
        {
            var usersFound = new List<User>();
            if (id is null)
            {
                usersFound = await _userRepository.GetListAsync(null);
            }
            else
            {
                var userInformed = await _userRepository.GetByIdAsync((int)id);
                if (userInformed is not null)
                    usersFound.Add(userInformed);
            }

            if (id is not null && (usersFound is null || usersFound.Count() == 0))
                return _controllerMessenger.ReturnNotFound404($"user with id '{id}'");
            else if (id is null && (usersFound is null || usersFound.Count() == 0))
                return _controllerMessenger.ReturnNotFound404();
            else if (usersFound is not null && usersFound.Count() > 0)
                return _controllerMessenger.ReturnSuccess(200, usersFound);
            else
                return _controllerMessenger.ReturnServiceUnavailable503();
        }
        catch (System.Exception ex)
        {
            return _controllerMessenger.ReturnInternalError500(ex.Message);
        }
    }

    //Ajustar após as atualizações de password
    public async Task<ControllerMessenger> UpdateAsync(int id, object userUpdated)
    {
        try
        {
            var userSerialized = (UserUpdateDto)userUpdated;
            var userInformed = await _userRepository.GetByIdAsync(id);
            if (userInformed is null)
                return _controllerMessenger.ReturnNotFound404($"User with id '{id}'");

            userInformed.UserName = (userSerialized.NewUserName is not null)
                ? userSerialized.NewUserName
                : userInformed.UserName;

            userInformed.Email = (userSerialized.NewEmail is not null)
                ? userSerialized.NewEmail
                : userInformed.Email;

            userInformed.PassWordHash = (userSerialized.NewPassWord is not null)
                ? null
                : userInformed.PassWordHash;

            await _userRepository.UpdateAsync(userInformed);
            return _controllerMessenger.ReturnSuccess(200, "User Updated.");

        }
        catch (System.Exception ex)
        {
            return _controllerMessenger.ReturnInternalError500(ex.Message);
        }
    }

    public async Task<ControllerMessenger> DeleteAsync(int? id)
    {
        try
        {
            User? userInformed = null;
            if (id is not null)
            {
                userInformed = await _userRepository.GetByIdAsync((int)id);
                if (userInformed is null)
                    return _controllerMessenger.ReturnNotFound404($"user with id '{id}'");
            }

            if (userInformed is not null)
            {
                await _userRepository.DeleteAsync(userInformed);
                return _controllerMessenger.ReturnSuccess(200, $"user with id '{id}' deleted");

            }
            else
            {
                await _userRepository.DeleteAllAsync();
                return _controllerMessenger.ReturnSuccess(200, "All records deleted");
            }

        }
        catch (System.Exception ex)
        {
            return _controllerMessenger.ReturnInternalError500(ex.Message);
        }
    }

    public async Task<ControllerMessenger> RegisterUserAsync(UserRegisterDto user)
    {
        try
        {
            var userWithSameEmail = await _userRepository.GetAsync(u => u.Email == user.Email);

            if (userWithSameEmail is not null)
            {
                return _controllerMessenger.ReturnBadRequest400("The informed email is already taken.");
            }

            user.InitializeComputedPassWordAndHash();
            if (user.PassWordHash is null)
                return _controllerMessenger.ReturnInternalError500("Intern Error");

            if (user.PassWordSalt is null)
                return _controllerMessenger.ReturnInternalError500("Intern Error");


            var transaction = await _userRepository.BeginTransactionAsync();

            await _userRepository.AddAsync(new User(
                user.UserName,
                user.Email,
                user.PassWordHash,
                user.PassWordSalt));

            var emailResult = await SendRegisterConfirmationEmail(user.Email);
            if (emailResult.ErrorTriggered)
            {
                _userRepository.Rollback(transaction); // Rollback transaction
                return emailResult;
            }

            await _userRepository.CommitAsync(transaction);// Commit transaction
            return _controllerMessenger.ReturnSuccess(201, new SuccessMessage { Status = 201, Message = "Objeto criado com sucesso." });
        }
        catch (System.Exception ex)
        {
            return _controllerMessenger.ReturnInternalError500(ex.Message);
        }
    }

    private async Task<ControllerMessenger> SendRegisterConfirmationEmail(string userEmail)
    {
        //Criar o link após o front estar pronto
        var template = EmailConfiguration.ReturnRegisterConfirmationHtml();
        var emailRequest = EmailRequest.CreateDefaultObject(userEmail, "User Verification", template);
        var retornoEmail = await _emailService.SendEmailAsync(emailRequest);
        return retornoEmail;
    }

    // public async Task<ControllerMessenger> LoginUser(UserLoginDto user)
    // {
    //     throw new NotImplementedException();
    // }


}