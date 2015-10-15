using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerNetworkManager : NetworkBehaviour
{
    [SyncVar] public bool setup = false;
    [SyncVar] public string role;
    [SyncVar] public string nickname;
    [SyncVar] public bool ready;

    void Start()
    {
        PlayerPrefs.SetString("role", "P1");
        PlayerPrefs.SetString("nickname", "vascozzz");

        if (isLocalPlayer)
        {
            // alert server to setup all copies
            CmdSyncSetup(PlayerPrefs.GetString("role"), PlayerPrefs.GetString("nickname"));

            // setup game manager
            GameManager.instance.SetLocalPlayer(this);
        }

           StartCoroutine("Setup");
    }

    IEnumerator Setup()
    {
        while (!setup)
        {
            yield return null;
        }

        if (role != "SPEC")
        {
            GameManager.instance.ShowReadyButton();
        }
    }

    [Command]
    void CmdSyncSetup(string role, string nickname)
    {
        this.role = role;
        this.nickname = nickname;
        this.setup = true;
    }

    [Command]
    void CmdSetPlayerReady(bool ready)
    {
        this.ready = ready;
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
