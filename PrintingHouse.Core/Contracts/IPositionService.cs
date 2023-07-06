﻿namespace PrintingHouse.Core.Contracts
{
    using AdminModels.Position;

    public interface IPositionService
    {
        Task<IEnumerable<AllPositionViewModel>> GetAllAsync();

        Task AddNewAsync(AddPositionViewModel viewModel);
    }
}
