# Aerial Combat Game - Prototype Unity

## Description
Jeu de combat aérien réaliste avec plusieurs avions de chasse modernes (Rafale, F-35, F-22, F-15, F-16), mode multijoueur et système de missiles avancé.

## Caractéristiques
- ✈️ Cinq avions réalistes (Rafale, F-35, F-22, F-15, F-16)
- 🎮 Mode solo et multijoueur (2 joueurs)
- 🚀 Système de missiles réaliste
- 🏃 Décollage depuis un porte-avion
- 💥 Combats aériens dynamiques
- 🌍 Support Android/iOS
- 🎯 Interface type "Tonnerre de Guerre"

## Installation

### Prérequis
- Unity 2022.3 LTS ou supérieur
- C# 9.0+

### Setup
1. Clonez le repository
2. Ouvrez avec Unity Hub
3. Importez les packages Unity Netcode for GameObjects (pour le multijoueur)
4. Lancez la scène MainMenuScene

## Structure du Projet
```
Assets/
├── Scripts/
│   ├── Aircraft/
│   ├── Combat/
│   ├── UI/
│   ├── Network/
│   └── Manager/
├── Scenes/
├── Prefabs/
└── Settings/
```

## Contrôles

### Clavier
- **ZQSD** : Mouvements (Altitude/Lacet)
- **Espace** : Accélération
- **Shift** : Freinage
- **R** : Décollage/Atterrissage
- **F** : Tir missiles
- **G** : Tir canons
- **Vue Souris** : Caméra libre

### Manette (Android/iOS)
- **Joystick gauche** : Contrôle avion
- **Joystick droit** : Caméra
- **Gachette** : Tir

## Développement

Ce prototype inclut:
- Système physique avancé des avions
- IA des ennemis
- Synchronisation réseau
- Interface utilisateur complète
- Système de missiles guidés

## Statut
🚧 En développement - Prototype fonctionnel

## Auteur
Didthomas03-maker
