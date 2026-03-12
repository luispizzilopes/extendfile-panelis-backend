using ErrorOr;
using ExtendFile.Panelis.Application.Modules.Test.Requests.GetCountDaysWithoutTest;
using ExtendFile.Panelis.Application.Modules.Test.Responses.GetCountDaysWithoutTest;
using MediatR;

namespace ExtendFile.Panelis.Application.Modules.Test.Queries.GetCountDaysWithoutTest;

public record GetCountDaysWithoutTestQuery(GetCountDaysWithoutTestRequest Request) 
    : IRequest<ErrorOr<GetCountDaysWithoutTestResponse>>;