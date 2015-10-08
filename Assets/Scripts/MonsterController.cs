using UnityEngine;
using System.Collections;

public class MonsterController : MonoBehaviour 
{
    private GameManager gameManager;
    private PlayerNetworkManager playerNetworkManager;

	void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
	}

    public void setPlayerNetworkManager(PlayerNetworkManager playerNetworkManager)
    {
        this.playerNetworkManager = playerNetworkManager;
    }

    void OnMouseDown()
    {
        gameManager.monsterClicked(this, playerNetworkManager.isLocalPlayer);
    }
}
