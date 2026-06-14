# Installation et Configuration

## Prérequis

- **Unity 2022.3 LTS** ou supérieur
- **C# 9.0+**
- **Visual Studio 2022** ou autre IDE C#
- **Git** (optionnel mais recommandé)

## Installation du Projet

### 1. Cloner le Repository
```bash
git clone https://github.com/didthomas03-maker/avion.git
cd avion
```

### 2. Ouvrir dans Unity
1. Ouvrez **Unity Hub**
2. Cliquez sur **Open Project**
3. Sélectionnez le dossier `avion`
4. Attendez que Unity importe les assets

### 3. Importer les Packages Requis

#### Netcode for GameObjects (Multijoueur)
```
Window > TextMesh Pro > Import TMP Essentials
Window > Netcode > Create Player Prefab
```

#### Input System (Contrôles)
```
Window > TextMesh Pro > Import TMP Essentials
```

### 4. Configuration de la Scène

1. Ouvrez la scène **MainMenuScene**
2. Vérifiez que tous les prefabs sont assignés
3. Testez le menu en appuyant sur **Play**

## Configuration Android/iOS

### Android

1. **Build Settings** > **Android**
2. **Player Settings**:
   - Orientation: **Landscape**
   - Minimum API: **21**
   - Target API: **32+**
3. **Build** > **Build APK**

### iOS

1. **Build Settings** > **iOS**
2. **Player Settings**:
   - Orientation: **Landscape**
   - iOS Version: **13.0+**
3. **Build** > **Build and Run**

## Contrôles Tactiles

### Android/iOS
- **Joystick Gauche** : Contrôle l'avion
- **Joystick Droit** : Caméra
- **Boutons D-Pad** : Actions spéciales
- **Gâchette Droite** : Tir
- **Gâchette Gauche** : Freinage

## Dépannage

### Erreur: "Netcode Manager not found"
**Solution**: Assurez-vous que Netcode for GameObjects est installé:
```
Window > Netcode > Create Player Prefab
```

### Erreur: "Missing Prefab"
**Solution**: Réassignez les prefabs dans l'Inspector de la scène.

### Performance faible
**Solution**: Réduisez la qualité graphique dans les settings.

## Documentation API

Voir `Assets/Documentation/API.md` pour les détails techniques.
