using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerNetworkManager : NetworkBehaviour
{
    public bool ready = false;
    public string role;
    public string nickname;

    void Start()
    {
        //PlayerPrefs.SetString("role", "P1");
        //PlayerPrefs.SetString("nickname", "vascozzz");

        if (isLocalPlayer)
        {
            // Share setup info
            CmdSetup(PlayerPrefs.GetString("role"), PlayerPrefs.GetString("nickname"));

            // Save local player for later access
            ClientManager.instance.SetLocalPlayer(this);

            if (role != "SPEC")
            {
                // Show Ready Button on HUD
                ClientManager.instance.ShowReadyButton();
            }
        }
    }
    
    /* 
     * Setup:
     * Server will spread every client's role and nickname to all copies
     * Sever will store Player1 and Player2 for later access
     */
    [Command]
    void CmdSetup(string role, string nickname)
    {
        RpcSetup(role, nickname);

        if (role == "P1")
        {
            ServerManager.instance.SetPlayerP1(this);
        }
        else if (role == "P2")
        {
            ServerManager.instance.SetPlayerP2(this);
        }
    }

    [ClientRpc]
    void RpcSetup(string role, string nickname)
    {
        this.role = role;
        this.nickname = nickname;
    }

    /* 
     * Ready:
     * Server spreads ready info to all copies
     * The server copy check if all players are ready and starts the game
     */

    public void SetPlayerReady(bool ready)
    {
        if (isLocalPlayer)
        {
            CmdSetPlayerReady(ready);
        }
    }

    [Command]
    void CmdSetPlayerReady(bool ready)
    {
        RpcSetPlayerReady(ready);
    }

    [ClientRpc]
    void RpcSetPlayerReady(bool ready)
    {
        this.ready = ready;

        if(isServer)
        {
            if(ServerManager.instance.ArePlayersReady())
            {
                Debug.Log("Starting game");
            }
        }
    }

}
