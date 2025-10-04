using System.Threading.Tasks;
using Core.Domain;
using Core.Infra;

namespace Core.Usecases {
    public class PurchaseItemUseCase {
        private readonly IPurchaseGateway _gateway;
        public PurchaseItemUseCase(IPurchaseGateway gateway) {
            _gateway = gateway;
        }

        public async Task<bool> ExecuteAsync(string playerId, string itemId) {
            // Business rules can go here: validation, limits, etc.
            var result = await _gateway.PurchaseAsync(playerId, itemId);
            return result;
        }
    }
}
