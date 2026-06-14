# Development Guide

## Architecture

### Core Systems
1. **AircraftController** - Main flight physics and input
2. **AerodynamicsEngine** - Realistic air physics
3. **MissileSystem** - Guided weapon systems
4. **BattleManager** - Battle flow and scoring
5. **EnemyAI** - AI-controlled aircraft
6. **MultiplayerManager** - Network synchronization

### Scene Flow
MainMenu → AircraftSelection → BattleScene (or Multiplayer)

## Input System
- **W/S**: Throttle control
- **Mouse/Stick**: Aircraft rotation
- **Space**: Fire missile
- **LMB**: Cannon fire
- **E**: Launch from carrier

## Networking
Using Mirror for multiplayer support.

## Physics
- Rigidbody-based flight dynamics
- Atmospheric density calculations
- G-force calculations
- Drag and lift modeling

## TODOs
- [ ] Implement proper AI pathfinding
- [ ] Add 3D aircraft models
- [ ] Implement damage system
- [ ] Add sound effects and music
- [ ] Create particle effects for explosions
- [ ] Implement radar system
- [ ] Add campaign mode
- [ ] Optimize for performance
