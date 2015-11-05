using UnityEngine;
using System.Collections;

public abstract class BaseAttack : ScriptableObject {

    protected string attackName;
    protected int runningSpeed;

    protected abstract void Init();
    protected virtual void BeforeAttack() {}
    protected virtual void AfterAttack() {}
    
    public IEnumerator routine(MonsterController selectedMonster, MonsterController targetedMonster)
    {
        Init();

        Transform originalTransf = selectedMonster.transform.parent;
        Transform targetedTransf = targetedMonster.transform;

        // move towards target
        Animator animator = selectedMonster.GetComponent<Animator>();
        animator.SetBool("Running", true);

        while (true)
        {
            selectedMonster.transform.position = Vector3.MoveTowards(selectedMonster.transform.position, targetedTransf.position, Time.deltaTime * runningSpeed);

            if (Vector3.Distance(selectedMonster.transform.position, targetedTransf.position) < 0.1f)
            {
                selectedMonster.transform.position = targetedTransf.position;
                break;
            }

            yield return null;
        }

        // attack
        BeforeAttack();
        animator.SetTrigger(attackName);

        // wait for animation to end @TODO should this really be here? can call function on animation end via mecanim instead
        while (true)
        {
            if (animator.IsInTransition(0) && animator.GetNextAnimatorStateInfo(0).IsName("Running"))
            {
                AfterAttack();
                targetedMonster.GetHit();
                break;
            }

            yield return null;
        }

        // return
        while (true)
        {
            selectedMonster.transform.position = Vector3.MoveTowards(selectedMonster.transform.position, originalTransf.position, Time.deltaTime * runningSpeed);

            if (Vector3.Distance(selectedMonster.transform.position, originalTransf.position) < 0.1f)
            {
                selectedMonster.transform.position = originalTransf.position;
                break;
            }

            yield return null;
        }

        animator.SetBool("Running", false);
        ClientManager.instance.EndTurn();
    }

    public string GetName()
    {
        return attackName;
    }
}
