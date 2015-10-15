using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    private PlayerNetworkManager localPlayer;
    private GameState currentState;

    [SerializeField] GameObject readyButton;

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

    public void SetLocalPlayer(PlayerNetworkManager localPlayer)
    {
        this.localPlayer = localPlayer;
    }

    public void ShowReadyButton()
    {
        this.readyButton.SetActive(true);
    }

    public void SetPlayerReady(bool ready)
    {
        Debug.Log("Clicked ready button");

        localPlayer.SetPlayerReady(ready);
    }
}
