using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Main : MonoBehaviour
{

    public int numVillagers;
    public GameObject villagerPalette;
    RitualController ritualController;

    Map map;

    // Use this for initialization
    void Start()
    {
        ritualController = GetComponent<RitualController>();
        // creamos el mapa
        map = MapGenerator.Generate();
        FindManager.SetMap(map);

        // Creamos varios ciudadanos
        for (int i = 0; i < numVillagers; ++i)
        {
            GameObject go = (GameObject)GameObject.Instantiate(villagerPalette, new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), 0), Quaternion.identity);
            Villager vill = go.GetComponent<Villager>();
            map.AddVillager(go);
            ActionManager.AddAction(vill, ActionEnum.PROCASTINATE, 1, false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        map.getVillagers().RemoveAll(v => v.GetComponent<Villager>().lifes <= 0);
        if (map.getVillagers().Count == 0)
        {
            Application.Quit();
            Debug.Log("JUEGO PERDIDO POR ANIQUILACIÓN!!!");
        }
        else if (ritualController.GetTimeToEclipse()<=0)
        {
            if (ritualController.IsReady())
            {
                Application.Quit();
                Debug.Log("JUEGO GANADO!!! :-)");
            }
            else 
            {
                Application.Quit();
                Debug.Log("JUEGO PERDIDO POR TIEMPO/FALTA DE RECURSOS!!!");
            }
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            List<GameObject> idleVill = map.getVillagers().FindAll(v => !v.GetComponent<Villager>().isOnGodDuty());
            if (idleVill.Count > 0)
            {
                Villager vill = idleVill[Random.Range(0, idleVill.Count)].GetComponent<Villager>();
                ActionManager.AddAction(vill, ActionEnum.CHOP, 3, true);
            }
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            List<GameObject> idleVill = map.getVillagers().FindAll(v => !v.GetComponent<Villager>().isOnGodDuty());
            if (idleVill.Count > 0)
            {
                Villager vill = idleVill[Random.Range(0, idleVill.Count)].GetComponent<Villager>();
                ActionManager.AddAction(vill, ActionEnum.FARM, 3, true);
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            List<GameObject> idleVill = map.getVillagers();
            if (idleVill.Count > 0)
            {
                foreach (GameObject go in idleVill)
                {
                    ActionManager.AddAction(go.GetComponent<Villager>(), ActionEnum.DANCE, 1, true);
                }
            }
        }

        string timeLeft = "Time Left: " + ritualController.GetTimeToEclipse();
        string woodNeeded = "Wood: " + ritualController.GetCurrentWood() + "/" + ritualController.woodNeeded;
        string foodNeeded = "Food: " + ritualController.GetCurrentFood() + "/" + ritualController.foodNeeded;
        Debug.Log(timeLeft + ", " + woodNeeded + ", " + foodNeeded);
    }
}
