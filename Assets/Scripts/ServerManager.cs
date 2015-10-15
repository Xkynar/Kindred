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

    public void setPlayerP1(PlayerNetworkManager p1)
    {
        this.p1 = p1;
    }

    public void setPlayerP2(PlayerNetworkManager p2)
    {
        this.p2 = p2;
    }
}
