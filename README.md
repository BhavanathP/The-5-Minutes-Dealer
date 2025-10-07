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

3. Pedestrians 
- Walk along street → purely cosmetic, adds life. 

4. Cops 
- Patrol on fixed or semi-random paths. 
- If near a buyer while player selling → enter “suspicious state”. 
- Show warning icon. 
- After short delay, turns to face → if player still holding → busted. 
- If no player activity → resumes patrol. 

5. Progress Bar (Selling Mechanic) 
- UI bar above player during selling. 
- Fills while holding E. 
- Drains if released early. 
- Success = full bar. 
- Fail = bar reaches 0. 

6. Score & Currency 
- Score = successful deals count (leaderboard metric). 
- Coins = currency to unlock new products in shop. 

7. Shop System 
- Between rounds: Buy stronger “products” (slower to sell, more profit). 
- First iteration: just seeds/weed → cocaine → high-class drugs (or safe placeholders like "basic goods → rare goods"). 

8. UI 
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
