using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

public class OverseerSpawnerDeath : NetworkBehaviour, IKillable {
    
	public void OnDeath()
    {
        int goldAmount = GetComponent<ProfileSystem>().Worth;
        KUSNetworkManager.HostPlayer.CmdAddGoldToHeros(goldAmount);

        Destroy(this.gameObject);
    }    
}
