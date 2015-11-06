using UnityEngine;
using System.Collections;

public class BonesRageCataclysm : BaseAttack {

    public override void Init()
    {
        attackName = "Rage Cataclysm";
        runningSpeed = 2;
        damage = 10f;
        manaCost = 10f;
        attackDistance = 1f;
    }
}
