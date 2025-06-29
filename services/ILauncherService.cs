namespace PacoYakuzaMAUI.Services
{
    public interface ILauncherService
    {
        Task<bool> OpenAsync(Uri uri);
        Task<bool> OpenAsync(string uri);
    }
}