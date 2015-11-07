using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerNetworkManager : NetworkBehaviour
{
    [SyncVar] public string role;
    [SyncVar] public string nickname;

    private bool ready = false;

    void Start()
    {
        // PlayerPrefs.SetString("role", "P2");
        // PlayerPrefs.SetString("nickname", "Xkynar");

        if (isLocalPlayer)
        {
            // share setup info with server and all clients
            CmdSetup(PlayerPrefs.GetString("role"), PlayerPrefs.GetString("nickname"));

            // store local player for later access
            ClientManager.Instance.SetLocalPlayer(this);

            if (role != "SPEC")
            {
                // display a READY button
                HUDManager.Instance.DisplayReadyButton();
            }
        }
    }

    /****************************************************************************
     * Setup Process
     * 
     * Stores a client's role and info as SyncVars so all clients can access them.
     * Stores references to Player1 and Player2 for later access.
     ****************************************************************************/

    [Command]
    void CmdSetup(string role, string nickname)
    {
        this.role = role;
        this.nickname = nickname;

        if (role == "P1")
        {
            ServerManager.Instance.SetPlayerP1(this);
        }
        else if (role == "P2")
        {
            ServerManager.Instance.SetPlayerP2(this);
        }
    }

    /****************************************************************************
     * Ready-up Process
     * 
     * Alerts all players of a client's ready state.
     * If both players are ready, the game is initiated.
     ****************************************************************************/

    public bool IsReady()
    {
        return ready;
    }

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
           if (ServerManager.Instance.ArePlayersReady())
           {
               ServerManager.Instance.StartGame();
           }   
        }
    }

    /****************************************************************************
     * Turn-based Events
     * 
     * Responsible for starting and ending the turns for all players.
     ****************************************************************************/

    [ClientRpc]
    public void RpcSetTurn()
    {
        //Hide ready button
        HUDManager.Instance.HideReadyButton();

        if (isLocalPlayer)
        {
            Debug.Log("It's YOUR turn!");
            ClientManager.Instance.StartTurn();
        }
        else
        {
            Debug.Log("It's " + nickname + "'s turn!");
        }
    }

    public void EndTurn()
    {
        if (isLocalPlayer)
        {
            ClientManager.Instance.SetGameState(GameState.WAIT_TURN);
            CmdEndTurn();
        }
    }

    [Command]
    public void CmdEndTurn()
    {
        ServerManager.Instance.SwitchTurn();
    }

    /****************************************************************************
     * Attack Events
     * 
     * Responsible for coordenating and syncing attacks and animations across all 
     * clients.
     ****************************************************************************/

    public void Attack(string selectedMonster, string targetedMonster, int attackIndex)
    {
        if (isLocalPlayer)
        {
            CmdAttack(selectedMonster, targetedMonster, attackIndex);
        }
    }

    [Command]
    public void CmdAttack(string selectedMonster, string targetedMonster, int attackIndex)
    {
        RpcAttack(selectedMonster, targetedMonster, attackIndex);
    }

    [ClientRpc]
    public void RpcAttack(string selectedMonster, string targetedMonster, int attackIndex)
    {
        ClientManager.Instance.Attack(selectedMonster, targetedMonster, attackIndex);
    }

    /****************************************************************************
     * Game Over
     * 
     * Responsible for alerting all players that the game is now over.
     ****************************************************************************/
    public void GameOver()
    {
        if (isLocalPlayer)
        {
            CmdGameOver();
        }
    }

    [Command]
    public void CmdGameOver()
    {
        PlayerNetworkManager opponent = ServerManager.Instance.GetOpponent(this);
        RpcGameOver(opponent.nickname);
    }

    [ClientRpc]
    public void RpcGameOver(string winner)
    {
        if (winner == this.nickname)
        {
            Debug.Log("You win!");
        }
        else
        {
            Debug.Log(nickname + "wins");
        }

        ClientManager.Instance.EndGame();
    }
}
