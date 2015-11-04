using UnityEngine;
using System.Collections;

public class MonsterController : MonoBehaviour
{
    public float runningSpeed;
    public float turningSpeed;

    private string monsterName;
    private Animator animator;
    private bool isMine = false;


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
        StartCoroutine(PerformAttack(attackName, targetMonster));
    }

    public void GetHit()
    {
        animator.SetTrigger("Hit");
    }

    private IEnumerator PerformAttack(string attackName, MonsterController targetedMonster)
    {
        Transform originalTransf = this.transform.parent;
        Transform targetedTransf = targetedMonster.transform;

        // move towards target
        animator.SetBool("Running", true);

        while (true)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, targetedTransf.position, Time.deltaTime * runningSpeed);

            if (Vector3.Distance(this.transform.position, targetedTransf.position) < 0.1f)
            {
                this.transform.position = targetedTransf.position;
                break;
            }

            yield return null;
        }

        // attack
        animator.SetTrigger(attackName);

        // wait for animation to end @TODO should this really be here? can call function on animation end via mecanim instead
        while (true)
        {
            if (animator.IsInTransition(0) && animator.GetNextAnimatorStateInfo(0).IsName("Running"))
            {
                targetedMonster.GetHit();
                break;
            }

            yield return null;
        }

        // return
        while (true)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, originalTransf.position, Time.deltaTime * runningSpeed);

            if (Vector3.Distance(this.transform.position, originalTransf.position) < 0.1f)
            {
                this.transform.position = originalTransf.position;
                break;
            }

            yield return null;
        }

        animator.SetBool("Running", false);
        ClientManager.instance.EndTurn();
    }
}