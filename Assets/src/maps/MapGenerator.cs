using UnityEngine;
using System.Collections;

public class MapGenerator : MonoBehaviour {

    public GameObject totemPalette;
    public GameObject warehousePalette;
    public GameObject treePalette;
    public GameObject farmfieldPalette;
    public GameObject poolPalette;

    static MapGenerator instance;

    // Use this for initialization
    void Start()
    {
        instance = this;
    }

    public static Map Generate()
    {
        return instance.GenerateMap();
    }

    public Map GenerateMap()
    {
        Map map = new Map();

        /// ponemos el tótem en el centro
        map.SetTotem((GameObject)GameObject.Instantiate(totemPalette, new Vector3(0, 1, 0), Quaternion.LookRotation(-Vector2.up)));

        /// Añadimos 2 arboles
        map.AddTree((GameObject)GameObject.Instantiate(treePalette, new Vector3(5, 1, 5), Quaternion.LookRotation(-Vector2.up)));
        map.AddTree((GameObject)GameObject.Instantiate(treePalette, new Vector3(-4, 1, -4), Quaternion.LookRotation(-Vector2.up)));

        /// añadimos dos campos
        map.AddFarmField((GameObject)GameObject.Instantiate(farmfieldPalette, new Vector3(-5, 1, 5), Quaternion.LookRotation(-Vector2.up)));
        map.AddFarmField((GameObject)GameObject.Instantiate(farmfieldPalette, new Vector3(4, 1, -4), Quaternion.LookRotation(-Vector2.up)));

        /// añadimos almacen
        map.SetWarehouse((GameObject)GameObject.Instantiate(warehousePalette, new Vector3(10, 1, 0), Quaternion.LookRotation(-Vector2.up)));


        return map;
    }
}
