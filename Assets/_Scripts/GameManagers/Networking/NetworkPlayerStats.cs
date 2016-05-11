using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class NetworkPlayerStats : NetworkBehaviour {

    [SyncVar]
    public int Gold = 0;

    [SyncVar]
    public int MinionKills = 0;

    [SyncVar]
    public int HeroKills = 0;

    [SyncVar]
    public int Deaths = 0;

    [SyncVar]
    public int TowersPlaced = 0;

    [SyncVar]
    public int TowersDestroyed = 0;

    [SyncVar]
    public int GoldSpent = 0;

    [SyncVar]
    public int TotalGoldEarned = 0;

    /// <summary>
    /// Add a tower kill for the player
    /// </summary>
    public void AddTowerKill()
    {
        this.TowersDestroyed++;
    }

    /// <summary>
    /// Add a tower kill for a player
    /// </summary>
    /// <param name="player">player that got the tower kill</param>
    public static void AddTowerKill(NetworkPlayerObject player)
    {
        player.GetComponent<NetworkPlayerStats>().AddTowerKill();
    }

    /// <summary>
    /// Add a tower placed for the player
    /// </summary>
    public void AddTowerPlaced()
    {
        this.TowersPlaced++;
    }

    /// <summary>
    /// Add a tower placed for a player
    /// </summary>
    /// <param name="player">The player to add to</param>
    public static void AddTowerPlaced(NetworkPlayerObject player)
    {
        player.GetComponent<NetworkPlayerStats>().AddTowerPlaced();
    }

    /// <summary>
    /// Add a death for the player
    /// </summary>
    public void AddDeath()
    {
        this.Deaths++;
    }

    /// <summary>
    /// Add a death to a player
    /// </summary>
    /// <param name="player">The player to add a death to</param>
    public static void AddDeath(NetworkPlayerObject player)
    {
        player.GetComponent<NetworkPlayerStats>().AddDeath();
    }

    /// <summary>
    /// Incremet the kills for the player
    /// </summary>
    public void AddMinionKill()
    {
        this.MinionKills++;
    }

    /// <summary>
    /// Increment the kills for a player
    /// </summary>
    /// <param name="player">The player who got the kill</param>
    public static void AddMinionKill(NetworkPlayerObject player)
    {
        player.GetComponent<NetworkPlayerStats>().AddMinionKill();
    }

    /// <summary>
    /// Increment the hero kills for the player
    /// </summary>
    public void AddHeroKill()
    {
        this.HeroKills++;
    }

    /// <summary>
    /// Increment the hero kills for a player
    /// </summary>
    /// <param name="player">The player who got the hero kill</param>
    public static void AddHeroKill(NetworkPlayerObject player)
    {
        player.GetComponent<NetworkPlayerStats>().AddHeroKill();
    }

    /// <summary>
    /// A function to add gold to a player's gold count
    /// </summary>
    /// <param name="amount">The amount of gold to add</param>
    public void AddGold(int amount)
    {
        KUSNetworkManager.HostPlayer.CmdUpdateGold(this.gameObject.GetComponent<NetworkIdentity>(), this.Gold + amount);
        this.TotalGoldEarned += amount;
    }

    /// <summary>
    /// A function to subtract gold from a player's gold count
    /// </summary>
    /// <param name="amount">The amount of gold to subtract</param>
    public void SubtractGold(int amount)
    {
        this.Gold -= amount;
    }

    /// <summary>
    /// A function to make sure a player has enough gold to purchase things
    /// </summary>
    /// <param name="amountNeeded">The amount needed to purchase the item</param>
    /// <returns>True if the player has enough, false if they don't</returns>
    public bool HasEnough(int amountNeeded)
    {
        if (this.Gold - amountNeeded >= 0)
            return true;
        else
            return false;
    }

    /// <summary>
    /// A function to be used to make purchases
    /// </summary>
    /// <param name="amount">The amount the thing costs</param>
    /// <returns>True if completed, false if not</returns>
    public bool PurchaseItem(int amount)
    {
        if (HasEnough(amount))
        {
            SubtractGold(amount);
            this.GoldSpent += amount;
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// A function to add gold to a player's gold count
    /// </summary>
    /// <param name="player">The player trying to purchase things</param>
    /// <param name="amount">The amount of gold to add</param>
    public static void AddGold(NetworkPlayerObject player, int amount)
    {
        player.GetComponent<NetworkPlayerStats>().AddGold(amount);
    }

    /// <summary>
    /// A function to subtract gold from a player's gold count
    /// </summary>
    /// <param name="player">The player trying to purchase things</param>
    /// <param name="amount">The amount of gold to subtract</param>
    public static void SubtractGold(NetworkPlayerObject player, int amount)
    {
        player.GetComponent<NetworkPlayerStats>().SubtractGold(amount);
    }

    /// <summary>
    /// A function to make sure a player has enough gold to purchase things
    /// </summary>
    /// <param name="player">The player trying to purchase things</param>
    /// <param name="amountNeeded">The amount needed to purchase the item</param>
    /// <returns>True if the player has enough, false if they don't</returns>
    public static bool HasEnough(NetworkPlayerObject player, int amountNeeded)
    {
        return player.GetComponent<NetworkPlayerStats>().HasEnough(amountNeeded);
    }

    /// <summary>
    /// A function to be used to make purchases
    /// </summary>
    /// <param name="player">The player trying to purchase things</param>
    /// <param name="amount">The amount the thing costs</param>
    /// <returns>True if completed, false if not</returns>
    public static bool PurchaseItem(NetworkPlayerObject player, int amount)
    {
        return player.GetComponent<NetworkPlayerStats>().PurchaseItem(amount);
    }
}
