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
        PlayerPrefs.SetString("role", "P1");
        PlayerPrefs.SetString("nickname", "vascozzz");

        if (isLocalPlayer)
        {
            // 
            CmdSetup(PlayerPrefs.GetString("role"), PlayerPrefs.GetString("nickname"));

            //
            ClientManager.instance.SetLocalPlayer(this);

            if (role != "SPEC")
            {
                ClientManager.instance.ShowReadyButton();
            }
        }
    }

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

    public void SetPlayerReady(bool ready)
    {
        if (isLocalPlayer)
        {
            CmdSetPlayerReady(ready);
        }
    }





















    [Command]
    void CmdDoSomething()
    {
        RpcDoSomething();
    }

    [ClientRpc]
    void RpcDoSomething()
    {
        Debug.Log("Player " + netId + " clicked a button.");
    }

    [ClientCallback]
    void Update()
    {
        if (isLocalPlayer && Input.GetMouseButtonDown(0))
        {
            CmdDoSomething();
        }
    }
}
