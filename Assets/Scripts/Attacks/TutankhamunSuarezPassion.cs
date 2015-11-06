using UnityEngine;
using System.Collections;

public class TutankhamunSuarezPassion : BaseAttack
{

    public override void Init()
    {
        attackName = "Suarez Passion";
        runningSpeed = 1;
        damage = 10f;
        manaCost = 10f;
        attackDistance = 1f;
    }
}
