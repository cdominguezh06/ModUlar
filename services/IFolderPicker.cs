namespace ModUlar.services;

public interface IFolderPicker
{
    Task<string?> PickFolderAsync();
}