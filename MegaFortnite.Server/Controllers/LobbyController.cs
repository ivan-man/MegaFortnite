using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MegaFortnite.Server.Lobby;
using Microsoft.AspNetCore.SignalR;

namespace MegaFortnite.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LobbyController : ControllerBase
    {
        private IHubContext<LobbyHub> _hub;




    }
}
