using UnityEngine;
using System.Collections;

public class Villager : MonoBehaviour {

    bool onGodDuty;/// Indica si está haciendo algo porque Dios lo ha dicho o por cuenta propia

	Movement movement;


	public int lifes{
		get{
			return lifes;
		}

	}

	public int food{
		get{
			return food;
		}
	}
	public int stamina{
		get{
			return stamina;
		}
	}




	private int mLifes;
	private int mFood;
	private int mStamina;

	public bool standing{
		get{
			return movement.standing();
		}
	}

	// Use this for initialization
	void Start () {
        onGodDuty = false;
		movement = GetComponent<Movement> ();
		lifes = 5;
		food = 5;
		stamina = 5;
	}
	
	// Update is called once per frame
	void Update () {

	}

    internal void SetBusy(bool onGodDuty)
    {
        this.onGodDuty = onGodDuty;
    }

	public void moveTo(Vector3 target){
		movement.MoveTo(target);
	}

	public void moveToWareHouse(){
		movement.MoveTo (FindManager.getWareHouse ());
	}

	public void moveToTotem(){
		movement.MoveTo (FindManager.getTotem ());
	}




	
}
