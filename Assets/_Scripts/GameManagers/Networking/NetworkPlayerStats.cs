using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class NetworkPlayerStats : NetworkBehaviour {

    [SyncVar]
    public int Gold = 0;

    [SyncVar]
    public int Kills = 0;

    [SyncVar]
    public int Deaths = 0;

    /// <summary>
    /// A function to add gold to a player's gold count
    /// </summary>
    /// <param name="amount">The amount of gold to add</param>
    public void AddGold(int amount)
    {
        this.Gold += amount;
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
