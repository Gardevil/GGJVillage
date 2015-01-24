using UnityEngine;
using System.Collections;

public class ProcastinateAction : BaseAction
{
    bool finished;
    float timeToGetBored = 10;///segundos
    bool inTotem;
    float speed = 1;///m/s

    public override void Initialize()
    {
        finished = false;
        inTotem = false;
    }

    public override void Update()
    {
        if (!inTotem)
        {
            /// si no ha llegado a la posición del chamán, va
            float stepSpeed = speed * Time.deltaTime;
            villager.transform.position = Vector3.MoveTowards(villager.transform.position, totemPosition, stepSpeed);
            inTotem = villager.transform.position == totemPosition;
        }
        else
        {/// se mueve al azar
            if (timeToGetBored > 0)
            {
                timeToGetBored -= Time.deltaTime;
            }
            if (timeToGetBored <= 0)
            {
                float stepSpeed = speed * Time.deltaTime;
                Vector3 randPos = new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), 0);
                villager.transform.position = Vector3.MoveTowards(villager.transform.position, randPos, stepSpeed);
            }
        }
    }

    protected override void Finishing()
    {
        /// no necesita hacer nada antes de considerar acabado
        finished = true;
    }

    public override bool IsFinished()
    {
        return finished;
    }
}
