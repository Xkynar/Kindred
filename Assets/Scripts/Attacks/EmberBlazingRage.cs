using UnityEngine;
using System.Collections;

public class EmberBlazingRage : BaseAttack
{
    [SerializeField] GameObject effect;
    private GameObject effectObj;

    public override void Init()
    {
        attackName = "Blazing Rage";
        runningSpeed = 1;
        damage = 70f;
        manaCost = 20f;
        attackDistance = 1f;
    }

    protected override void BeforeAttack(Transform selectedTransf, Transform targetedTransf)
    {
        effectObj = Instantiate(effect, targetedTransf.position, targetedTransf.rotation) as GameObject;
        effectObj.transform.parent = targetedTransf.transform.parent;
        effectObj.transform.localPosition = new Vector3(0f, 0.5f, 0f);
    }

    protected override void AfterAttack(Transform selectedTransf, Transform targetedTransf)
    {
        Destroy(effectObj);
    }
}
