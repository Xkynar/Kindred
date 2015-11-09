using UnityEngine;
using System.Collections;

public class TutankhamunPainfulJitter : BaseAttack
{

    public override void Init()
    {
        attackName = "Painful Jitter";
        runningSpeed = 2;
        damage = 5f;
        manaCost = 3f;
        attackDistance = 1f;
    }
}
