INCLUDE globals.ink
VAR dialogId = "talkedToShadow"

-> start

=== start ===
- Ah, you're finally awake. It's about time.
    * [Who... who are you? Where am I?]
        -> mid
    * [\* Stay silent \*]
        - Silent, huh?
        -> mid

=== mid ===
- I'm a part of you, a shadow of your former self. As for where you are, you're in a dungeon. A rather unfortunate predicament.
    * [A shadow of my former self? What do you mean? And why can't I remember anything?]
        -> options
    * [\* Stay silent \*]
        - ...
        -> options
        
=== options ===
- Your memory is a puzzle, scattered and hidden. Perhaps for a reason. But we can piece it together, if you make the right choices
    * [How can I trust you? But if you're part of me, you must want to help. What should I do?]
        -> good
    * [I need to get out of here. What are my options?]
        -> neutral
    * [Why should I care about my past? I need to get out of here, and I don't care how.]
        -> bad
    
=== good ===
    ~ humanPoints += 10 
    Trust is earned, not given. But you're right, I do want to help. There's a guard outside your cell. He's not the brightest. If you appeal to his sense of empathy, he might just help you out.
    -> ending
    
=== neutral ===
    Straight to the point, I see. There's a guard outside. He's weak and easily intimidated. A show of force might convince him to let you go, or at least get him to drop his keys
    -> ending
=== bad === 
    ~ humanPoints -= 10 
    Practical and level-headed. I like that. The guard outside can be dealt with in a few ways: you could talk to him, threaten him, or wait for an opportunity to escape when he's distracted.
    -> ending
    
=== ending ===
- Remember, every choice has consequences. Tread carefully, for your past is a shadow that follows, and your future is shaped by the steps you take.
~ talkedToShadow = 1

-> END