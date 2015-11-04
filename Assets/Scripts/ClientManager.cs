using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ClientManager : MonoBehaviour
{
    public static ClientManager instance = null;

    private PlayerNetworkManager networkManager;
    private GameState gameState;
    private MonsterController selectedMonster;

    [SerializeField] GameObject readyButton;
    [SerializeField] GameObject ARCamera;

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

    void Start()
    {
        gameState = GameState.SETUP;
    }

    public void SetLocalPlayer(PlayerNetworkManager localPlayer)
    {
        this.networkManager = localPlayer;
    }

    /*
     * Displays a "Ready" button on the main HUD. Called by the local player.
     */
    public void ShowReadyButton()
    {
        this.readyButton.SetActive(true);
    }

    /*
     * Callback for the "Ready" button clicked event. Alerts the local player to update its copies.
     */
    public void SetPlayerReady(bool ready)
    {
        networkManager.SetPlayerReady(ready);
        SetMonsters();
        SetGameState(GameState.WAIT_TURN);
    }

    /*
     * Sets monster ownership for local player. 
     */
    public void SetMonsters()
    {
        List<GameObject> imageTargets = ARCamera.GetComponent<TrackableList>().GetImageTargets();

        foreach (GameObject imageTarget in imageTargets)
        {
            MonsterController monsterController = imageTarget.GetComponent<MonsterController>();

            if (monsterController != null)
            {
                monsterController.SetMine(true);
            }
        }
    }

    /*
     * Sets the client's game state
     */
    public void SetGameState(GameState gameState)
    {
        this.gameState = gameState;

        //Handle this gamestate
        switch (gameState)
        {
            case GameState.WAIT_TURN:
                Debug.Log("Wait Turn");
                break;

            case GameState.PICK_MONSTER:
                Debug.Log("Pick Monster");
                break;

            case GameState.SELECT_ACTION:
                Debug.Log("Select Action");
                break;

            case GameState.TARGET_MONSTER:
                Debug.Log("Target Monster");
                break;

            case GameState.WAIT_ACTION:
                Debug.Log("Wait Action");
                break;
        }
    }

    public void ClickedMonster(MonsterController clicked)
    {
        switch (gameState)
        {
            case GameState.WAIT_TURN:
                Debug.Log("Can't do shit atm");
                break;

            case GameState.PICK_MONSTER:
                if (selectedMonster.IsMine())
                {
                    Debug.Log("Selected " + clicked.gameObject.name);
                    selectedMonster = clicked;
                    SetGameState(GameState.TARGET_MONSTER);
                }
                else
                {
                    Debug.Log("Can't do that, son");
                }
                
                break;

            case GameState.SELECT_ACTION:
                break;

            case GameState.TARGET_MONSTER:
                if (!selectedMonster.IsMine())
                {
                    Debug.Log("Chose to attack " + clicked.gameObject.name);
                    selectedMonster.Attack("Peace Breaker", clicked);
                    SetGameState(GameState.WAIT_ACTION);
                }
                else
                {
                    Debug.Log("Can't attack your own. Should re-pick here.");
                }
                
                break;

            case GameState.WAIT_ACTION:
                Debug.Log("Waiting for attack to end...");
                break;
        }
    }

    public void EndTurn()
    {
        SetGameState(GameState.WAIT_TURN);
        networkManager.EndTurn();
    }
}