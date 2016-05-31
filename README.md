# MinesweeperLike
MinesweeperLike is a single-player puzzle game. The goal of the game is to uncover all the squares that do not contain mines without being "blown up" by clicking on a square with a mine underneath. The location of the mines is discovered by a process of logic. Clicking on the game board will reveal what is hidden underneath the chosen square or squares (a large number of blank squares may be revealed in one go if they are adjacent to each other). Some squares are blank but some contain numbers (1 to 8), each number being the number of mines adjacent to the uncovered square. To help avoid hitting a mine, the location of a suspected mine can be marked by flagging it with the right mouse button. The game is won once all blank squares have been uncovered without hitting a mine, any remaining mines not identified by flags being automatically flagged by the computer. 

  ![game-completed](img/game-completed-example.gif?raw=true)

However, in the event that a game is lost and the player mistakenly flags a safe square, that square will either appear with a red X covering the mine (denoting it as safe). 

  ![not-a-mine](img/not-a-mine-example.gif?raw=true)
  
## Game Actions

  These actions are all available from the ‘Game’ menu.
  
- ### New Game
  Starts a new game, with a random initial state.
    
  - Click the **Game** menu, and then click **New Game**.

  ![not-a-mine](img/new-game-example.gif?raw=true)
___
- ### Restart
  Resets the current game to its initial state.
  
  - Click the **Game** menu, and then click **Restart**.

  ![not-a-mine](img/restart-game-example.gif?raw=true)
___
- ### Solve
  Transforms the puzzle instantly into its solved state.
  
  - Click the **Game** menu, and then click **Solve**.
  
  ![not-a-mine](img/solve-game-example.gif?raw=true)
___
- ### Exit
  Closes the application entirely.
  
  - Click the **Game** menu, and then click **Exit**.

## The Board

  MinesweeperLike has three standard boards to choose from, each progressively more difficult.
  
  These boards are all available from the ‘Type’ menu. It's contain a list of preset game settings. Selecting one of these will start a new random game with the parameters specified.
  
<table style="width:100%">
  <tr>
    <td>Width</th>
    <td>Height</td>
    <td>Mines</td>
  </tr>
  <tr>
    <td rowspan="2">9</th>
    <td rowspan="2">9</td>
    <td>10</td>
  </tr>
  <tr>
    <td>35</td>
  </tr>
 <tr>
    <td rowspan="2">16</th>
    <td rowspan="2">16</td>
    <td>40</td>
  </tr>
  <tr>
    <td>99</td>
  </tr>
 <tr>
    <td rowspan="2">30</th>
    <td rowspan="2">16</td>
    <td>130</td>
  </tr>
  <tr>
    <td>170</td>
  </tr>
</table>

  
- ### 9x9
  
  Click the **Type** menu, and then click **"9x9, 10 mines"** or **"9x9, 35 mines"**.
  
  ![not-a-mine](img/type9x9-example.gif?raw=true)
___
- ### 16x16

  Click the **Type** menu, and then click **"16x16, 40 mines"** or **"16x16, 99 mines"**.
  
  ![not-a-mine](img/type16x16-example.gif?raw=true)
___
- ### 30x16

  Click the **Type** menu, and then click **"30x16, 130 mines"** or **"30x16, 170 mines"**.
  
  ![not-a-mine](img/type30x16-example.gif?raw=true)

## TODO

 - Custom board
 - Help menu
 - Keyboard shortcuts
 - Never lose on first click
