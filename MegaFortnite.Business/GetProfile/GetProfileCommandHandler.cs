using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Mapster;
using MediatR;
using MegaFortnite.Common.Result;
using MegaFortnite.Contracts.Dto;
using MegaFortnite.DataAccess;
using MegaFortnite.Engine;
using Microsoft.Extensions.Logging;

namespace MegaFortnite.Business.GetProfile
{
    public class GetProfileCommandHandler : IRequestHandler<GetProfileCommand, Result<PlayerProfileDto>>
    {
        private IUnitOfWork _unitOfWork;
        private ILogger<GetProfileCommandHandler> _logger;

        public GetProfileCommandHandler(IUnitOfWork unitOfWork, ILogger<GetProfileCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Result<PlayerProfileDto>> Handle(GetProfileCommand request, CancellationToken cancellationToken)
        {
            var profile = await _unitOfWork.Profiles.GetAsync(q => q.CustomerId == request.CustomerId);

            if (profile == null)
                return Result<PlayerProfileDto>.NotFound($"Profile with Id = '{request.CustomerId}' not found");

            return Result<PlayerProfileDto>.Ok(profile.Adapt<PlayerProfileDto>());
        }
    }
}
