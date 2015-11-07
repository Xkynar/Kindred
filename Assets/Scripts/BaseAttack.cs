using UnityEngine;
using System.Collections;

public abstract class BaseAttack : ScriptableObject {

    protected string attackName;
    protected int runningSpeed;
    protected float attackDistance;
    protected float damage;
    protected float manaCost;

    public abstract void Init();
    protected virtual void BeforeAttack(Transform selectedTransf, Transform targetedTransf) {}
    protected virtual void AfterAttack(Transform selectedTransf, Transform targetedTransf) { }
    
    public IEnumerator Routine(MonsterController selectedMonster, MonsterController targetedMonster)
    {
        Transform originalTransf = selectedMonster.transform.parent;

        Transform selectedTransf = selectedMonster.transform;
        Transform targetedTransf = targetedMonster.transform;

        // move towards target
        Animator animator = selectedMonster.GetComponent<Animator>();
        animator.SetBool("Running", true);

        while (true)
        {
            Vector3 eulerBefore = selectedTransf.localEulerAngles;
            selectedTransf.LookAt(targetedTransf);
            eulerBefore.y = selectedTransf.localEulerAngles.y;
            selectedTransf.localEulerAngles = eulerBefore;

            selectedTransf.position = Vector3.MoveTowards(selectedTransf.position, targetedTransf.position, Time.deltaTime * runningSpeed);

            if (Vector3.Distance(selectedTransf.position, targetedTransf.position) < attackDistance)
            {
                break;
            }

            yield return null;
        }

        // attack
        BeforeAttack(selectedTransf, targetedTransf);
        animator.SetTrigger(attackName);

        // wait for animation to end @TODO should this really be here? can call function on animation end via mecanim instead
        while (true)
        {
            if (animator.IsInTransition(0) && animator.GetNextAnimatorStateInfo(0).IsName("Running"))
            {
                AfterAttack(selectedTransf, targetedTransf);
                targetedMonster.GetHit(damage);
                break;
            }

            yield return null;
        }

        // return
        while (true)
        {
            Vector3 eulerBefore = selectedTransf.localEulerAngles;
            selectedTransf.LookAt(originalTransf);
            eulerBefore.y = selectedTransf.localEulerAngles.y;
            selectedTransf.localEulerAngles = eulerBefore;

            selectedTransf.position = Vector3.MoveTowards(selectedTransf.position, originalTransf.position, Time.deltaTime * runningSpeed);

            if (Vector3.Distance(selectedTransf.position, originalTransf.position) < 0.1f)
            {
                selectedTransf.position = originalTransf.position;
                selectedTransf.transform.rotation = originalTransf.rotation;
                break;
            }

            yield return null;
        }

        animator.SetBool("Running", false);
        ClientManager.Instance.EndTurn();
    }

    public string GetName()
    {
        return attackName;
    }

    public float GetManaCost()
    {
        return manaCost;
    }
}
