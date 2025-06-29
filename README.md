# ModUlar

ModUlar es una herramienta diseñada para activar/desactivar características individuales de mods compatibles para juegos de la saga Yakuza/Like a Dragon.

## Descripción

ModUlar permite a los usuarios personalizar sus mods habilitando o deshabilitando componentes específicos sin necesidad de reinstalar o modificar manualmente los archivos. Esto facilita la personalización de la experiencia de juego según las preferencias del usuario.

## Funcionamiento

El programa utiliza un archivo de configuración `modular.yaml` ubicado en la carpeta de cada mod para obtener información sobre el mod y sus componentes configurables. Basándose en esta información, ModUlar permite a los usuarios activar o desactivar características específicas del mod.

## Estructura del archivo modular.yaml

Cada mod compatible con ModUlar debe contener un archivo `modular.yaml` en su directorio principal. Este archivo define las propiedades del mod y sus configuraciones personalizables.

### Atributos principales

| Atributo | Descripción |
|----------|-------------|
| `Name` | Nombre del mod |
| `Author` | Autor/creador del mod |
| `Version` | Versión actual del mod |
| `Background` | URL a una imagen de fondo (desde internet) |
| `Description` | Descripción detallada del mod |
| `Settings` | Lista de ajustes configurables del mod |

### Estructura de los ajustes (Settings)

Cada ajuste dentro de la sección `Settings` puede contener:

| Atributo | Descripción |
|----------|-------------|
| `Name` | Nombre del ajuste |
| `Description` | Descripción del ajuste |
| `Files` | Lista de archivos que forman parte del ajuste |

### Estructura de los archivos (Files)

Cada archivo dentro de la sección `Files` puede contener:

| Atributo | Descripción |
|----------|-------------|
| `Name` | Nombre del archivo |
| `Folder` | Subcarpeta de la carpeta del mod en la que se encuentra el archivo |

## Ejemplo de modular.yaml

```yaml
Name: Enhanced Mod
Author: ModderX
Version: 1.2.3
Background: https://example.com/mod-background.jpg
Description: >
  Este mod modifica el tema de pelea de LAD : Pirate Yakuza In Hawaii y añade un reskin de una camiseta
Settings:
  - Name: Nueva musica
    Description: Modifica la música del juego
    Files:
      - Name: "bbg_d.acb"
        Folder: "sound"
      - Name: "bbg_d.awb"
        Folder: "stream"
  - Name: Nueva camiseta
    Description: Reskin de la camiseta del protagonista
    Files:
      - Name: "c_ca_t_basketb_mus_rogo_di.dds"
        Folder: "chara/dds_hires/00"