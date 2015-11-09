using UnityEngine;
using System.Collections;

public class BruceLunge : BaseAttack
{

    public override void Init()
    {
        attackName = "Lunge";
        runningSpeed = 2;
        damage = 10f;
        manaCost = 5f;
        attackDistance = 1f;
    }
}
