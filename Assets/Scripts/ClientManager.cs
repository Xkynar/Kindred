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
    private Dictionary<string, MonsterController> monsters;

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
        SetGameState(GameState.SETUP);
        RegisterMonsters();
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
        SetPlayerMonsters();
        SetGameState(GameState.WAIT_TURN);
        networkManager.SetPlayerReady(ready);
    }

    /*
     * Stores all the monsters in the scene for fast lookups.
     */
    private void RegisterMonsters()
    {
        List<GameObject> imageTargets = ARCamera.GetComponent<TrackableList>().GetImageTargets();
        monsters = new Dictionary<string, MonsterController>();

        foreach (GameObject imageTarget in imageTargets)
        {
            MonsterController monsterController = imageTarget.GetComponentInChildren<MonsterController>();

            if (monsterController != null)
            {
                monsters.Add(monsterController.GetMonsterName(), monsterController);
            }
        }
    }

    /*
     * Sets monster ownership of all visible monsters for local player. 
     */
    private void SetPlayerMonsters()
    {
        List<GameObject> imageTargets = ARCamera.GetComponent<TrackableList>().GetActiveImageTargets();

        foreach (GameObject imageTarget in imageTargets)
        {
            MonsterController monsterController = imageTarget.GetComponentInChildren<MonsterController>();

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
                if (clicked.IsMine())
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
                if (!clicked.IsMine())
                {
                    Debug.Log("Chose to attack " + clicked.gameObject.name);
                    networkManager.Attack(selectedMonster.GetMonsterName(), clicked.GetMonsterName(), "Peace Breaker");
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
        networkManager.EndTurn();
    }

    public void Attack(string selectedMonsterName, string targetedMonsterName, string attackName)
    {
        MonsterController selectedMonster = monsters[selectedMonsterName];
        MonsterController targetedMonster = monsters[targetedMonsterName];

        selectedMonster.Attack(attackName, targetedMonster);
    }
}