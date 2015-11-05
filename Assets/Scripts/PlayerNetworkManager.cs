using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerNetworkManager : NetworkBehaviour
{
    public bool ready = false;
    [SyncVar] public string role;
    [SyncVar] public string nickname;

    void Start()
    {
        //PlayerPrefs.SetString("role", "P1");
        //PlayerPrefs.SetString("nickname", "Xkynar");

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
        this.role = role;
        this.nickname = nickname;

        if (role == "P1")
        {
            ServerManager.instance.SetPlayerP1(this);
        }
        else if (role == "P2")
        {
            ServerManager.instance.SetPlayerP2(this);
        }
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

        if (isServer)
        {
            if (ServerManager.instance.ArePlayersReady())
            {
                ServerManager.instance.StartGame();
            }
        }
    }

    [ClientRpc]
    public void RpcSetTurn()
    {
        if (isLocalPlayer)
        {
            Debug.Log("It's YOUR turn");
            ClientManager.instance.SetGameState(GameState.PICK_MONSTER);
        }
        else
        {
            Debug.Log("It's " + nickname + "'s turn");
        }
    }

    public void EndTurn()
    {
        if (isLocalPlayer)
        {
            ClientManager.instance.SetGameState(GameState.WAIT_TURN);
            CmdEndTurn();
        }
    }

    [Command]
    public void CmdEndTurn()
    {
        ServerManager.instance.SwitchTurn();
    }

    public void Attack(string selectedMonster, string targetedMonster, string attackName)
    {
        if (isLocalPlayer)
        {
            CmdAttack(selectedMonster, targetedMonster, attackName);
        }
    }

    [Command]
    public void CmdAttack(string selectedMonster, string targetedMonster, string attackName)
    {
        RpcAttack(selectedMonster, targetedMonster, attackName);
    }

    [ClientRpc]
    public void RpcAttack(string selectedMonster, string targetedMonster, string attackName)
    {
        ClientManager.instance.Attack(selectedMonster, targetedMonster, attackName);
    }
}
