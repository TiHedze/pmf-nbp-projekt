namespace Pmf.PublicationTracker.Domain.Common.ViewModels
{
    public interface IConstructibleFromDomainEntity<TDomainEntity, TViewModel>
        where TDomainEntity : EntityBase
        where TViewModel : ViewModelBase
    {
        public static abstract TViewModel FromEntity(TDomainEntity entity);
    }
}
