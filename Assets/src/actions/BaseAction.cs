using UnityEngine;
using System.Collections;

public abstract class BaseAction
{
    protected Villager villager;
    protected int repetitions;
    protected ActionEnum actionType;
    protected Vector3 targetPosition;// = FindManager.getTotemPosition();

    private bool markedToFinish = false;

    public void SetData(Villager villager, int repetitions, ActionEnum actionType)
    {
        this.villager = villager;
        this.repetitions = repetitions;
        this.actionType = actionType;
    }

    public abstract void Initialize();
    public abstract void Update();
    public abstract bool IsFinished();

    public virtual ActionEnum GetNextAction()
    {
        return ActionEnum.PROCASTINATE;
    }

    protected abstract void Finishing();
    public void Finish()
    {/// Con esto evitamos que se haga la finalizacion varias veces
        if (!markedToFinish)
        {
            markedToFinish = true;
            Finishing();
        }
    }
}
