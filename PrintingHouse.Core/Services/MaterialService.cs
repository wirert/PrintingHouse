namespace PrintingHouse.Core.Services
{
    using PrintingHouse.Core.Services.Contracts;
    using PrintingHouse.Infrastructure.Data.Common.Contracts;
    using PrintingHouse.Infrastructure.Data.Entities;
    using System.Threading.Tasks;

    public class MaterialService : IMaterialService
    {
        private readonly IRepository repo;

        public MaterialService(IRepository _repo)
        {
            repo = _repo;
        }

        public async Task<string?> GetNameByIdIfExistAsync(int materialId)
        {
            var material = await repo.GetByIdAsync<Material>(materialId);

            return material.Type;
        }
    }
}
