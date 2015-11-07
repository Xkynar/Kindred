using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ClientManager : MonoBehaviour
{
    public static ClientManager Instance = null;

    [SerializeField] GameObject ARCamera;
    [SerializeField] float initialMana;
    [SerializeField] float maxMana;
    [SerializeField] float manaIncrement;

    private float currentMana;
    private PlayerNetworkManager networkManager;
    private GameState gameState;
    private MonsterController selectedMonster;
    private Dictionary<string, MonsterController> monsters;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        SetGameState(GameState.SETUP);
        RegisterMonsters();
        
        currentMana = initialMana;
        HUDManager.Instance.InitMana(initialMana, maxMana);
    }

    /*
     * Called by the server, via the PlayerNetworkManager, whenever a new turn is initiated.
     */
    public void StartTurn()
    {
        SetGameState(GameState.SELECT_MONSTER);

        currentMana = Mathf.Min(maxMana, currentMana + manaIncrement);
        HUDManager.Instance.UpdateMana(currentMana);
    }

    public float GetCurrentMana()
    {
        return currentMana;
    }

    public void UpdateMana(float manaCost)
    {
        currentMana -= manaCost;
        HUDManager.Instance.UpdateMana(currentMana);
    }

    /*
     * Sets a reference to network manager responsible for the local player. 
     * This is used for all events that need to communicate with the server and other clients.
     */
    public void SetLocalPlayer(PlayerNetworkManager networkManager)
    {
        this.networkManager = networkManager;
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
     * Sets everything up to start the game.
     */
    public void ReadyUp()
    {
        SetPlayerMonsters();
        SetGameState(GameState.WAIT_TURN);
        networkManager.SetPlayerReady(true);
    }

    /*
     * Sets ownership of all visible monsters. 
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
     * Sets the client's game state.
     */
    public void SetGameState(GameState gameState)
    {
        this.gameState = gameState;

        switch (gameState)
        {
            case GameState.WAIT_TURN:
                Debug.Log("NEW STATE: WAIT_TURN");
                break;

            case GameState.SELECT_MONSTER:
                Debug.Log("NEW STATE: SELECT_MONSTER");
                break;

            case GameState.SELECT_ACTION:
                Debug.Log("NEW STATE: SELECT_ACTION");
                break;

            case GameState.TARGET_MONSTER:
                Debug.Log("NEW STATE: TARGET_MONSTER");
                break;

            case GameState.WAIT_ACTION:
                Debug.Log("NEW STATE: WAIT_ACTION");
                break;
        }
    }

    /*
     * Callback for the player's actions. Handles actions with a state machine.
     */
    public void OnMonsterClick(MonsterController clickedMonster)
    {
        switch (gameState)
        {
            case GameState.SETUP:
                Debug.Log("The game hasn't started yet.");
                break;

            case GameState.WAIT_TURN:
                Debug.Log("Not your turn. Can't do anything.");
                break;

            case GameState.SELECT_MONSTER:
                HandleSelectMonsterState(clickedMonster);
                break;

            case GameState.SELECT_ACTION:
                HandleSelectAction(clickedMonster);
                break;

            case GameState.TARGET_MONSTER:
                HandleTargetMonsterState(clickedMonster);
                break;

            case GameState.WAIT_ACTION:
                Debug.Log("Waiting for attack to end...");
                break;
        }
    }

    /*
     * Handles processing during the SELECT_MONSTER state.
     */
    private void HandleSelectMonsterState(MonsterController clickedMonster)
    {
        if (clickedMonster.IsMine())
        {
            Debug.Log("Selected " + clickedMonster.GetMonsterName() + ".");

            selectedMonster = clickedMonster;
            selectedMonster.Select();
            HUDManager.Instance.OpenAttackUI(clickedMonster.GetAttacks());
            SetGameState(GameState.SELECT_ACTION);
        }
        else
        {
            Debug.Log("You have to choose one of your own monsters.");
        }
                
    }

    /*
     * Handles processing during the TARGET_MONSTER state.
     */
    private void HandleTargetMonsterState(MonsterController clickedMonster)
    {
        if (!clickedMonster.IsMine())
        {
            Debug.Log("Chose to attack " + clickedMonster.GetMonsterName() + ".");
            
            int attackIndex = HUDManager.Instance.GetSelectedAttackIndex();

            networkManager.Attack(selectedMonster.GetMonsterName(), clickedMonster.GetMonsterName(), attackIndex);
            HUDManager.Instance.CloseAttackUI();
            UpdateMana(selectedMonster.GetAttack(attackIndex).GetManaCost());
            SetGameState(GameState.WAIT_ACTION);
        }
        else
        {
            Debug.Log("Re-selected " + clickedMonster.GetMonsterName() + ".");

            selectedMonster.Deselect();
            selectedMonster = clickedMonster;
            selectedMonster.Select();
            HUDManager.Instance.OpenAttackUI(clickedMonster.GetAttacks());
            SetGameState(GameState.SELECT_ACTION);
        }
    }

    /**
     * Handles the process during the SELECT_ACTION state.
     */
    private void HandleSelectAction(MonsterController clickedMonster)
    {
        if(clickedMonster.IsMine())
        {
            Debug.Log("Re-selected " + clickedMonster.GetMonsterName() + ".");

            selectedMonster.Deselect();
            selectedMonster = clickedMonster;
            selectedMonster.Select();
            HUDManager.Instance.OpenAttackUI(clickedMonster.GetAttacks());
            SetGameState(GameState.SELECT_ACTION);
        }
    }

    /*
     * Triggered by the monster currently attacking. Once its animation finishes, the turn is over.
     */
    public void EndTurn()
    {
        // note that this will run on all clients, but the player that initiated the turn should be the only one to end it
        if (gameState == GameState.WAIT_ACTION)
        {
            selectedMonster.Deselect();
            networkManager.EndTurn();
        }   
    }

    /*
     * Initiates an attack. Called via RPC, by the PlayerNetworkManager, so all clients can sync attacks.
     */
    public void Attack(string selectedMonsterName, string targetedMonsterName, int attackIndex)
    {
        MonsterController selectedMonster = monsters[selectedMonsterName];
        MonsterController targetedMonster = monsters[targetedMonsterName];

        selectedMonster.Attack(targetedMonster, attackIndex);
    }
}