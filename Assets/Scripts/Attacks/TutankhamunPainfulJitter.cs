using UnityEngine;
using System.Collections;

public class TutankhamunPainfulJitter : BaseAttack
{

    public override void Init()
    {
        attackName = "Painful Jitter";
        runningSpeed = 1;
        damage = 10f;
        manaCost = 10f;
        attackDistance = 1f;
    }
}
