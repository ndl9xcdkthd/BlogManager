using BlogManager.Application.DTOs;
using BlogManager.Application.Interfaces.Shared;
using BlogManager.Web.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogManager.Web.Areas.Identity.Pages.Account
{
    public class AuditLogModel : PageModel
    {
        private readonly IMediator _mediator;
        private readonly IAuthenticatedUserService _userService;
        public List<AuditLogResponse> AuditLogResponses;
        private IViewRenderService _viewRenderer;

        public AuditLogModel(IMediator mediator, IAuthenticatedUserService userService, IViewRenderService viewRenderer)
        {
            _mediator = mediator;
            _userService = userService;
            _viewRenderer = viewRenderer;
        }

        //public async Task OnGet()
        //{
        //    var response = await _mediator.Send(new GetAuditLogsQuery() { userId = _userService.UserId });
        //    AuditLogResponses = response.Data;
        //}
    }
}