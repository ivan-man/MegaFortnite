using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MegaFortnite.Common.Result;
using MegaFortnite.Contracts.Dto;

namespace MegaFortnite.Business.GetProfile
{
    public class GetProfileCommand : IRequest<Result<PlayerProfileDto>>
    {
        public int Id { get; set; }
    }
}
