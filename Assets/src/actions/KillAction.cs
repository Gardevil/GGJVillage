using UnityEngine;
using System.Collections;

public class KillAction : BaseAction
{
    private Villager victim;
    float attackingTimeMax = 1;
    float attackingTimeActual;
    bool chasing;

    public override void Initialize()
    {
        victim = FindManager.getClosestVillager(villager);
        villager.moveTo(victim.transform.position);
        attackingTimeActual = 0;
        chasing = true;
    }

    public override void Update()
    {
        if (!chasing)
        {
            return;
        }
        /// estamos persiguiendo a la victima (que se mueve)
        if (!villager.standing && attackingTimeActual==0)
        {            
            villager.moveTo(victim.transform.position);
        }
        else
        {
            /// atacamos durante x tiempo
            attackingTimeActual += Time.deltaTime;
            if (attackingTimeActual >= attackingTimeMax)
            {
                villager.moveTo(victim.transform.position);
                attackingTimeActual = 0;
                victim.lifes--;
            }
        }
    }

    protected override void Finishing()
    {
        chasing = false;
    }

    public override bool IsFinished()
    {
        return !chasing || victim.lifes == 0;
    }
}
