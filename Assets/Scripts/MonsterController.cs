using UnityEngine;
using System.Collections;

public class MonsterController : MonoBehaviour
{
    public float runningSpeed;
    public float turningSpeed;

    private Animator animator;
    private bool isMine = false;


    void Start()
    {
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

    public void Attack(string attackName, MonsterController targetMonster)
    {
        StartCoroutine(PerformAttack(attackName, targetMonster));
    }

    public void GetHit()
    {
        animator.SetTrigger("Hit");
    }

    private IEnumerator PerformAttack(string attackName, MonsterController targetMonster)
    {
        Vector3 originalPos = this.transform.position;
        Vector3 targetPos = targetMonster.gameObject.transform.position;
        Vector3 goalPos = targetPos;

        // move towards target
        animator.SetBool("Running", true);

        while (this.transform.position != goalPos)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, goalPos, Time.deltaTime * runningSpeed);
            yield return null;
        }

        // attack
        animator.SetTrigger(attackName);

        // wait for animation to end @TODO should this really be here? can call function on animation end via mecanim instead
        while (true)
        {
            if (!animator.GetBool(attackName))
            {
                targetMonster.GetHit();
                goalPos = originalPos;
                break;
            }

            yield return null;
        }

        // return
        while (this.transform.position != goalPos)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, goalPos, Time.deltaTime * runningSpeed);
            yield return null;
        }

        animator.SetBool("Running", false);
        ClientManager.instance.EndTurn();
    }
}