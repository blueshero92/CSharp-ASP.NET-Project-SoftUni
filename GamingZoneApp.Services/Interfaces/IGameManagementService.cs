using GamingZoneApp.ViewModels.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamingZoneApp.Services.Core.Interfaces
{
    public interface IGameManagementService
    {
        Task<GameInputModel?> GetEditAsync(Guid gameId);

        Task<bool> PostEditAsync(Guid gameId, GameInputModel inputModel);
    }
}
