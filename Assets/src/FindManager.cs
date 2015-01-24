using UnityEngine;
using System.Collections;

public class FindManager : MonoBehaviour {

    private static FindManager instance;

	void Start () {
        instance = this;
	}

    public static Vector3 getTotem()
    {
        return Vector3.zero;
    }
    public static Vector3 getClosestVillager()
    {
        return Vector3.zero;
    }

    public static Vector3 getClosestTree()
    {
        return Vector3.one*10;
    }
}
