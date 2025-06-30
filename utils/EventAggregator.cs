namespace ModUlar.utils;

public class EventAggregator
{
    public event Func<Task> BackgroundChanged;
    
    public async Task NotifyBackgroundChanged()
    {
        var handler = BackgroundChanged;
        if (handler != null)
        {
            await handler.Invoke();
        }
    }
}