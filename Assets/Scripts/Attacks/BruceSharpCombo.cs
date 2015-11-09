using UnityEngine;
using System.Collections;

public class BruceSharpCombo : BaseAttack
{

    public override void Init()
    {
        attackName = "Sharp Combo";
        runningSpeed = 2;
        damage = 15f;
        manaCost = 7f;
        attackDistance = 1f;
    }
}
