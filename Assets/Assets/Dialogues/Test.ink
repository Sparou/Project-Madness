INCLUDE globals.ink
-> main

=== main ===
Устроить резню?
    * {humanPoints < -10} [Так точно!]
        -> chosen(-10)
    * {humanPoints > 10} [Никак нет!]
        -> chosen(10)
    * [Ты кто..]
        -> chosen(0)
        
=== chosen(points) ===
Получено очков: {points}!
~ humanPoints += points 
-> END