﻿using UnityEngine;
using System.Collections;

public class FarmAction : ExploitResurceAction
{
    protected override Resource GetClosestResource()
    {
        return FindManager.getClosestFarmingField(villager.transform.position);
    }

    protected override string GetSpeakText()
    {
        return "Me farming!";
    }
}