using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerNetworkManager : NetworkBehaviour
{
    [SyncVar] public string squadList;
    [SyncVar] public string side;

    public GameObject monsterExample;

    void Start()
    {
        PlayerPrefs.SetString("side", "LEFT");

        if (isLocalPlayer)
        {
            CmdSyncSetup(Random.Range(1, 100).ToString(), PlayerPrefs.GetString("side"));
        }

        StartCoroutine("Setup");
    }

    IEnumerator Setup()
    {
        while (squadList == null || squadList == "")
        {
            yield return null;
        }

        SpawnSquad();
    }

    void SpawnSquad()
    {
        GameObject squadParent = null;
        int squadCount = 5;
        float squadSpacing = 0.2f;
        float initialPos = -squadSpacing * (squadCount - 1) / 2;

        if (side == "LEFT")
        {
            squadParent = GameObject.FindGameObjectWithTag("LeftSquad");
        }
        else if (side == "RIGHT")
        {
            squadParent = GameObject.FindGameObjectWithTag("RightSquad");
        }

        for (int i = 0; i < squadCount; i++)
        {
            GameObject monster = Instantiate(monsterExample, Vector3.zero, Quaternion.identity) as GameObject;
            monster.transform.parent = squadParent.transform;
            monster.transform.localPosition = new Vector3(initialPos + squadSpacing * i, 0f, 0f);
        }
    }

    [Command]
    void CmdSyncSetup(string squadList, string side)
    {
        this.squadList = squadList;
        this.side = side;
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
