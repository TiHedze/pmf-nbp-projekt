namespace Pmf.PublicationTracker.Presentation.Api.Internal.Mappings
{
    using Pmf.PublicationTracker.Domain.Common;
    using Pmf.PublicationTracker.Domain.Common.ViewModels;

    internal static class ViewModelMapper
    {
        public static TViewModel Map<TDomainEntity, TViewModel>(TDomainEntity domainEntity)
            where TViewModel : ViewModelBase, IConstructibleFromDomainEntity<TDomainEntity, TViewModel>
            where TDomainEntity : EntityBase
            => TViewModel.FromEntity(domainEntity);

        public static List<TViewModel> Map<TDomainEntity, TViewModel>(List<TDomainEntity> domainEntities)
            where TViewModel : ViewModelBase, IConstructibleFromDomainEntity<TDomainEntity, TViewModel>
            where TDomainEntity : EntityBase
            => domainEntities
                .Select(Map<TDomainEntity, TViewModel>)
                .ToList();
    }
}
