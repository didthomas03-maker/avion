# 🎮 Fighter Jets Combat Game - Setup Guide

## ⚡ Quick Start (5 minutes)

### 1. **Create New Unity Project**
- Unity 2022 LTS or newer
- 3D (Built-in Render Pipeline)
- Target platform: PC/Mac/Linux

### 2. **Clone/Download Repository**
```bash
git clone https://github.com/didthomas03-maker/avion.git
cd avion
```

### 3. **Import Project Files**
- Copy `Assets` folder into your Unity project
- Wait for all scripts to compile

### 4. **Setup Input System**
- Go to `Edit > Project Settings > Input Manager`
- Ensure these axes exist:
  - **Vertical** (W/S for throttle)
  - **Horizontal** (A/D for roll)
  - **Mouse X/Y** for camera

### 5. **Create Scenes**
The following scenes should be in `Assets/Scenes/`:
- ✅ `MainMenu.unity` (already included)
- ✅ `BattleScene.unity` (already included)
- Create `AircraftSelection.unity` (or copy from included)

### 6. **Setup Build Settings**
1. Go to `File > Build Settings`
2. Add scenes in this order:
   - 0: MainMenu
   - 1: AircraftSelection
   - 2: BattleScene

### 7. **Play!**
- Open `MainMenu.unity` scene
- Press Play ▶️

---

## 🎯 Controls

| Key | Action |
|-----|--------|
| **W/S** | Throttle |
| **A/D** | Roll |
| **Mouse** | Pitch & Yaw |
| **SPACE** | Fire Missile |
| **LMB** | Cannon Fire |
| **E** | Launch from Carrier |

---

## 📦 Project Structure

```
Assets/
├── Scripts/
│   ├── Aircraft/          ← Flight physics
│   ├── Combat/            ← Missile & combat
│   ├── Menu/              ← UI screens
│   ├── Battle/            ← Battle management
│   ├── AI/                ← Enemy AI
│   ├── Camera/            ← Camera follow
│   ├── HUD/               ← Head-up display
│   ├── Environment/       ← Skybox, weather
│   ├── Managers/          ← Game managers
│   ├── Network/           ← Multiplayer
│   └── Utils/             ← Extensions
├── Prefabs/
│   ├── Aircraft.prefab    ← Player aircraft
│   └── Missile.prefab     ← Missile
├── Scenes/
│   ├── MainMenu.unity
│   ├── AircraftSelection.unity
│   └── BattleScene.unity
├── Data/
│   └── AircraftList.json  ← Aircraft stats
└── InputSystem/
    └── Controls.inputactions
```

---

## 🚀 What's Included

✅ **AircraftController.cs** - Complete flight physics
✅ **5 Aircraft Models** - Rafale, F-35, F-22, F-15, F-16
✅ **Missile System** - Guided missiles with lock-on
✅ **Enemy AI** - 4-state AI (Patrol, Chase, Attack, Evade)
✅ **HUD System** - Speed, altitude, fuel, missiles display
✅ **Carrier Launch** - Catapult system from porte-avion
✅ **Combat System** - Lock-on targeting
✅ **Main Menu** - Aircraft selection UI
✅ **Multiplayer Ready** - Mirror networking framework

---

## ⚙️ Next Steps

### 1. **Import 3D Models** (Optional)
- Replace the placeholder cube models with real aircraft models
- Import FBX files and assign to prefabs

### 2. **Add Audio**
- Add jet engine sounds
- Add missile launch sounds
- Add background music

### 3. **Add Particle Effects**
- Missile trail effects
- Explosion effects
- Smoke/contrails from jets

### 4. **Customize Aircraft**
- Edit `Assets/Data/AircraftList.json` to tweak stats
- Modify prefab properties in Inspector

### 5. **Test Multiplayer**
- Use Mirror package: `com.mirror-networking.mirror`
- Setup server/client configuration

---

## 🐛 Troubleshooting

**Problem: Scripts not compiling**
- Solution: Make sure all scripts are in `Assets/Scripts/`
- Restart Unity if needed

**Problem: Aircraft not spawning**
- Solution: Check that Aircraft.prefab is in `Assets/Prefabs/`
- Verify prefab has required components

**Problem: No enemies in battle**
- Solution: Check BattleManager settings
- Verify enemy prefab is assigned

**Problem: Controls not working**
- Solution: Go to Edit > Project Settings > Input Manager
- Add Vertical and Horizontal axes if missing

---

## 📖 Documentation

See `Docs/DEVELOPMENT.md` for:
- Architecture details
- AI behavior breakdown
- Physics system explanation
- Network protocol

---

## 🎮 Game Flow

```
MainMenu
   ↓
AircraftSelection (choose Rafale, F-35, F-22, F-15, or F-16)
   ↓
BattleScene
   ├─ Spawn on carrier deck
   ├─ Press E to launch
   ├─ Combat phase (shoot down enemies)
   └─ Victory/Defeat screen
```

---

## 💾 Save/Load

Player progress is saved via PlayerPrefs:
- Selected aircraft
- High scores
- Settings

---

## 🔧 Performance Tips

1. Use Distance Culling for enemies
2. Limit missile count
3. Use lower LOD models at distance
4. Enable GPU Instancing on materials
5. Profile with Unity Profiler

---

## 📝 License

This project is open source. Feel free to modify and distribute!

---

**Happy Flying! ✈️**
