namespace ProjectManagement.Services
{
    public interface IUnitOfWork : IDisposable
    {
        int Complete();
    }
}
