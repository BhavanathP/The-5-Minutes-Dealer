# 5 Minute Dealer  

## 🎮 Overview  
A 2D arcade stealth-timing game made in Unity.  
Sell to buyers, avoid cops, and earn as much money as possible before time runs out.  
Game Over if you get caught dealing!

## 🎮 Gameplay
- Player moves left/right along the street.
- Buyers spawn randomly → hold `E` to sell.
- Progress bar fills → complete the deal.
- Release early → bar drains, risk losing the buyer.
- Cops patrol → if they catch you selling → Game Over.
- 5-minute timer per run.
- Earn score & coins to unlock new items.

## 🛠 Tech Stack  
- **Engine:** Unity 6 (2D URP)  
- **Language:** C#  
- **Version Control:** Git (Bitbucket + Sourcetree)  
- **Project Management:** Trello 
- **Communication:** Slack (Plug&Play Studio)  

## 🚀 Development Timeline  
**Week 1 – Core Systems**   

**Week 2 – Expansion & Polish**  

---
# 👩‍💻 Contributing Guidelines

## 🔀 Branching Model
- `main` → stable release builds only  
- `develop` → active sprint build  
- `feature/*` → feature branches (e.g., `feature/player-movement`)

## 💬 Commit Messages
Follow format:  
`[Feature] Short description`  
Examples:  
- `[Player] Added movement controller`  
- `[Farming] Implemented tile state transitions`

## ✅ Pull Requests
1. Always branch from `develop`.  
2. Commit small, logical changes.  
3. Open a PR into `develop`.  
4. Teammate reviews & approves before merging.  

## 📦 Sprint Workflow
- Tasks tracked in Trello.  
- Pick tasks from **This Sprint** column.  
- Move card → `In Progress` → `Done` when complete.
---

# GDD - 5 Minutes Dealer
- Game Title (Working): 5 Minute Dealer 
- Genre: 2D Arcade Stealth-Timing 
- Platform: PC (Unity) 
- Session Length: 5 minutes 

## Core Concept 
You’re a street hustler trying to make as much money as possible in 5 minutes. 
- Buyers spawn on the street → you must sell to them. 
- Police patrol the area → if they spot you mid-deal, it’s Game Over. 
- Selling requires holding a key to fill a progress bar. 
- Watch for cop warning signs and release in time to avoid suspicion. 

## Core Gameplay Loop 
-  Walk along the street (x-axis only).
-  Find a buyer → hold E to start selling.
-  Fill progress bar to complete the deal. 
-  Cops patrol → if they near a buyer, watch for warning signs. 
-  Release E if a cop’s about to turn. 
-  Earn money for completed sales. 
-  5 minutes pass → final score and money shown. 

## Systems Breakdown 
*1. Player* 
- Movement: Left/right (A/D or arrow keys). 
- Interaction: Hold E to sell → triggers progress bar. 

*2. Buyers* 
- Spawn randomly on street. 
- Stay idle for X seconds before leaving. 
- If bar reaches 0 → buyer leaves unsatisfied. 
- On successful sale → despawns, adds points/coins. 

*3. Pedestrians* 
- Walk along street → purely cosmetic, adds life. 

*4. Cops* 
- Patrol on fixed or semi-random paths. 
- If near a buyer while player selling → enter “suspicious state”. 
- Show warning icon. 
- After short delay, turns to face → if player still holding → busted. 
- If no player activity → resumes patrol. 

*5. Progress Bar (Selling Mechanic)* 
- UI bar above player during selling. 
- Fills while holding E. 
- Drains if released early. 
- Success = full bar. 
- Fail = bar reaches 0. 

*6. Score & Currency* 
- Score = successful deals count (leaderboard metric). 
- Coins = currency to unlock new products in shop. 

*7. Shop System* 
- Between rounds: Buy stronger “products” (slower to sell, more profit). 
- First iteration: just seeds/weed → cocaine → high-class drugs (or safe placeholders like "basic goods → rare goods"). 

*8. UI* 
- In-game: Timer, Score, Coins (top bar). 
- Game Over: Final score, coins collected, Restart. 
- Shop: Simple grid of items to unlock with coins. 

## Development Timeline (2 Weeks) 

Week 1 – Core Systems 
- Player movement + interaction (progress bar mechanic). 
- Buyer spawn/despawn system. 
- Cop patrol AI + warning sign. 
- Progress bar fill/drain logic. 

Week 2 – Expansion & Polish 
- Score & currency manager. 
- Shop system (basic seed unlocks). 
- Pedestrian filler NPCs. 
- Audio + placeholder art/VFX. 
- Playtest, bug fixes, polish, final build. 

## Art & Audio 
- Pixel art style, side street background. 
- Characters : Player, Buyers, Cops, Pedestrians. 
- Audio: 
  - Footsteps (player & cops). 
  - Bar filling sound. 
  - Cop warning cue. 
  - Success & busted sounds. 

## Win/Lose Conditions 
- Win = Survive until timer ends → high score. 
- Lose = Caught by police before time ends.
---

# Technical Design Document (TDD) #
- Game: 5 Minute Dealer 
- Engine: Unity (C#) 
## Core Systems & Scripts ##
*1. Player System*
- PlayerController.cs 
 - Handles horizontal input (A/D or arrows). 
 - Movement speed adjustable. 
- PlayerInteraction.cs 
 - Detects nearby buyers (trigger colliders). 
 - Starts selling when E pressed. 
 - Communicates with ProgressBarController. 

*2. Progress Bar System* 
- ProgressBarController.cs 
 - UI element spawned above player on interaction. 
 - Fills while E held. 
 - Drains when released. 
 - Events: OnSaleSuccess, OnSaleFail, OnSaleInterrupted. 

*3. Buyer System* 
- BuyerSpawner.cs 
 - Spawns buyers at random street positions. 
 - Configurable spawn rate & wait duration. 
- BuyerController.cs 
 - Idle until interacted. 
 - Waits for bar completion → success/fail outcome. 
 - Leaves if ignored or failed. 
- Events: OnBuyerLeft, OnBuyerServed. 

*4. Cop System* 
- CopSpawner.cs 
 - Spawns cops at intervals or predefined patrol points. 
- CopController.cs 
 - Moves left/right along street. 
 - Detects buyers in range. 
 - Enters “suspicious state” if player selling near buyer. 
 - Displays warning icon before turning. 
 - On turn → if player selling → Game Over event. 

*5. Pedestrian System* 
- PedestrianSpawner.cs 
 - Spawns neutral NPCs for background movement. 
- PedestrianController.cs 
 - Walks across street, despawns off-screen. 
 - No gameplay impact. 

*6. Score & Economy* 
- ScoreManager.cs 
 - Tracks deals completed (score). 
 - Raises OnScoreChanged. 
- CurrencyManager.cs 
 - Tracks coins earned. 
 - Used by ShopManager. 

*7. Shop System* 
- ShopItemData (SO) 
 - ItemName, Price, SellTimeMultiplier, ProfitMultiplier. 
- ShopManager.cs 
 - Displays unlockable items in UI. 
 - Deducts currency on purchase. 
 - Updates PlayerInteraction selling parameters. 

*8. Game Flow* 
- GameTimer.cs 
 - 5 min countdown. 
 - Raises OnTick, OnTimeUp. 
- GameManager.cs 
 - Central state machine (Idle → Playing → GameOver). 
 - Listens for GameOver from cops or timer end. 

*9. UI System* 
- UIManager.cs 
 - Updates score, coins, timer UI. 
- GameOverUI.cs 
 - Shows results (score + coins). 
 - Restart button → reloads scene. 
- WarningUI.cs 
 - Displays cop icon above their head. 

*10. Audio & VFX* 
- AudioManager.cs (already modular from Farmer). 
 - Plays footsteps, bar fill, warning, bust, success. 
- VFXManager.cs (pooled). 
 - Plays warning effect, success burst, busted effect. 

## Event Flow Example (Selling Encounter) ## 
1. Player collides with Buyer → presses E. 
2. PlayerInteraction tells ProgressBarController to start filling. 
3. BuyerController locks state to “in deal”. 
4. If Cop nearby & suspicious → CopController raises warning. 
 - WarningUI shows exclamation mark. 
 - After delay, cop turns. 
 - If still selling → GameManager → GameOver. 
5. If bar completes → ProgressBarController raises OnSaleSuccess. 
 - ScoreManager + CurrencyManager updated. 
 - Buyer leaves, cop resumes patrol. 

## Development Order (Suggested) ## 

*Week 1 – Core Loop* 
- PlayerController + PlayerInteraction. 
- ProgressBarController. 
- BuyerSpawner + BuyerController. 
- CopSpawner + CopController (basic patrol + suspicion). 
- GameTimer + GameManager.
  
*Week 2 – Expansion & Polish* 
- ScoreManager + CurrencyManager. 
- Shop system (basic product upgrades). 
- Pedestrians (flavor). 
- Audio + VFX. 
- UI polish (HUD + GameOver). 
- Playtest + bug fixes.
