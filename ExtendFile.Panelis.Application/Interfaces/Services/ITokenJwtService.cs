using ExtendFile.Panelis.Application.Modules.User.Responses;
using ExtendFile.Panelis.Domain.Modules.User.Entities;

namespace ExtendFile.Panelis.Application.Interfaces.Services;

public interface ITokenJwtService
{
    TokenJwtInformationResponse CreateTokenUser(User user); 
}