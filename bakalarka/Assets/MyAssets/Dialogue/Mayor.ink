INCLUDE globals.ink


-> main

=== main ===
{suppliesVar: {necromancerVar: Greetings! Do you need something?|Maybe there is more you can do for me...}|{necromancerVar: Maybe there is more you can do for me...| -> questsTaken}}

{suppliesVar:
    + [I heard that you have problems with supplies]
        -> supply()
}

{necromancerVar:
    + [I heard some rumours about graveyard. What do you know about that?]
        -> necromancer()
}


=== supply() ===
Yes, the cargo should have been here yesterday. Can you find out what happened? They probably had some accident along the road...
    + [Yes, I will take care of that.]
    ~ questVariable = "Supplies"
    -> thanks_supplies()
    + [No, I don't have time for that.]
    -> bye()

=== thanks_supplies ===
Thank you. You will get your reward after the job is done.
    ~ suppliesVar = false
    ~ questVariable = ""
->main

=== bye ===
Ok then, bye.
->END   

=== necromancer() ===
Yes, some people say that there is something strange happening at the graveyard. Some of them even say that they have seen dead coming alive. Can you find out what is going on there?  
    + [Yes, I will take care of that.]
    ~ questVariable = "Necromancer"
    -> thanks_necromancer()
    + [No, I don't have time for this nonsense.]
    -> bye()
    
=== thanks_necromancer ===
Thank you. You will get your reward after the job is done.
    ~ necromancerVar = false
    ~ questVariable = ""
->main


=== questsTaken ===
Did you find out whats going on?

{Supplies:
    + [Dragon killed carriers and destroyed supplies. ]
    ~ questVariable = "Supplies"
    -> dragon()
}
{Necromancer:
    + [The necromancer is dead.]
    ~ questVariable = "Necromancer"
    -> rewardGraveyard()
}
{Dragon:
    +[I killed the dragon. Here is its head.]
    ~ questVariable = "Dragon"
    -> rewardDragon()
}
+ [Not yet.]
->getIntoIt 

=== dragon ===
Kill that beast and bring me his head.
    + [I will take care of that, but it wont be cheap.]
    ~ questVariable = "Dragon"
    -> thanks_dragon()

=== rewardGraveyard ===
Here is yourd reward.
~ questVariable = ""
~ Necromancer = false
-> questsTaken

=== rewardDragon===
Thank you for slaying that beast. Here is your reward.
~ questVariable = ""
~ Dragon = false
-> questsTaken

=== thanks_dragon ===
Thank you. Money is not a problem. Everything for the safety of my people...
    ~ questVariable = ""
    
-> questsTaken

=== getIntoIt ===
So what are you waiting for? Get into it!
-> END