using UnityEngine;
using System.Collections;

public class Villager : MonoBehaviour {

    bool onGodDuty;/// Indica si está haciendo algo porque Dios lo ha dicho o por cuenta propia

	// Use this for initialization
	void Start () {
        onGodDuty = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    internal void SetBusy(bool onGodDuty)
    {
        this.onGodDuty = onGodDuty;
    }
}
