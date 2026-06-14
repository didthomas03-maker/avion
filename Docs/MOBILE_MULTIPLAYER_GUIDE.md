# 📱 MULTIPLAYER ANDROID/iOS - GUIDE COMPLET

## 🎮 MODE MULTIPLAYER SUR MOBILE

### ✅ Fonctionnalités
- ✅ **PvP 1v1** direct sur Android/iOS
- ✅ **WiFi Local** ou **Internet** (LAN/WAN)
- ✅ **Joysticks tactiles** synchronisés en réseau
- ✅ **Synchronisation en temps réel** des avions
- ✅ **HUD PvP** avec ping et force du signal
- ✅ **Mirror Networking Framework**

---

## 🔌 Architecture Réseau

```
┌─────────────────────┐        WiFi/Internet        ┌─────────────────────┐
│   Android 1 (HOST)  │◄──────────────────────────►│   Android 2 (CLIENT)│
│  Joysticks tactiles │                             │  Joysticks tactiles │
│  Avion Local        │◄──────────────────────────►│  Avion Distant      │
│  HUD complet        │    Synchronisation           │  HUD complet        │
└─────────────────────┘        Missiles              └─────────────────────┘
     🎮 Player 1                Santé                    🎮 Player 2
```

---

## 🛰️ Protocole de Synchronisation

### **Position et Rotation (20 updates/sec)**
- Envoyé via `CmdUpdateAircraftState`
- Interpolation smooth

### **Missiles Tirés (Événement)**
- `CmdFireMissile` → Serveur
- `RpcFireMissileEffect` → Tous les clients

### **Dégâts et Santé**
- `CmdTakeDamage` → Serveur
- Sync automatique via `[SyncVar]`

---

## 📱 Écran de Connexion Mobile

```
┌────────────────────────────────────────┐
│  MULTIPLAYER ANDROID/iOS               │
│                                        │
│  🟢 État: DÉCONNECTÉ                  │
│                                        │
│  Adresse IP:  [127.0.0.1]             │
│  Port:        [7777]                  │
│                                        │
│  ┌────────────────────────────────┐  │
│  │   🔴 CRÉER UNE PARTIE (HOST)   │  │
│  └────────────────────────────────┘  │
│                                        │
│  ┌────────────────────────────────┐  │
│  │   🔵 REJOINDRE PARTIE (CLIENT) │  │
│  └────────────────────────────────┘  │
│                                        │
│  Ping: 45ms  Signal: ███████░░░      │
│                                        │
└────────────────────────────────────────┘
```

---

## 🎯 HUD PENDANT LA BATAILLE PvP

```
┌────────────────────────────────────────┐
│  YOU                      PING:45ms    │
│  HP: ███████░ 70%     Signal: 🟢🟢🟢  │
│  SPD: 1200 km/h                       │
│  ALT: 5000 m                          │
│  MISSILES: 5/8                        │
│                                        │
│        [Champ de bataille]            │
│                                        │
│  ENEMY HP: ███████░░░ 60%             │
│  ENEMY MISSILES: 3/8                  │
│                                        │
│                                        │
│  🟢 CONNECTÉ                          │
└────────────────────────────────────────┘
```

---

## 🕹️ Contrôles Tactiles Synchronisés

### **Joystick Gauche (Synchronisé)**
```
[Joystick Position] → CmdSendNetworkInput → Serveur → RpcUpdateRemoteAircraft
```

### **Joystick Droit (Synchronisé)**
```
[Joystick Position] → CmdSendNetworkInput → Serveur → RpcUpdateRemoteAircraft
```

### **Boutons Missiles**
```
[Click Missile] → CmdFireMissile → Serveur → RpcFireMissileEffect (Tous)
```

---

## 📡 Flux de Données Réseau

```
Joueur 1 (Android HOST):
  Input Joystick → CmdSendNetworkInput → Serveur Mirror
  ↓
  Serveur reçoit et valide
  ↓
  RpcUpdateRemoteAircraft → Joueur 2
  ↓
Joueur 2 (Android CLIENT):
  Reçoit position/rotation de Joueur 1
  ↓
  Affiche avion ennemi interpolé
  ↓
  Input Joystick → CmdSendNetworkInput → Serveur
```

---

## 🔧 Scripts Réseau Ajoutés

### 1. **MobileMultiplayerManager.cs**
- Gestion serveur/client
- Détection des joueurs connectés
- Lancement de partie

### 2. **MobileNetworkAircraft.cs**
- Synchronisation avion en réseau
- [SyncVar] pour santé, missiles
- Commandes réseau (Cmd/Rpc)

### 3. **MobileNetworkInput.cs**
- Capture joysticks tactiles
- Envoi inputs au serveur
- Synchronisation 20 updates/sec

### 4. **MobileNetworkSpawner.cs**
- Spawn avions pour les joueurs
- Points de spawn définis
- Attribution connexions

### 5. **MobilePvPHUD.cs**
- Affichage santé joueur/ennemi
- Affichage missiles
- Statut réseau et ping

### 6. **MobileNetworkConnectionUI.cs**
- UI connexion
- Saisie IP/Port
- Indicateur connexion

---

## 📱 Compilation Android

### Étape 1: Configuration du Projet
```
File → Build Settings
Platform: Android
Player Settings:
  - Graphics: OpenGL ES 3
  - Min API Level: 24 (Android 7.0+)
  - Internet Permission: ✓
```

### Étape 2: Setup Réseau Mirror
```
Assets → Import Mirror Package
Window → Mirror → Asphalt Networking
```

### Étape 3: Build APK
```
File → Build and Run
Choisir chemin APK
Signer l'APK (KeyStore)
```

---

## 🍎 Compilation iOS

### Étape 1: Setup Xcode
```
File → Build Settings
Platform: iOS
Player Settings:
  - SDK: Latest
  - Camera Permission: ✓
  - Microphone Permission: ✓
```

### Étape 2: Build et Deploy
```
File → Build
Ouvrir dans Xcode
Connecteur iPhone
Build and Run
```

---

## 🌐 Modes de Connexion

### **WiFi Local (LAN)**
```
Joueur 1: 192.168.1.100:7777 (HOST)
Joueur 2: Se connecte à 192.168.1.100:7777 (CLIENT)
```

### **Internet (WAN)**
```
Joueur 1: Adresse IP publique:7777 (HOST)
Joueur 2: Se connecte via IP publique

⚠️ Nécessite port forwarding sur le routeur
```

### **Relay Server**
```
Utiliser Mirror Relay ou Photon:
- HOST: Se connecte au relay
- CLIENT: Se connecte au relay
- Relay transmet messages
```

---

## 📊 Performance

| Métrique | Valeur |
|----------|--------|
| Input Rate | 20 updates/sec |
| Network Sync | Position/Rotation/Munitions |
| Latency | <100ms (recommandé) |
| Max Players | 2 (PvP) |
| Bandwidth | ~2 Mbps |
| Max Connections | 2 |

---

## 🐛 Troubleshooting

### **Problème: Cannot connect**
```
Solution:
1. Vérifier IP address correcte
2. Vérifier port 7777 ouvert
3. Même WiFi pour LAN
4. Firewall peut bloquer
```

### **Problème: High ping/Lag**
```
Solution:
1. WiFi 5GHz plus stable
2. Rapprocher des routeurs
3. Réduire distance joueurs
4. Fermer autres applis réseau
```

### **Problème: Missile ne synchronise pas**
```
Solution:
1. Vérifier CmdFireMissile exécuté
2. Vérifier RpcFireMissileEffect
3. Checker Debug logs
4. Relancer partie
```

---

## 🎮 Scénarios de Jeu

### **PvP 1v1 Local**
```
1. Joueur 1 lance partie (HOST)
2. Joysticks tactiles s'affichent
3. Joueur 2 se connecte (CLIENT)
4. Battle commence automatiquement
5. 1v1 aerial dogfight
```

### **Joueur vs Ami (WiFi)**
```
1. Joueur 1 créé partie sur son IP
2. Joueur 2 entre IP de Joueur 1
3. Connexion établie
4. Battle synchronisée
5. Joysticks tactiles synchronisés
```

---

## 📋 Checklist Multijoueur Mobile

- ✅ Mirror Networking importé
- ✅ MobileMultiplayerManager assigné
- ✅ Prefabs réseau configurés
- ✅ Joysticks tactiles en réseau
- ✅ HUD PvP actif
- ✅ Synchronisation missiles
- ✅ Synchronisation santé
- ✅ Android permissions
- ✅ iOS permissions
- ✅ Build APK testé
- ✅ Build iOS testé

---

## 🚀 Prochaines Améliorations

- [ ] Mode 2v2 ou 4 joueurs
- [ ] Lobby système
- [ ] Matchmaking
- [ ] Leaderboards cloud
- [ ] Replay système
- [ ] Voice chat
- [ ] Spectator mode
- [ ] Customization des avions en réseau

---

**C'est prêt! Téléchargez l'APK et jouez en multiplayer! 🎮🚀**
