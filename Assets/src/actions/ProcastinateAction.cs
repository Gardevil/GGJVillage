using UnityEngine;
using System.Collections;

public class ProcastinateAction : BaseAction
{
    bool finished;
    float timeToGetBored = 10;///segundos
    bool inTotem;

    public override void Initialize()
    {
        targetPosition = FindManager.getTotemPosition();
        finished = false;
        inTotem = false;
        villager.moveToTotem();
    }

    public override void Update()
    {
        if (!inTotem)
        {
            inTotem = villager.standing;
        }
        else
        {
            /// espera hasta aburrirse
            if (timeToGetBored > 0)
            {
                timeToGetBored -= Time.deltaTime;
            }
            if (timeToGetBored <= 0)
            {
                Finish();
            }
        }
    }

    protected override void Finishing()
    {
        /// no necesita hacer nada antes de considerar acabado
        finished = true;
    }

    public override ActionEnum GetNextAction()
    {
        return ActionEnum.KILL;
    }

    public override bool IsFinished()
    {
        return finished;
    }
}
