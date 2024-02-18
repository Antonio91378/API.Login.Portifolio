namespace API.Login.Service.Users;

using System;
using System.Threading.Tasks;
using API.Login.Domain.Dtos.Request;
using API.Login.Domain.Dtos.Response;
using API.Login.Domain.Entities.User;
using API.Login.Infra.Users;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly ControllerMessenger _controllerMessenger;

    public UserService(IUserRepository userRepository, ControllerMessenger controllerMessenger)
    {
        _userRepository = userRepository;
        _controllerMessenger = controllerMessenger;

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

    public async Task<ControllerMessenger> GetAsync(int? id)
    {
        try
        {
            var usersFound = new List<User>();
            if (id is null)
            {
                usersFound = await _userRepository.GetAsync(null);
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

            userInformed.PassWord = (userSerialized.NewPassWord is not null)
                ? userSerialized.NewPassWord
                : userInformed.PassWord;

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

    public async Task<ControllerMessenger> RegisterUser(UserRegisterDto user)
    {
        try
        {
            var userWithSameEmail = await _userRepository.GetAsync(u => u.Email == user.Email);

            if (userWithSameEmail is not null && userWithSameEmail.Count() > 0)
            {
                return _controllerMessenger.ReturnBadRequest400("The informed email is already taken.");
            }

            //I need add new filds in the user entity

        }
        catch (System.Exception ex)
        {
            return _controllerMessenger.ReturnInternalError500(ex.Message);
        }
    }

    public Task<UserDto> LoginUser(UserLoginDto user)
    {
        throw new NotImplementedException();
    }

    Task<ControllerMessenger> IUserService.LoginUser(UserLoginDto user)
    {
        throw new NotImplementedException();
    }
}