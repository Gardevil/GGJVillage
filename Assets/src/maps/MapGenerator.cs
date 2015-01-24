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
        map.SetTotem((GameObject)GameObject.Instantiate(totemPalette, Vector3.zero, Quaternion.identity));

        /// Añadimos 2 arboles
        map.AddTree((GameObject)GameObject.Instantiate(treePalette, new Vector3(5, 5, 0), Quaternion.identity));
        map.AddTree((GameObject)GameObject.Instantiate(treePalette, new Vector3(-4, -4, 0), Quaternion.identity));

        /// añadimos dos campos
        map.AddFarmField((GameObject)GameObject.Instantiate(farmfieldPalette, new Vector3(-5, 5, 0), Quaternion.identity));
        map.AddFarmField((GameObject)GameObject.Instantiate(farmfieldPalette, new Vector3(4, -4, 0), Quaternion.identity));

        /// añadimos almacen
        map.SetWarehouse((GameObject)GameObject.Instantiate(warehousePalette, new Vector3(10, 0, 0), Quaternion.identity));


        return map;
    }
}
