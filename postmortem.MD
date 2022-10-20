Well, this is my first postmortem, but seems like a great habit to have, so here we are!  
Wish me luck!  
  
**Theme and first hours**  
The theme here in Italy is announced at Friday midnight, so I brainstormed for 1/2 hours before going to bed.  
I started by writing down on Milanote whatever came to mind, then I got a list of the main game genres from Wikipedia and tried to apply the theme to any of them.  
I ended up with quite a lot of stuff, and came back on first day (late) morning to trim my list and elaborate on the ideas that did pick my interest.  
  
## First day
After a little trim, this is what I had by the start of the first day:    
  
![Screenshot_12](https://user-images.githubusercontent.com/33135141/196859498-0db841a3-71dd-4494-9e63-52618915ea56.png)

  
**Chosing an idea**  
Here is a list of some concepts worth mentioning and the reasons that led me to discard them:  
1. **Teleport**  
Understanding some past events through environmental storytelling while being teleported around every 10 secs  
-> _Out of skillset and resource budget_  
2. **"change world layer"**  
Sidescroller platformer with objects on multiple layers that are switched between every 10 secs  
-> _Was meant to work something like Hue, so I discarded it for the lack of originality. Thinking of it now it could have led to totally different puzzles through the 10 secs limitation, who knows..._  
3. **Vision**  
Being able to see every 10 secs  
-> _Couldn't really think of a fun way to wait in front of a black screen for 10 secs, but I came across some great entries that used this approach_  
4. **State swap**  
Swap character or team every 10 secs  
-> _This actually seemed like a lot of fun, but I wanted to keep it singleplayer and not having to write a decent AI in a rush. I'll probably try to extend more on this one concept._  
  
I'm pretty happy with what I made, but I can't help but think many ideas I discarded could have led to a more interesting gameplay. Probably not as polished though.  
  
**First prototypes**  
At the beginning I thought to make the arena harder to navigate using physics based zero-g controls, and the main character had to be an astronaut wearing a jetpack.  
After making a first prototype it was too frustrating to move around and avoid stuff, so I decided to stick with simpler, snappier controls and increase bullets number, speed and pattern variety instead.  
  
**Graphics and concept**  
While scouting the Web for assets and graphic ideas I came across these cool trees and knight asset packs that ended un in the final game. I then started making the arena and implementing the leaf particles when trees get hit (Yeah, I'm one of those bad guys that waste a lot of time on trivial stuff before they even have a playable demo).  
  
Having a knight in a forest I thought the attackers would be druids, mages or something on this line, so I made some generic magic bullet shader and a flash of light of the same colour to announce the shot.
  
This is what it looked like by the end of the first day:  
  
![Day one progress](https://user-images.githubusercontent.com/33135141/196859551-398e5d16-294e-4a07-a0a9-7f76881fb136.png)

  
## Second day
On day two I took it a little slower: the previous day I realized I couldn't submit the game as a compo because I used third party graphics, so I had one more day to finish.  
  
**Core mechanics and level editor**  
I abandoned the idea of having bullet spawners inside the arena because I couldn't think of a coherent look for them. It just seemed cooler to have some unseen foe hidden in the forest.
    
So I worked on the bullet variants and the spawning system (Before I only had a fixed source bound to an object) and did set up two splines that define the possible spawn points.  
  
Then I wrote a draft of the level editing system. It uses Unity ScriptableObjects to store lists of bullet types associated with the seconds to wait before launch. Spawn position is a randomized point on the previously mentioned splines.  
  
**Music**  
To take a break from development I programmed a drum loop in Ableton Live and recorded guitar and bass for the soundtrack. The final version is pretty much unchanged except for a little editing and mastering done the next day.  
  
## Third day
On day three the core game was finished, more or less.  
  
**The Horse part**  
I found a vector graphics horse I didn't notice before in one of the asset packs I was using, and I wanted to turn it into an NPC, so I made a the little dialogue system you can see in the game.  
The plan was to make both the horse an in-game character and the knight present in the dialogue, so that they could answer each other and react to the dialogue in the world, but I had to cut this for the lack of time.  
Anyway I had time to make two little intro and ending scenes. 
  
**Health system and general polishing**  
I dedicated some time to animating the character and making win and death screens.
Then I decided to add a few lives to make the game less frustrating and made a new HUD to match the style of the dialogue panels. I also made sure hit feedback was as clear as possible (Flash on character sprite, camera shake, sound fx).
  
**Bullets graphics update**  
I wasn't satisfied with the graphics of the bullets (Variants were just different sized color swapped versions) and with the intro gag the game had took a humoristic turn enough to think of some new bullets to reinforce that aspect.  
The first idea was to throw completely random stuff, then I kept it fruit-only for consistency.  
  
At this point it made no sense to keep the flashy light before launch, and it got replaced by a more cartoonish speech bubble. Actually I wanted them to preview the type of bullet, but hadn't that much time and they would have probably been too small to give any significant help, so I kept the danger sign.  
  
**Level design and jam end**  
My last jam hours have been spent making levels and testing them, until I got ten working levels and was happy and tired enough to call it a day and upload.  
  
Some features that didn't make it into the final game:  
Speech bubble bullet preview, banana boomerang trajectory, dialogue featuring both characters, in-game Horse NPC.
  
And here is the final version that you can play on my itch.io:
  
![Gameplay_1](https://user-images.githubusercontent.com/33135141/196859593-b5ac79c4-687e-48ee-8a31-eb92fd5c35c9.png)

  
You might argue that blue bullets had better contrast with the environment, and I would agree with you, but having bullets of multiple solid colors in the scene (to differentiate between types) didn't look that great.  
  
## Conclusion  
Ludum Dare is a great place to try new stuff, see cool ideas and get constructive feedback, and once again it has been a great pleasure to be part of this amazing community.  
  
Thank you for making it this far in this long post, I hope this was useful to you in some way.  
Take care and I hope to see you again in a few months for LD52!  
  
![HorseAndKnight](https://user-images.githubusercontent.com/33135141/196859627-c55e961d-7b3c-469b-b700-67f7fa5fd318.png)
