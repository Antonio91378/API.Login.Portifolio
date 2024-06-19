using API.Login.Domain.Dtos.Response;

namespace API.Login.Domain.Interfaces.Service;

public interface IControllerMessenger
{
    bool ErrorTriggered { get; }
    int StatusCode { get; }
    object? ResponseObject { get; }
    ControllerMessenger ReturnSuccess(int statusCode, string message);
    ControllerMessenger ReturnSuccess(int statusCode, object returnObject);
    ControllerMessenger ReturnBadRequest400(string? message);
    ControllerMessenger ReturnNotFound404(string element);
    ControllerMessenger ReturnNotFound404();
    ControllerMessenger ReturnInternalError500(string ex);
    ControllerMessenger ReturnServiceUnavailable503();
}