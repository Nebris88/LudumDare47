# LudumDare47

This is my entry in Ludum Dare 47. 
Theme: Stuck in a Loop

You are stuck time maze, unable to live, unable to die.  You must solve the maze by collecting relics that will aid you in your next life. Use their power to find the exit and escape the time maze!

When you die, you are reborn at the beginning.  The world around you is reset.  However, there are special relics scattered throughout the maze that will give you special powers... but only in your next life.  You might have to kill yourself to progress further.

Character Abilities:
	Standard:
		Move
		Jump
		Crouch
	Relic:
		Sprint - can get to spaces quicker (open gate, go through gate before closes)
		Climb - can climb up certain walls
		Double Jump - can jump a second time while in air
		Fireproof - can go through area that would normally kill you
		Phase - can walk through doors
		Telekenises - can interact with buttons from a distance
		Blink - can teleport to visible locations
		
	Some areas can be accessed by more than one ability (double jump can jump up ledge you first climbed over)!
	
World Tile Map
	Empty
	Ground
	Monument
	Relic
	Climbable
	Spikes
	Flames
	Button
	Pressure Plate
	Door
	ToggleState (can exist or not, some disappear forever, others come back on timer, etc)
	Falling (falls down to next tile)

Solve Order:
		
		
	/ -	4 - 6
	| /	|
	5 - 1 - 2
	  \	| 
		3
		
					Req				Relic				Blocker				Task
	1-2				none			Sprint				1-2 blocks 1-3/4	Collect Sprint
	1-3				Sprint			Climb				1-3 blocks 1-2		Press button and go through door quickly with sprint - collect Climb
	1-4				Climb			Fireproof			1-4 block 1-3		Climb pillar, Collect Fireproof
	1-5				Fireproof		DJ+S									Walk through fire, Collect Double Jump,
	1-2-1-3			DJ+S			Sprint+Climb							Access 2 with double jump, collect Sprint, then Collect Climb
	1-4-5-1-2		Sprint+Climb	DJ+FP+Sprint		1-4 block 1-3		Collect Fireproof, Sprint Door to 5, then Collect Double Jump, Collect Sprint
	1-2-1-5-4-5-3	DJ+FP+Sprint	S+C+DJ+FP			1-3 blocks 1-5		Collect Sprint, Collect DJ, 5-4, collect FP, return to 5, long jump to 3, collect climb		
	1-4-6			S+C+DJ+FP		Escape!									climb pillar, mega super long jump to 6, freedom!
	
	
	1-2		none	dj		dj,fp,s
	1-3		s		dj
	1-4		c		sc		s,c,dj,fp
	1-5		fp		s,dj,fp
	4-5		sc		s,dj,fp,
	
	
	*Cannot collect Sprint and Climb at same time without double jump
	*Cannot climb 4 after collecting sprint, cannot collect climb after climbing 4
	*Cannot enter 2 or 4 after entering 3 (can be frustrating if they do this one first life, as they need 2 to get 3... maybe only disallow 2 after using sprint)
	
	trigger tile block to fall.  
	tile block allows player to step up to entrance to area 2 while blocking access to area 3.  
	area 2 is accessable via doulbe jump without dropping tile block.
	undropped tile block is necessary for climbing pillar.
	upon entering 3, a different tile block prevents acces to area 2 and another to 5
	
	
	
	
	

