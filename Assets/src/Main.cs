using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Main : MonoBehaviour {

    public int numVillagers;
    public GameObject villagerPalette;

    List<Villager> villagers;

	// Use this for initialization
    void Start()
    {
        // creamos el mapa
        FindManager.SetMap(MapGenerator.Generate());

        villagers = new List<Villager>();
	    // Creamos varios ciudadanos
        for (int i = 0; i < numVillagers; ++i)
        {
            GameObject go = (GameObject)GameObject.Instantiate(villagerPalette, new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), 0), Quaternion.identity);
            Villager vill = go.GetComponent<Villager>();
            villagers.Add(vill);
            ActionManager.AddAction(vill, ActionEnum.PROCASTINATE, 1, false);
        }

	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            List<Villager> idleVill = villagers.FindAll(v => !v.isOnGodDuty());
            if (idleVill.Count > 0)
            {
                Villager vill = idleVill[Random.Range(0, idleVill.Count)];
                ActionManager.AddAction(vill, ActionEnum.CHOP, 3, true);   
            }
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            List<Villager> idleVill = villagers.FindAll(v => !v.isOnGodDuty());
            if (idleVill.Count > 0)
            {
                Villager vill = idleVill[Random.Range(0, idleVill.Count)];
                ActionManager.AddAction(vill, ActionEnum.FARM, 3, true);
            }
        }
	}
}
