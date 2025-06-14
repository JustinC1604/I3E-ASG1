# I3E-ASG1
Git repository for I3E assignment 1

## Author
Chia Jia Cong Justin  
Date: 14 June 2025

---

## Overview
This is a Unity 3D game featuring a player navigating through hazards, collecting coins and treasure chests, and opening and unlocking doors. The player has health and can take damage and die from environmental hazards.

---

## How to Run the Game

### Platforms/Hardware:
- Compatible with Windows PC 
- Game tested with keyboard and mouse controls.

### Installation:
- Clone or download the project repository.
- Open in Unity Editor.
- Build and run for target platform or run from the Editor play mode.

---

## Controls

| Action               | Key          |
|----------------------|--------------|
| Move forward/back    | W / S        |
| Move left/right      | A / D        |
| Interact (Collect coins, key, open locked door and collect treasure chests) | E            |
| Jump                 | Spacebar     |
| Sprint                 | W/A/S/D + hold shift     |

---

## Game Mechanics & Features

- **Health System:** Player starts with 100 health points. Health decreases upon contact with hazards (e.g., spikes, lava, poison).
- **Damage Sources:**  
  - Lava pool and spikes cause instant death.  
  - Lava bricks deals 15 damage per second.
  - Poison area deals 10 damage per second.  
- **Normal Doors:** Doors that are not locked require the player to step on a pressure plate in front of the door. Stepping on the pressure plate will trigger the door to open.
- **Locked Door:** Requires a key to unlock. Players need to find the key to unlock the door.
- **Coins:** Collect coins scattered throughout the level to increase player score. Each coin increases the player score by 10.
- **Key**: When the player fulfils the requirement, the key will automatically spawn and the player can collect it.
- **Treasure Chest:** Collect a special treasure chest to gain bonus points and trigger game completion message.
- **UI Prompts:** Messages appear when interacting with objects (E.g "This door is locked..." message appears when trying to open the locked door without the key).

---

## Game Hacks / Cheats (If Any)

- None implemented.

---

## Known Limitations / Bugs

- Player health does not regenerate.
- No pause or save functionality currently implemented.

---

## Assets and Credits

**Audio/Sound effects**
- [Coin collection sound](https://freesound.org/people/AceOfSpadesProduc100/sounds/341695/).
- [Take damage sound](https://freesound.org/people/dersuperanton/sounds/437651/).
- [Open locked door sound](https://freesound.org/people/Leady/sounds/12739/).
- [Open door sound](https://freesound.org/people/pfranzen/sounds/262705/).
- [Victory sound](https://freesound.org/people/mokasza/sounds/810330/).
- [Background music](https://freesound.org/people/BloodPixelHero/sounds/696408/)
- The rest of the audio were taken from [Mixkit.co](https://mixkit.co/).

**Models and textures**
- The pillars, torches, goblet and treasure chest models were done by me from my previous assignments.
- Pillar texture was taken from [Polyhaven](https://polyhaven.com/a/plaster_brick_pattern).
- Torch textures were taken from [3D Substance Share](https://substance3d.adobe.com/community-assets).
- Treasure chest and goblet textures were done in Adobe Substance Painter using the built-in textures.
- The rest of the models were built using Unity ProBuilder.

**Code References**
- The open door animation and code in the DoorController script was taken from a YouTube channel called [SpeedTutor](https://youtu.be/tJiO4cvsHAo?si=IV-73wCqdp1Xl2rY).
- The coin collection code in the CoinCollection script was taken from a YouTube channel called [Rigor Mortis Tortoise](https://youtu.be/6iSJ_jh6Rdo?si=pAz4uRdLn4yBcGdA) with a little modification done by myself.
---

## Puzzle Solutions / Answers

- **Locked Door:** The key to unlock the door only spawns when the player score reaches 50. Therefore the player must collect all 5 coins in order for the key to spawn.
- **Glass Bridge:** Step only on safe tiles to avoid falling into lava.
    - ***First row:*** 1st, 3rd and 5th tile are safe.
    - ***Second row:*** Only 3rd tile is safe.
    - ***Third row:*** Only 2nd and 3rd tile are safe.
    - ***Fourth row:*** Only 2nd and 4th tile are safe.
    - ***Fifth row:*** Only second tile is safe.
    - ***Ideal route:*** 3, 3, 3, 2, 2
- **Treasure Chest:** Collect all coins first to spawn the key, then unlock the door to access the chest.

---
