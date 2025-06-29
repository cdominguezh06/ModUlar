using PacoYakuzaMAUI.utils;

namespace PacoYakuzaMAUI.services;

public class BackgroundState
{
    private readonly EventAggregator _eventAggregator;
    private string _mainBackgroundStyle = "";
    
    public BackgroundState(EventAggregator eventAggregator)
    {
        _eventAggregator = eventAggregator;
    }
    public string MainBackgroundStyle 
    { 
        get => _mainBackgroundStyle;
        set
        {
            if (_mainBackgroundStyle != value)
            {
                _mainBackgroundStyle = value;
                // Notificar el cambio usando el EventAggregator
                _ = _eventAggregator.NotifyBackgroundChanged();
            }
        }
    }
}