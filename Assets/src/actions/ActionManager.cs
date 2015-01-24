﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ActionManager : MonoBehaviour {

    private static ActionManager instance;

    private Dictionary<ActionEnum, Type> enumTypeMap;

    private Dictionary<Villager, List<BaseAction>> activities;

	// Use this for initialization
	void Start () {
        activities = new Dictionary<Villager, List<BaseAction>>();
        enumTypeMap = new Dictionary<ActionEnum, Type>();
        enumTypeMap.Add(ActionEnum.CHOP, typeof(ChopAction));
        enumTypeMap.Add(ActionEnum.PROCASTINATE, typeof(ProcastinateAction));
        instance = this;
	}

    /// <summary>
    /// Añade una acción nueva a un ciudadano, haciendo que acabe la anterior si estaba haciendo algo
    /// </summary>
    /// <param name="villager">ciudadano que realizará la acción</param>
    /// <param name="action">que acción realizará</param>
    /// <param name="repetitions">cuántas veces realizará la acción</param>
    /// <param name="godOrder">la acción la dictamina el jugador</param>
    public static void AddAction(Villager villager, ActionEnum action, int repetitions, bool godOrder)
    {
        if (!instance.activities.ContainsKey(villager))
        {/// Si el ciudadano no estaba asignado, creamos la estructura
            instance.activities.Add(villager, new List<BaseAction>());
        }
        else if (instance.activities[villager].Count>0)
        {/// Si el ciudadano estaba haciendo cosas, se le dice que acabe
            instance.activities[villager][0].Finish();
            /// Si tenía alguna tarea encolada se le quita
            instance.activities[villager].RemoveRange(1, instance.activities[villager].Count - 1);
        }
        villager.SetBusy(godOrder);
        instance.activities[villager].Add(instance.GetAction(villager, action, repetitions));
    }
	
	// Update is called once per frame
    void Update()
    {
        Debug.Log(activities.Keys.Count);
        foreach (Villager vill in activities.Keys)
        {
            List<BaseAction> actionList = activities[vill];
            /// Si tiene acciones por hacer
            if (actionList.Count > 0)
            {
                actionList[0].Update();
                /// si la acción ha acabado completamente, se quita
                if (actionList[0].IsFinished())
                {
                    actionList.RemoveAt(0);
                    /// si no tiene más cosas que hacer, se pone a procastinar
                    if (actionList.Count == 0)
                    {
                        AddAction(vill, ActionEnum.PROCASTINATE, 3, false);
                    }
                    /// comienza la otra acción
                    actionList[0].Initialize();
                }
            }
        }
    }

    private BaseAction GetAction(Villager villager, ActionEnum action, int repetitions)
    {
        BaseAction result = (BaseAction)Activator.CreateInstance(enumTypeMap[action]);
        result.SetData(villager, repetitions, action);
        return result;
    }
}