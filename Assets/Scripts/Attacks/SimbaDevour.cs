﻿using UnityEngine;
using System.Collections;

public class SimbaDevour : BaseAttack
{

    public override void Init()
    {
        attackName = "Devour";
        runningSpeed = 1;
        damage = 20f;
        manaCost = 10f;
        attackDistance = 1f;
    }
}
