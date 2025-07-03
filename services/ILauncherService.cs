namespace ModUlar.services
{
    public interface ILauncherService
    {
        Task<bool> OpenAsync(Uri uri);
        Task<bool> OpenAsync(string uri);
    }
}