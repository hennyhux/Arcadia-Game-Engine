# Arcadia Game Engine
An open-source 2D side-scrolling game engine that is written using the MonoGame framework and C#. Currently in the works with many new features planned.
In the current state, the entire level of Mario 1-1 is playable. 

Features in the works:
- Greater extendability of classes (i.e cleaning up dead code that breaks cohesion).
- Improved collision detection algorithm using a sweep-prune approach.
- Change XML parser to use CV instead for greater control in making levels. 
- Improved physics, get rid of hardcoded variables in favor of runtime computations.
- Quake-like console for debugging/inputting cheat codes (CURRENT FOCUS - IN PROGRESS).


Future Plans:
- Implement concurrency to utilize multiple threads for heavy computations (rendering, collision detection, physics calculations).
- 

Completed features
- Improve big-level performance by modifying collision detection to prune objects not in viewport from the collision list. 
