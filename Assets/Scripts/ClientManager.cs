using UnityEngine;
using UnityEngine.Networking;
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
    private MonsterController targetedMonster;
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
     * 
     */
    public float GetCurrentMana()
    {
        return currentMana;
    }

    /*
     * Responsible for updating current mana and respective HUD elements.
     */
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
     * Sets everything up to start the game.
     */
    public void ReadyUp()
    {
        SetPlayerMonsters();
        SetGameState(GameState.WAIT_TURN);
        networkManager.SetPlayerReady(true);
    }

    /*
    * Called by the server, via the PlayerNetworkManager, whenever a new turn is initiated.
    */
    public void StartTurn()
    {
        if (IsGameOver())
        {
            networkManager.GameOver();
        }
        else
        {
            SetGameState(GameState.SELECT_MONSTER);

            currentMana = Mathf.Min(maxMana, currentMana + manaIncrement);
            HUDManager.Instance.UpdateMana(currentMana);
        }    
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
     * Checks whether all of a player's monsters are dead and the game should end.
     */
    private bool IsGameOver()
    {
        foreach (MonsterController monster in monsters.Values)
        {
            if (monster.IsMine() && monster.IsAlive())
            {
                return false;
            }
        }

        return true;
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
                break;

            case GameState.SELECT_MONSTER:
                break;

            case GameState.SELECT_ACTION:
                break;

            case GameState.TARGET_MONSTER:
                break;

            case GameState.WAIT_ACTION:
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
        if (clickedMonster.IsMine() && clickedMonster.IsAlive())
        {
            selectedMonster = clickedMonster;
            selectedMonster.Select();
            HUDManager.Instance.OpenAttackUI(clickedMonster.GetAttacks());

            SetGameState(GameState.SELECT_ACTION);
        }
        else
        {
            Debug.Log("Choose a valid monster to attack with.");
        }
                
    }

    /**
     * Handles the process during the SELECT_ACTION state.
     */
    private void HandleSelectAction(MonsterController clickedMonster)
    {
        // if we select one of our own, update the selected monster
        if(clickedMonster.IsMine() && clickedMonster.IsAlive())
        {
            selectedMonster.Deselect();
            selectedMonster = clickedMonster;
            selectedMonster.Select();
            HUDManager.Instance.OpenAttackUI(clickedMonster.GetAttacks());

            SetGameState(GameState.SELECT_ACTION);
        }
    }

    /*
 * Handles processing during the TARGET_MONSTER state.
 */
    private void HandleTargetMonsterState(MonsterController clickedMonster)
    {
        // target a living enemy monster
        if (!clickedMonster.IsMine() && clickedMonster.IsAlive())
        {
            int attackIndex = HUDManager.Instance.GetSelectedAttackIndex();

            targetedMonster = clickedMonster;
            targetedMonster.Target();
            HUDManager.Instance.CloseAttackUI();
            UpdateMana(selectedMonster.GetAttack(attackIndex).GetManaCost());
            networkManager.Attack(selectedMonster.GetMonsterName(), clickedMonster.GetMonsterName(), attackIndex);

            SetGameState(GameState.WAIT_ACTION);
        }

        // if we select one of our own, update the selected monster
        else if (clickedMonster.IsMine() && clickedMonster.IsAlive())
        {
            selectedMonster.Deselect();
            selectedMonster = clickedMonster;
            selectedMonster.Select();
            HUDManager.Instance.OpenAttackUI(clickedMonster.GetAttacks());

            SetGameState(GameState.SELECT_ACTION);
        }

        // 
        else
        {
            Debug.Log("Choose a valid target, or re-select a monsters to attack with");
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
            targetedMonster.Deselect();
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

    /*
     * Ends the game.
     */
    public void EndGame()
    {
        Invoke("ReturnToMainMenu", 5f);
    }

    private void ReturnToMainMenu()
    {
        networkManager.StopServer();
    }
}