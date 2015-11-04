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

    /*
     * Sets monster ownership relative to the local player.
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

    /*
     * Returns monster's unique name.
     */
    public string GetMonsterName()
    {
        return monsterName;
    }

    /*
     * Event triggered on mouse click (depends on the presence of a collider).
     */
    void OnMouseDown()
    {
        ClientManager.Instance.OnMonsterClick(this);
    }

    /*
     * Initiates an attack coroutine.
     */
    public void Attack(string attackName, MonsterController targetMonster)
    {
        StartCoroutine(PerformAttack(attackName, targetMonster));
    }

    /*
     * Handled separately in the event that something else needs to occur.
     */
    public void GetHit()
    {
        animator.SetTrigger("Hit");
    }
    
    /*
     * The actual attacking process, where movement and animations are handled.
     */
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

        // wait for animation to end
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

        // stop running, end turn
        animator.SetBool("Running", false);
        ClientManager.Instance.EndTurn();
    }
}