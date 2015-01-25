using UnityEngine;
using System.Collections;

public class ProcastinateAction : BaseAction
{
    bool finished;
    float timeToGetBored = 30;///segundos
    bool inTotem;

    public override void Initialize()
    {
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
                float prev = timeToGetBored;
                timeToGetBored -= Time.deltaTime;
                if (prev > 3 && timeToGetBored <= 3)
                {
                    villager.gameObject.GetComponent<Speaker>().SpeakUp("Me Booooring...", 2.5f);
                }
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
