using UnityEngine;
using System.Collections;

public class DanceAction : BaseAction
{
    float dancingTimeMax = 3;
    float dancingTimeActual;
    float angleSpeed = 500;
    float actualAngle;
    Quaternion initalRot;

    public override void Initialize()
    {
        dancingTimeActual = dancingTimeMax;
        initalRot = villager.gameObject.transform.rotation;
        actualAngle = 0;
    }

    public override void Update()
    {
        dancingTimeActual -= Time.deltaTime;
        actualAngle += angleSpeed * Time.deltaTime;
        villager.gameObject.transform.rotation = Quaternion.AngleAxis(actualAngle, Vector3.up);
    }

    public override bool IsFinished()
    {
        return dancingTimeActual <= 0;
    }

    protected override void Finishing()
    {
        villager.gameObject.transform.rotation = initalRot;
    }
}
