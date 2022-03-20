using System.Threading.Tasks;

namespace MegaFortnite.Common.Interfaces
{
    public interface INotifyService
    {
        Task SendNotificationAsync(string lobbyKey, string message);
    }
}
