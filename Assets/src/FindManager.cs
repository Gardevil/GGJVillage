using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class FindManager : MonoBehaviour
{

    private static FindManager instance;

    private static GameObject tree;
    private static GameObject farmingField;

    private Map currMap;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        tree = new GameObject();
        tree.AddComponent<Resource>();
        tree.transform.position = new Vector3(3.5f, 3.5f, 0);
        farmingField = new GameObject();
        farmingField.AddComponent<Resource>();
        farmingField.transform.position = new Vector3(-3.5f, -3.5f, -0);
    }

    public static void SetMap(Map map)
    {
        instance.currMap = map;
    }

    public static GameObject getTotem()
    {
        return instance.currMap.GetTotem();
    }

    public static GameObject getWarehouse()
    {
        return instance.currMap.GetWarehouse();
    }

    private static Resource getClosestResource(List<GameObject> list, Vector3 position)
    {
        Resource res = null;
        IEnumerable<GameObject> freeTrees = list.Where(t => !t.GetComponent<Resource>().inUse);
        if (freeTrees.Any())
        {
            GameObject tree = freeTrees.OrderBy(t => (t.transform.position - position).sqrMagnitude).First();
            res = tree.GetComponent<Resource>();
        }
        return res;
    }

    public static Villager getClosestVillager(Villager excluded)
    {
        /// si solo hay un aldeano, el objetivo será él mismo y se suicidará
        Villager vill = excluded;
        IEnumerable<GameObject> villagers = instance.currMap.getVillagers().Where(v => v != excluded.gameObject);
        if (villagers.Any())
        {
            GameObject go = villagers.OrderBy(v => (v.transform.position - excluded.transform.position).sqrMagnitude).First();
            vill = go.GetComponent<Villager>();
        }
        return vill;
    }

    public static Resource getClosestTree(Vector3 position)
    {
        return getClosestResource(instance.currMap.GetTrees(), position);
    }

    public static Resource getClosestFarmingField(Vector3 position)
    {
        return getClosestResource(instance.currMap.GetFarmfields(), position);
    }
}
