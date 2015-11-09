using UnityEngine;
using System.Collections;

public class TutankhamunSuarezPassion : BaseAttack
{

    public override void Init()
    {
        attackName = "Suarez Passion";
        runningSpeed = 1;
        damage = 12f;
        manaCost = 5f;
        attackDistance = 1f;
    }
}
