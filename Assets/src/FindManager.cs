﻿using UnityEngine;
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

    public static Vector3 getTotemPosition()
    {
        return instance.currMap.GetTotemPosition();
    }

    public static Vector3 getWarehousePosition()
    {
        return instance.currMap.GetWarehousePosition();
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

    public static Vector3 getClosestVillager()
    {
        return Vector3.zero;
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
