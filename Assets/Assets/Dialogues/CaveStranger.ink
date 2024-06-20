INCLUDE globals.ink
- Greetings, traveler.
    * [Hi there, stranger?]
        -> good
    * [...]
        -> neutral
    
=== good ===
~ humanPoints += 2
- I'm not a stranger at all, you know I like you, would you like to buy a couple of items at a discount?
    * [Not right now maybe...]
    
    {
        - humanPoints <= -10:
            -> endingWarn
        - else:
            -> endingNeutral
    }

=== neutral ===
- Okay... I'm just a simple merchant, would you like to come into my shop?
    * [Not right now maybe...]
    {
        - humanPoints <= -10:
            -> endingWarn
        - else:
            -> endingNeutral
    }
    
    * [...]
        ~ humanPoints -= 4
    
    {
        - humanPoints <= 0:
            -> endingWarn
        - else:
            -> endingNeutral
    }
=== endingNeutral ===
 - Good...
-> END

=== endingWarn ===
 - Hm... I see darkness in you, traveler. Be careful.
-> END