﻿using UnityEngine;
using System.Collections;

public class ChopAction : ExploitResurceAction {

    protected override Resource GetClosestResource()
    {
        return FindManager.getClosestTree(villager.transform.position);
    }

    protected override string GetSpeakText()
    {
        return "Me chopping!";
    }
}
