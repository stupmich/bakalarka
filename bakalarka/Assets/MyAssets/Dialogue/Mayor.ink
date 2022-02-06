INCLUDE Globals.ink

-> main

=== main ===
{suppliesVar: {necromancerVar: Greetings! Do you need something?|Is there something else you want to discuss?}|{necromancerVar: Is there something else you want to discuss?| Thats all for now. ->END }}

{suppliesVar:
    + [I heard that you have problems with supplies]
        -> supply()
}
{necromancerVar:
    + [Is there something else I can do for you?]
        -> necromancer()
}

=== supply() ===
Yes, the cargo should have been here yesterday. Can you find out what happened? They probably had some accident along the road...
    + [Yes, I will take care of that.]
    -> thanks_dragon()
    + [No, I don't have time for that.]
    -> bye()

=== thanks_dragon ===
Thank you. You will get your reward after the job is done.
    ~ suppliesVar = false
->main

=== bye ===
Ok then, bye.
->END   

=== necromancer() ===
Yes, some people are saying that there is something strange happening at the graveyard. Some of them even say that they have seen dead coming alive. Can you find out what is going on there?  
    + [Yes, I will take care of that.]
    -> thanks_necromancer()
    + [No, I don't have time for this nonsense.]
    -> bye()
    
=== thanks_necromancer ===
Thank you. You will get your reward after the job is done.
    ~ necromancerVar = false
->main
