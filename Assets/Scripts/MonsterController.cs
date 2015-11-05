using UnityEngine;
using System.Collections;

public class MonsterController : MonoBehaviour
{
    public float runningSpeed;
    public float turningSpeed;

    private string monsterName;
    private Animator animator;
    private bool isMine = false;

    public BaseAttack[] attacks;
    
    void Start()
    {
        monsterName = this.gameObject.name;
        animator = this.GetComponent<Animator>();
    }

    void OnMouseDown()
    {
        if (isMine)
        {
            Debug.Log("Mine");
        }
        else
        {
            Debug.Log("NOT mine.");
        }

        ClientManager.instance.ClickedMonster(this);
    }

    /*
     * Sets monster ownership to local player.
     */
    public void SetMine(bool isMine)
    {
        this.isMine = isMine;
    }

    /*
     * Returns true if monster belongs to local player.
     */
    public bool IsMine()
    {
        return isMine;
    }

    public string GetMonsterName()
    {
        return monsterName;
    }

    public void Attack(string attackName, MonsterController targetMonster)
    {
        StartCoroutine(attacks[0].routine(this, targetMonster));
    }

    public void GetHit()
    {
        animator.SetTrigger("Hit");
    }
}