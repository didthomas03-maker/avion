# 🏳️ DRAPEAU BLANC - SYSTÈME DE REDDITION

## ✨ Fonctionnalités

### 🏳️ Mécanique d'Abandon
- Appuyez sur **ESC** ou le bouton **Abandon** pour quitter la bataille
- **Popup de confirmation** pour éviter les accidents
- Affiche l'écran **"🏳️ ABANDON - DÉFAITE"**
- Compte les abandons dans les statistiques

---

## 📱 Contrôles Tactiles Android/iOS

### **Joystick Gauche (Bas-Gauche)**
- **Horizontal**: Roulis gauche/droit
- **Vertical**: Cabré haut/bas

### **Joystick Droit (Bas-Droit)**
- **Horizontal**: Lacet
- **Vertical**: Accélération

### **Boutons d'Action (Haut-Droit)**
- 🔴 **ROUGE**: Tirer canon
- 🟡 **JAUNE**: Lancer missile
- 🟤 **MARRON**: Abandonner

---

## 🎮 Mise en Page Mobile

```
┌─────────────────────────────────┐
│  HUD (Vitesse, Alt, Carburant)  │
│                                 │
│                    🔴    🟡     │
│                CANON  MISSILE   │
│                                 │
│     🕹️               🕹️        │
│   MOUVEMENT          VISÉE      │
│                                 │
└─────────────────────────────────┘
```

---

## 🖥️ Contrôles Bureau

| Touche | Action |
|--------|--------|
| **W/S** | Accélération |
| **A/D** | Roulis |
| **Souris** | Visée |
| **SPACE** | Missile |
| **Clic gauche** | Canon |
| **ESC** | Abandonner |

---

## 🔌 Mode Hors Ligne

### Fonctionnalités:
- ✅ Jouer sans connexion Internet
- ✅ 100% ennemis IA locaux
- ✅ Batailles en solo
- ✅ Mode Campagne (scénarios)

### Modes de Jeu:
1. **Hors Ligne** - Batailles rapides vs IA
2. **Campagne** - 10 missions d'histoire
3. **En Ligne** - Batailles multijoueurs (connexion requise)

---

## 🚀 Implémentation

### Nouveaux Scripts:
1. `SurrenderSystem.cs` - Abandon avec drapeau blanc
2. `MobileJoystick.cs` - Contrôleur joystick tactile
3. `MobileControlsUI.cs` - Gestionnaire UI mobile
4. `OfflineModeManager.cs` - Gestionnaire mode hors ligne/campagne
5. `GameModeSelectionUI.cs` - Menu sélection mode
6. `AndroidJoystickLayout.cs` - Constructeur mise en page Android
7. `AndroidAircraftInput.cs` - Gestionnaire entrée Android
8. `CampaignManager.cs` - Progression campagne

---

## 📋 Mode Campagne

**10 Missions:**
1. Entraînement de base
2. Supériorité aérienne
3. Appui au sol
4. Bataille porte-avions
5. Mission d'escorte
6. Combat multi-ennemis
7. Tactiques avancées
8. Survie
9. Défi élite
10. Bataille finale

---

## 💡 Utilisation

### Activer Mode Hors Ligne:
```csharp
PlayerPrefs.SetString("GameMode", "Offline");
```

### Abandonner:
```csharp
SurrenderSystem.OnConfirmSurrender();
```

### Progression Campagne:
```csharp
int missions = CampaignManager.instance.GetCompletedMissions();
```

---

## 📊 Statistiques

- Nombre de missions complétées
- Nombre d'abandons
- Victoires/Défaites
- Meilleur score
- Temps de jeu total

---

## 🎯 À Venir

- [ ] Retours haptiques mobiles
- [ ] Briefings de mission
- [ ] Récompenses campagne
- [ ] Classements multijoueurs
- [ ] Pub vidéo optionnelle
- [ ] Synchronisation sauvegarde cloud

