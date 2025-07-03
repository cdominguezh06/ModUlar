# 🇺🇸 ModUlar

ModUlar is a tool designed to activate/deactivate individual features of compatible mods for games in the Yakuza/Like a Dragon saga.

## Description

ModUlar allows users to customize their mods by enabling or disabling specific components without the need to reinstall or manually modify files. This facilitates the customization of the gaming experience according to the user's preferences.

## How It Works

The program uses a `modular.yaml` configuration file located in each mod's folder to obtain information about the mod and its configurable components. Based on this information, ModUlar allows users to activate or deactivate specific features of the mod.

## Structure of the modular.yaml file

Each mod compatible with ModUlar must contain a `modular.yaml` file in its main directory. This file defines the mod's properties and its customizable settings.

### Main Attributes

| Attribute    | Description                                                 |
|--------------|-------------------------------------------------------------|
| `Name`       | Name of the mod                                             |
| `Author`     | Author/creator of the mod                                   |
| `Version`    | Current version of the mod                                  |
| `Background` | URL to a background image (from the internet)               |
| `Description`| Detailed description of the mod                             |
| `Settings`   | List of configurable settings for the mod                   |

### Structure of Settings

Each setting within the `Settings` section can contain:

| Attribute    | Description                         |
|--------------|-------------------------------------|
| `Name`       | Name of the setting                 |
| `Description`| Description of the setting          |
| `Files`      | List of files that are part of it   |

### Structure of Files

Each file within the `Files` section can contain:

| Attribute | Description                                                       |
|-----------|-------------------------------------------------------------------|
| `Name`    | Name of the file                                                  |
| `Folder`  | Subfolder within the mod folder where the file is located         |

## Example of modular.yaml

```yaml
Name: Enhanced Mod
Author: ModderX
Version: 1.2.3
Background: https://example.com/mod-background.jpg
Description: >
  This mod modifies the battle theme of LAD: Pirate Yakuza In Hawaii and adds a t-shirt reskin
Settings:
  - Name: New music
    Description: Modifies the game's music
    Files:
      - Name: "bbg_d.acb"
        Folder: "sound"
      - Name: "bbg_d.awb"
        Folder: "stream"
  - Name: New t-shirt
    Description: Reskin of the protagonist's t-shirt
    Files:
      - Name: "c_ca_t_basketb_mus_rogo_di.dds"
        Folder: "chara/dds_hires/00"
```
﻿

# 🇪🇸 ModUlar

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
