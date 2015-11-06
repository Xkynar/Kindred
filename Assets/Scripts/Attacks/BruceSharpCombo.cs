using UnityEngine;
using System.Collections;

public class BruceSharpCombo : BaseAttack
{

    public override void Init()
    {
        attackName = "Sharp Combo";
        runningSpeed = 1;
        damage = 10f;
        manaCost = 10f;
        attackDistance = 1f;
    }
}
