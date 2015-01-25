using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class RitualController : MonoBehaviour, IEventListener
{
    public float startTimeToEclipse = 300;
    private float actualTimeToEclipse;

    public int woodNeeded = 300;
    public int initialWood = 150;
    private int actualWood;

    public int foodNeeded = 300;
    public int initialFood = 150;
    private int actualFood;

    public int woodConsuming = 5;
    public int foodConsuming = 5;
    public float timeToConsume = 10;
    private float actualConsumingTime;

    void Start()
    {
        actualTimeToEclipse = startTimeToEclipse;
        actualWood = initialWood;
        actualFood = initialFood;

        actualConsumingTime = 0;

        EventManager.instance.AddListener(this as IEventListener, "ResourceAdd");
    }

    // Update is called once per frame
    void Update()
    {
        actualTimeToEclipse -= Time.deltaTime;
        /// Cada X segundos descontamos recursos
        actualConsumingTime += Time.deltaTime;
        if (actualConsumingTime >= timeToConsume)
        {
            actualConsumingTime -= timeToConsume;
            actualFood = Math.Max(0, actualFood - foodConsuming);
            actualWood = Math.Max(0, actualWood - woodConsuming);
        }
    }

    public float GetTimeToEclipse()
    {
        return actualTimeToEclipse;
    }

    public int GetCurrentWood()
    {
        return actualWood;
    }

    public int GetCurrentFood()
    {
        return actualFood;
    }

    bool IEventListener.HandleEvent(IEvent evt)
    {
        KeyValuePair<ActionEnum, int> pair = (KeyValuePair<ActionEnum, int>)evt.GetData();
        if (pair.Key == ActionEnum.CHOP)
        {
            actualWood += pair.Value;
        }
        else if (pair.Key == ActionEnum.FARM)
        {
            actualFood += pair.Value;
        }
        return true;
    }


    internal bool IsReady()
    {
        return actualFood >= foodNeeded && actualWood >= woodNeeded;
    }
}
