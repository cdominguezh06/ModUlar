using Windows.Storage.Pickers;

namespace ModUlar.services;

public class GameFolderPicker
{
    public async Task<string?> PickFolderAsync()
    {
        var hwnd = ((MauiWinUIWindow)Application.Current.Windows[0].Handler.PlatformView).WindowHandle;

        var picker = new FolderPicker();
        picker.FileTypeFilter.Add("*");
        WinRT.Interop.InitializeWithWindow.Initialize(picker, hwnd);

        var folder = await picker.PickSingleFolderAsync();
        return folder?.Path;
    }
}