# Audio Reactive Fractal Tree Forest

## Aim

My aim for this project was to create a procedurally generated forest with many different trees that reacted to music.
Whilst I expected this to be a challenge, I was posed with some difficulties that I did not expect which came in the form of performance related issues.
Despite this I think I reached what I set out to achieve and learned a lot along the way.

## Content

- FPS Controller
- Fractal Tree
- Procedural Tree Spawner
- Audio Reactive Objects
- Problems Faced

### FPS Controller

First of all I knew I needed a way to navigate my scene and explore the forest I was about to create. I am very comfortable in creating a FPS controller and decided that it was probably bect to do this first and get it out of the way. 
I created an FPS controller that allows you to move with the WASD keys. There is also the ability to fly whilst holding the spacebar. You can also descend if you hold the F key.
As well as this, all rotations are controlled with the mouse. I clamped the mouse Y rotations so that the camera cannot flip around. The Escape key will unlock your mouse from the game screen and allow you to click out of the window.
*Can be viewed in the Movement script and the Mouse Camera Script*

### Fractal Tree

Before I created the method in which the trees were spawned, I decided it was best to create a tree to spawn in the first place. I decided to go with the process of recursion and create a fractal tree forest. 
The tree it self is made of two main parts. The branches and the segments. The number of branches decides how many splits there will be in the next section after the initial branch duplicates itself. The amount of segments dictates how big the tree will become, creating more unique and interesting patterens. Everytime a new segment instatiates itself it is completely unique. 
I used [this source] as base for my tree spawner (https://www.snip2code.com/Snippet/973268/Implementation-of-Poisson-Disk-Sampling-)
*This is all within the FarctalTree script and the Grow script*

### Procedural Tree Spawner

I anticipated that this was going to be the toughest task in the project coming into it. I had never procedurally generated anything before and had very little knowledge on where to start. However, I was pointed in the right direction with a suggestion that advised me to consider Poisson Disc Sampling. This set me on the right track as it is what I aimed to emulate with my tree spawner. 
I created a grid based system in determining where potential points of spawning could be. I then created a system that checked if a potential point was within the radius of any other points. If it wasn't it was an accpetable place to spawn, if it was it was removed and a new point would be created elsewhere.
Once all points were determined within a specific region size, the trees would spawn creating a multycolored forest.
*This is all within the Sampling script and the Grow script that is attached to the Spawner Gameobject*

### Audio Reactive Objects

Initially I had issues getting my trees to react with music the way I intended for them to. The way in which the trees were recursively growing caused manipulating the transforms and scales of the branches to be difficult. However, I settled on altering each part of the tree individually, rather than trying to get it to dance altogether at once.
*This is all within the AudioPlayer, AudioScale and AudioVisual scripts*

### Problems Faced

The majority of the problems I faced were performance related, specifically with rendering. My method of recursively creating the trees caused major lag and rendering problems. The amount of objects on screen severly limited the performance and forced me to limit the amount of trees I cuold spawn in game. This also limited the amount of branches and segments any tree coul dhave too as if there were too many, the editor would crash. Despite this, I am stilll proud of what I created and I am determined to optimise it better in the future.

Here you can find a [link](https://www.youtube.com/watch?v=uL1IK6VGnMY) to my video showcasing my assignment. The first half of the video displays less magical looking trees in a neat formation caused from my Sampling script. The second half of the video shows how I intended all trees to look and bhave but was unfortunately hindered by performance related problems.
