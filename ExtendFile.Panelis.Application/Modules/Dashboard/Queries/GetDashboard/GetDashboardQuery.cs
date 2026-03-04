using ErrorOr;
using ExtendFile.Panelis.Application.Modules.Dashboard.Requests.GetDashboard;
using ExtendFile.Panelis.Application.Modules.Dashboard.Responses.GetDashboard;
using MediatR;

namespace ExtendFile.Panelis.Application.Modules.Dashboard.Queries.GetDashboard;

public record GetDashboardQuery(GetDashboardRequest Request) : IRequest<ErrorOr<DashboardResponse>>;
