using UnityEngine;
using System.Collections;

public class DanceAction : BaseAction
{
    float dancingTimeMax = 3;
    float dancingTimeActual;

    public override void Initialize()
    {
        dancingTimeActual = dancingTimeMax;
    }

    public override void Update()
    {
        dancingTimeActual -= Time.deltaTime;
        //villager.gameObject.transform.rotation.
    }

    public override bool IsFinished()
    {
        return dancingTimeActual <= 0;
    }

    protected override void Finishing()
    {
    }
}
