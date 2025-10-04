using System.Threading.Tasks;

namespace Core.Infra {
    public interface IPurchaseGateway {
        Task<bool> PurchaseAsync(string playerId, string itemId);
    }
}
