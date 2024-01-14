VAR totalPoints = 0
-> main

=== main ===
Устроить резню?
    * [Так точно!]
        -> chosen(10)
    * [Никак нет!]
        -> chosen(-10)
    * [Ты кто..]
        -> chosen(0)
        
=== chosen(points) ===
Получено очков: {points}!
~totalPoints = points 
-> END