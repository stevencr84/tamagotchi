using System.Threading.Tasks;
using Tamagotchi.Domain.Common;

namespace Tamagotchi.Domain.AggregatesModel.DragonAggregate
{
    public interface IDragonRepository : IRepository<Dragon>
    {
        Dragon Add(Dragon dragon);

        Dragon Update(Dragon dragon);

        Task<Dragon> GetAsync(int dragonId);
    }
}