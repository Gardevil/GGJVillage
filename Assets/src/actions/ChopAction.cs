using UnityEngine;
using System.Collections;

public class ChopAction : BaseAction
{
    int tripsLeft;
    float speed = 3;///m/s
    float chopTimeMax = 5;///segundos
    float chopTimeActual;
    bool goingToChop;///controla si está yendo al arbol o trayendo el recurso al tótem
    Vector3 treePos; ///posición del arbol que talar

    public override void Initialize()
    {
        tripsLeft = repetitions;
        chopTimeActual = chopTimeMax;
        goingToChop = true;
        treePos = FindManager.getClosestTree();
    }

    public override void Update()
    {
        if (goingToChop)
        {
            /// si no ha llegado a la posición del árbol, va
            if (villager.transform.position != treePos)
            {
                float stepSpeed = speed * Time.deltaTime;
                villager.transform.position = Vector3.MoveTowards(villager.transform.position, treePos, stepSpeed);
            }
            else
            {
                chopTimeActual -= Time.deltaTime;
                if (chopTimeActual <= 0)
                {
                    chopTimeActual = chopTimeMax;
                    goingToChop = false;
                }
            }
        }
        else
        {/// se mueve al totem
            if (villager.transform.position != totemPosition)
            {
                float stepSpeed = speed * Time.deltaTime;
                villager.transform.position = Vector3.MoveTowards(villager.transform.position, totemPosition, stepSpeed);
            }
            else
            {
                tripsLeft--;
                if (tripsLeft > 0)
                {
                    goingToChop = true;
                }
            }
        }
    }

    protected override void Finishing()
    {
        /// se pone como la vuelta de cortar el arbol del último viaje
        tripsLeft = 1;
        goingToChop = false;
    }

    public override bool IsFinished()
    {
        return tripsLeft == 0;
    }
}
