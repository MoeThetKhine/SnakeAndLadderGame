                                             Snake and Ladder Game

--------------------------------------------------- Start Game  ----------------------------------------------------------
•	Players need to be created first.
•	Once players are created, you can click "Start Game" to begin playing.
Steps to Start the Game
1. Select "Start Game" and input the Board ID along with the IDs of the players who will play.
2. The system checks if the entered Board ID exists.
•	If the Board ID exists, a game is created.
•	A Game ID is automatically generated as a GUID.
Game Rules and Setup
The game supports up to 4 players per match, each assigned a random color from 4 predefined colors.
For each player:
•	A random color is assigned.
•	A GamePlayer entry is created.
•	Each player's starting position is set to 1.
Game Start
•	After setup is complete, the game officially starts.
•	A list is generated showing which player is assigned to which color.

-----------------------------------------------  Play Game Flow  -------------------------------------------------------

When starting a turn:
Input the Game ID and the Player ID whose turn it is.
The system will check:
•	If the Game ID exists.
•	If the Player ID exists in the game.
•	If the player has already won.
Turn Execution
If everything is valid:
Roll the dice and add the dice roll result to the player's current position.
Check if the Board ID exists:
•	If the Board ID exists, calculate the player's new position.
•	If the new position exceeds the board's limit, the player becomes the winner.
Winning Logic
When a player wins:
•	The winner rank is incremented by 1.
•	For a game with 3 players, there will be 2 winners, so the rank system is in place to track winners.
If the player is not a winner yet:
Continue rolling the dice.
•	If the player lands on a snake's mouth, their position will drop to the destination cell.
•	If the player lands on a ladder, their position will climb to the destination cell.
•	The current position is updated to the new position.
Game Completion
When a player wins:
•	That player is removed from the game.
•	The game continues until only 1 player remains, at which point the game is marked as complete.
Next Player's Turn
To proceed with the next turn:
•	Change the Player ID to the next player's ID and execute their turn.


