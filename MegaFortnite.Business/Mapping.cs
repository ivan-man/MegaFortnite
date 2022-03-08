using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mapster;
using MegaFortnite.Domain.Models;
using MegaFortnite.Engine;

namespace MegaFortnite.Business
{
    public class Mapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Lobby, Session>()
                .Map(dst => dst.LobbyKey, src => src.Key);
        }
    }
}
