using UnityEngine;
using System.Collections;

public class MonsterController : MonoBehaviour 
{
    private bool isMine = false;

    void OnMouseDown()
    {
        // gameManager.monsterClicked(this, playerNetworkManager.isLocalPlayer);
        if (isMine)
            Debug.Log("MINE");
        else
            Debug.Log("NOT MINE");
    }

    /*
     * Sets monster ownership to local player
     */
    public void SetMine(bool isMine)
    {
        this.isMine = isMine;
    }

    /*
     * Returns true if monster belongs to local player
     */
    public bool IsMine()
    {
        return isMine;
    }
}
