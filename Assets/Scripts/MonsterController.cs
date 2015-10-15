using UnityEngine;
using System.Collections;

public class MonsterController : MonoBehaviour 
{
    private bool isMine = false;

    void OnMouseDown()
    {
        // gameManager.monsterClicked(this, playerNetworkManager.isLocalPlayer);

        if (isMine)
        {
            Debug.Log("MINE");
        }
        else
        {
            Debug.Log("NOOOOOOOPE");
        }
    }

    public void SetMine(bool isMine)
    {
        this.isMine = isMine;
    }

    public bool IsMine()
    {
        return isMine;
    }
}
