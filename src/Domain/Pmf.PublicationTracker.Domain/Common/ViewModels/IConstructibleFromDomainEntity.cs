namespace Pmf.PublicationTracker.Domain.Common.ViewModels
{
    using Pmf.PublicationTracker.Domain.Common.Entities;

    public interface IConstructibleFromDomainEntity<TDomainEntity, TViewModel>
        where TDomainEntity : EntityBase
        where TViewModel : ViewModelBase
    {
        public static abstract TViewModel FromEntity(TDomainEntity entity);
    }
}
