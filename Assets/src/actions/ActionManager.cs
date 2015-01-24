using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class ActionManager : MonoBehaviour {

    private static ActionManager instance;

    private Dictionary<ActionEnum, Type> enumTypeMap;

    //private Dictionary<Villager, List<BaseAction>> activities;
    private Dictionary<Villager, BaseAction> activities;

	// Use this for initialization
	void Start () {
        //activities = new Dictionary<Villager, List<BaseAction>>();
        activities = new Dictionary<Villager, BaseAction>();
        enumTypeMap = new Dictionary<ActionEnum, Type>();
        enumTypeMap.Add(ActionEnum.CHOP, typeof(ChopAction));
        enumTypeMap.Add(ActionEnum.PROCASTINATE, typeof(ProcastinateAction));
        enumTypeMap.Add(ActionEnum.FARM, typeof(FarmAction));
        enumTypeMap.Add(ActionEnum.KILL, typeof(KillAction));

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
        /*if (!instance.activities.ContainsKey(villager))
        {/// Si el ciudadano no estaba asignado, creamos la estructura
        }
        else if (instance.activities[villager].Count>0)
        {/// Si el ciudadano estaba haciendo cosas, se le dice que acabe
            instance.activities[villager][0].Finish();
            /// Si tenía alguna tarea encolada se le quita
            instance.activities[villager].RemoveRange(1, instance.activities[villager].Count - 1);
        }
        villager.SetBusy(godOrder);
        instance.activities[villager].Add(instance.GetAction(villager, action, repetitions));*/

        ////// SIN LISTAS
        if (instance.activities.ContainsKey(villager))
        {
            instance.activities.Remove(villager);
        }
        villager.SetBusy(godOrder);
        instance.activities.Add(villager, instance.GetAction(villager, action, repetitions));
    }
	
	// Update is called once per frame
    void Update()
    {
        /// Quitamos los muertos
        foreach (var s in activities.Where(p => p.Key.lifes <= 0).ToList())
        {
            activities.Remove(s.Key);
        }

        Dictionary<Villager, ActionEnum> aux = new Dictionary<Villager, ActionEnum>();
        /// pasamos por las acciones
        foreach (Villager vill in activities.Keys)
        {
            /*List<BaseAction> actionList = activities[vill];
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
            }*/

            ///// SIN LISTAS
            BaseAction action = activities[vill];
            action.Update();
            if (action.IsFinished())
            {
                aux.Add(vill, action.GetNextAction());
            }
        }
        foreach (Villager v in aux.Keys)
        {
            AddAction(v, aux[v], 3, false);
        }
    }

    private BaseAction GetAction(Villager villager, ActionEnum action, int repetitions)
    {
        if (!enumTypeMap.ContainsKey(action))
        {
            throw new Exception("Acción no mapeada: " + action);
        }
        BaseAction result = (BaseAction)Activator.CreateInstance(enumTypeMap[action]);
        result.SetData(villager, repetitions, action);
        result.Initialize();
        return result;
    }
}
