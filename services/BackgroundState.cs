using ModUlar.utils;

namespace ModUlar.services;

public class BackgroundState
{
    private readonly EventAggregator _eventAggregator;
    private string _mainBackgroundStyle = "";
    private string _textColorStyle = "color: black;"; // Color de texto por defecto
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
                // Calcular y actualizar el color de texto basado en el nuevo fondo
                _textColorStyle = GetContrastingTextColor(value);
                _ = _eventAggregator.NotifyBackgroundChanged();
            }
        }
    }
    
    public string TextColorStyle
    {
        get => _textColorStyle;
    }
    
    // Estructura simple para representar un color RGB
    private struct RgbColor
    {
        public int R;
        public int G;
        public int B;
        
        public RgbColor(int r, int g, int b)
        {
            R = r;
            G = g;
            B = b;
        }
    }
    
    // Determina si un color es claro u oscuro para elegir texto contrastante
    private bool IsColorDark(RgbColor color)
    {
        // Fórmula para calcular el brillo percibido (YIQ)
        double brightness = (color.R * 0.299 + color.G * 0.587 + color.B * 0.114) / 255;
        return brightness < 0.5; // Si el brillo es menor a 0.5, consideramos que es un color oscuro
    }
    
    // Obtiene el color de texto más adecuado (blanco o negro) en función del fondo
    private string GetContrastingTextColor(string backgroundStyle)
    {
        try
        {
            // Para fondos con colores sólidos
            if (backgroundStyle.Contains("background-color"))
            {
                // Extraer el color en formato hexadecimal o rgb
                string colorValue = "";
                
                // Extraer valores hexadecimales (#rrggbb)
                if (backgroundStyle.Contains("#"))
                {
                    int startIndex = backgroundStyle.IndexOf('#');
                    int endIndex = backgroundStyle.IndexOf(';', startIndex);
                    if (endIndex == -1) endIndex = backgroundStyle.Length;
                    
                    colorValue = backgroundStyle.Substring(startIndex, endIndex - startIndex);
                    
                    // Convertir valor hexadecimal a RGB
                    RgbColor rgbColor = HexToRgb(colorValue);
                    return IsColorDark(rgbColor) ? "color: white;" : "color: black;";
                }
                // Extraer valores RGB
                else if (backgroundStyle.Contains("rgb"))
                {
                    int startIndex = backgroundStyle.IndexOf("rgb");
                    int endIndex = backgroundStyle.IndexOf(';', startIndex);
                    if (endIndex == -1) endIndex = backgroundStyle.Length;
                    
                    colorValue = backgroundStyle.Substring(startIndex, endIndex - startIndex);
                    
                    // Parsear valores RGB
                    RgbColor rgbColor = ParseRgb(colorValue);
                    return IsColorDark(rgbColor) ? "color: white;" : "color: black;";
                }
            }
            
            // Para fondos con imágenes o gradientes, usamos un enfoque conservador
            // Como no podemos analizar la imagen fácilmente, usamos color claro para asegurar legibilidad
            if (backgroundStyle.Contains("url") || backgroundStyle.Contains("linear-gradient"))
            {
                return "color: white; text-shadow: 1px 1px 2px rgba(0,0,0,0.8);"; // Texto blanco con sombra para legibilidad
            }
            
            // Por defecto, usamos texto negro
            return "color: black;";
        }
        catch
        {
            // En caso de error, volver a valores predeterminados
            return "color: black;";
        }
    }
    
    // Convierte un color hexadecimal a RGB
    private RgbColor HexToRgb(string hexColor)
    {
        hexColor = hexColor.TrimStart('#');
        
        if (hexColor.Length == 3) // formato #rgb
        {
            int r = Convert.ToInt32(hexColor[0].ToString() + hexColor[0].ToString(), 16);
            int g = Convert.ToInt32(hexColor[1].ToString() + hexColor[1].ToString(), 16);
            int b = Convert.ToInt32(hexColor[2].ToString() + hexColor[2].ToString(), 16);
            
            return new RgbColor(r, g, b);
        }
        else if (hexColor.Length == 6) // formato #rrggbb
        {
            int r = Convert.ToInt32(hexColor.Substring(0, 2), 16);
            int g = Convert.ToInt32(hexColor.Substring(2, 2), 16);
            int b = Convert.ToInt32(hexColor.Substring(4, 2), 16);
            
            return new RgbColor(r, g, b);
        }
        
        // Color por defecto (negro)
        return new RgbColor(0, 0, 0);
    }
    
    // Parsea un color en formato RGB
    private RgbColor ParseRgb(string rgbColor)
    {
        try
        {
            // Extraer los valores numéricos de rgb(r, g, b)
            string[] parts = rgbColor.Replace("rgb(", "").Replace(")", "").Split(',');
            
            if (parts.Length >= 3)
            {
                int r = int.Parse(parts[0].Trim());
                int g = int.Parse(parts[1].Trim());
                int b = int.Parse(parts[2].Trim());
                
                return new RgbColor(r, g, b);
            }
        }
        catch
        {
            // Ignorar errores de parsing
        }
        
        // Color por defecto (negro)
        return new RgbColor(0, 0, 0);
    }

    public Windows.UI.Color GetWindowColor()
    {
        var rgbColor = HexToRgb(_textColorStyle.Split(":")[1].TrimStart());
        return new Windows.UI.Color 
        { 
            A = 100,
            R = (byte)rgbColor.R,
            G = (byte)rgbColor.G,
            B = (byte)rgbColor.B
        };
    }
}