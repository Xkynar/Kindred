using UnityEngine;
using System.Collections;

public class EmberBlazingRage : BaseAttack
{
    [SerializeField] GameObject effect;

    public override void Init()
    {
        attackName = "Blazing Rage";
        runningSpeed = 1;
        damage = 10f;
        manaCost = 10f;
        attackDistance = 1f;
    }

    protected override void BeforeAttack(Transform selectedTransf, Transform targetedTransf)
    {
        Instantiate(effect, selectedTransf.position, selectedTransf.rotation);
    }
}
