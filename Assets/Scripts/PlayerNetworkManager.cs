using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerNetworkManager : NetworkBehaviour
{
    [SyncVar] public string squadList;

    void Start()
    {
        if (isLocalPlayer)
        {
            CmdSyncSquadList(Random.Range(1, 100).ToString());
        }

        StartCoroutine("Setup");
    }

    IEnumerator Setup()
    {
        while (squadList == null || squadList == "")
        {
            yield return null;
        }

        Debug.Log("Player " + netId + " Squad: " + squadList);
    }

    [Command]
    void CmdSyncSquadList(string squadList)
    {
        this.squadList = squadList;
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

    void Update()
    {
        if (isLocalPlayer && Input.GetMouseButtonDown(0))
        {
            CmdDoSomething();
        }
    }
}
