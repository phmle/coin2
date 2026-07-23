# CoinCollect

A simple 2D tile-based game built in C# with Raylib. Walk the player around the map, collect every coin (`C`), avoid enemies (`M`), then reach the exit (`E`).

### Features

- Tile-based map with walls, floor, coins, exit, and enemies
- WASD or arrow keys to move, escape to quit
- Walls block movement, exit stays locked until all coins are collected
- Enemies wander randomly every half second
- Win by reaching the exit; lose if an enemy catches you three times

### Requirements
.NET 8 SDK

Run
in bash: dotnet run

Default map: `maps/valid.txt`

Other maps:

dotnet run -- maps/level1.txt

dotnet run -- maps/level2.txt

dotnet run -- maps/bigmap.txt



## Map format
Plain text file:
11111
1P0C1
100E1
11111
1:  Wall 
0: Floor 
P: Player start (exactly one) 
C: Coin (at least one) 
E: Exit (exactly one) 
M: Enemy spawn (optional) 

The map must be rectangular, fully enclosed by walls, and have exactly 1 player, 1 exit, and at least 1 coin.
<img width="500" height="590" alt="image" src="https://github.com/user-attachments/assets/38a11c0c-8d91-4b77-b2a5-8a1add7cb8d3" />
