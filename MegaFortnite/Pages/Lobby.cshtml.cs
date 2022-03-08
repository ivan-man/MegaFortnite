using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MegaFortnite.Pages
{
    public class LobbyModel : PageModel
    {
        private readonly ILogger<LobbyModel> _logger;

        public LobbyModel(ILogger<LobbyModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}
