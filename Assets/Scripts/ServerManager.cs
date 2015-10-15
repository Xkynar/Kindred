using UnityEngine;
using System.Collections;

public class ServerManager : MonoBehaviour
{
    public static ServerManager instance = null;

    public PlayerNetworkManager p1, p2;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    /*
     * Stores Player1 for later use
     */
    public void SetPlayerP1(PlayerNetworkManager p1)
    {
        this.p1 = p1;
    }

    /*
     * Stores Player2 for later use
     */
    public void SetPlayerP2(PlayerNetworkManager p2)
    {
        this.p2 = p2;
    }

    /*
     * Returns true if all players are ready
     */
    public bool ArePlayersReady()
    {
        return p1 != null && p2 != null && p1.ready && p2.ready;
    }
}
