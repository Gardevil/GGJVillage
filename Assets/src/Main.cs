using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Main : MonoBehaviour {

    public int numVillagers;
    public GameObject villagerPalette;

    Map map;

	// Use this for initialization
    void Start()
    {
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
	}
}
