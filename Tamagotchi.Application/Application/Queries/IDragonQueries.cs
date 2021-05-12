using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tamagotchi.Application.Application.Queries
{
    public interface IDragonQueries
    {
        Task<DragonViewModel> GetDragonAsync(int id);

        Task<IEnumerable<DragonViewModel>> GetDragonsAsync();
    }
}
