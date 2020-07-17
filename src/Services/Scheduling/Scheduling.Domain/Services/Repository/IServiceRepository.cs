namespace Scheduling.Domain.Services.Repository
{
    public interface IServiceRepository
    {
        void AddService(Service service);
        void UpdateService(Service service);
    }
}
