# Architecture du Prototype

## Structure des Dossiers

```
Assets/
├── Scripts/
│   ├── Aircraft/
│   │   ├── AircraftBase.cs          # Classe de base pour tous les avions
│   │   └── FighterJet.cs            # Implémentation des avions de chasse
│   ├── Combat/
│   │   ├── Missile.cs               # Système de missiles
│   │   └── WeaponSystem.cs          # Système d'armes (à implémenter)
│   ├── UI/
│   │   ├── MainMenuUI.cs            # Menu principal
│   │   ├── HUDDisplay.cs            # Affichage tête haute
│   │   └── PauseMenu.cs             # Menu pause (à implémenter)
│   ├── Manager/
│   │   ├── GameManager.cs           # Gestionnaire de jeu
│   │   └── CarrierManager.cs        # Gestion du porte-avion
│   └── Network/
│       ├── MultiplayerManager.cs    # Gestion du multijoueur
│       └── PlayerSync.cs            # Synchronisation réseau (à implémenter)
├── Prefabs/
│   ├── Aircraft/
│   ├── UI/
│   └── Environment/
├── Scenes/
│   ├── MainMenu.scene
│   ├── CarrierScene.scene
│   └── MultiplayerLobby.scene
├── Materials/
├── Models/
└── Documentation/
```

## Systèmes Principaux

### 1. Système de Contrôle des Avions

**Classe**: `AircraftBase.cs`

```
Input (Clavier/Manette)
        ↓
   HandleInput()
        ↓
   UpdatePhysics()
        ↓
   Mouvement et Rotation
```

### 2. Système de Missiles

**Classe**: `Missile.cs`

```
Launch() → Trajectoire → OnTriggerEnter() → Explosion → Damage
```

### 3. Système de Multijoueur

**Classes**: `MultiplayerManager.cs`, `GameManager.cs`

```
NetworkManager
    ↓
    ├─ Host Server
    ├─ Client Connection
    └─ Scene Synchronization
```

### 4. Interface Utilisateur

**Classes**: `MainMenuUI.cs`, `HUDDisplay.cs`

```
Menu Principal
    ↓
    ├─ Solo
    ├─ Multijoueur
    └─ Settings
        ↓
    Jeu
        ↓
        HUD (Speed, Altitude, Health, Missiles)
```

## Diagrammes de Flux

### Flux de Décollage

```
[Porte-avion] 
    ↓
[Démarrage Moteurs] (R)
    ↓
[Accélération] (Espace)
    ↓
[Position de Décollage]
    ↓
[Vol - Altitude > 0]
```

### Flux de Combat

```
[Détection Ennemi]
    ↓
[Approche]
    ↓
    ├─ [Missile] (F) → Distance Moyenne
    ├─ [Canon] (G) → Combat Rapproché
    └─ [Évasion]
        ↓
[Victoire ou Défaite]
```

## Système de Réseau (Netcode)

### Synchronisation des Avions

1. **Position**: Synchronisée chaque frame
2. **Rotation**: Synchronisée chaque frame
3. **Vitesse**: Synchronisée chaque frame
4. **État**: Santé, Carburant, Missiles

### RPC (Remote Procedure Calls)

- `FireMissileServerRpc()` : Lancer un missile
- `TakeDamageServerRpc()` : Recevoir des dégâts
- `PlayerDeadServerRpc()` : Annoncer la mort

## Performance

### Optimisations Implémentées

1. **Object Pooling** : Réutiliser les missiles
2. **LOD (Level of Detail)** : Réduire les détails à distance
3. **Frustum Culling** : Ne dessiner que ce qui est visible
4. **Network Interpolation** : Lissage du mouvement des autres joueurs

### Cibles de Performance

- **PC**: 60 FPS minimum
- **Mobile**: 30-60 FPS
- **Latence Réseau**: < 100ms (cible)

## État Actuel

✅ Implémenté:
- Base système de vol
- Contrôles des avions
- Système de missiles basique
- Menu principal
- HUD

🚧 En cours:
- Synchronisation réseau complète
- IA des ennemis
- Système de combat avancé

❌ À faire:
- Graphiques améliorés
- Sons et musique
- Animations avancées
- Niveaux/Missions
- Boutique d'avions
