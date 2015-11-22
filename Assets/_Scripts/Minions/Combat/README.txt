COMBAT READ ME
Bryan Fawber
-------------

Combat for minions is fairly simple at the moment. When a new target enters
the attack range it sets it as the new destination, attacks it until it's dead,
and then continues to it's end target.

The IMinion_Attack interface should be used for every combat script for every minion 
type. 

THe Minion_Basic script is for the basic minion type. It just has a basic minion 
attack that's fairly weak, and a slow attack.