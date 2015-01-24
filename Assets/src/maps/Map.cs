using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Map {

    List<GameObject> trees=new List<GameObject>();
    List<GameObject> farmfields = new List<GameObject>();
    GameObject totem;
    GameObject warehouse;
    List<GameObject> pools = new List<GameObject>();

    List<GameObject> villagers = new List<GameObject>();

    public void AddTree(GameObject tree)
    {
        trees.Add(tree);
    }

    public List<GameObject> GetTrees()
    {
        return trees;
    }

    public void AddFarmField(GameObject field)
    {
        farmfields.Add(field);
    }

    public List<GameObject> GetFarmfields()
    {
        return farmfields;
    }

    public void AddPool(GameObject pool)
    {
        pools.Add(pool);
    }

    public List<GameObject> GetPools()
    {
        return pools;
    }

    public void SetTotem(GameObject totem)
    {
        this.totem = totem;
    }
    public Vector3 GetTotemPosition()
    {
        return totem.transform.position;
    }

    public void SetWarehouse(GameObject warehouse)
    {
        this.warehouse = warehouse;
    }
    public Vector3 GetWarehousePosition()
    {
        return warehouse.transform.position;
    }

    public void AddVillager(GameObject vill)
    {
        villagers.Add(vill);
    }

    internal List<GameObject> getVillagers()
    {
        return villagers;
    }
}
