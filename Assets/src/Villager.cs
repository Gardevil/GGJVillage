using UnityEngine;
using System.Collections;

public class Villager : MonoBehaviour {

    bool onGodDuty;/// Indica si está haciendo algo porque Dios lo ha dicho o por cuenta propia

	Movement movement;

	public int lifes{
		get{
            return mLifes;
		}
        set
        {
            mLifes = value;
        }
	}

	public int food{
		get{
            return mFood;
		}
	}
	public int stamina{
		get{
            return mStamina;
		}
	}
	private int mLifes;
	private int mFood;
	private int mStamina;

	public bool standing{
		get{
			return movement.standing;
		}
	}

    void Awake()
    {
        movement = GetComponent<Movement>();
    }

	// Use this for initialization
	void Start () {
        onGodDuty = false;
        mLifes = 5;
		mFood = 5;
		mStamina = 5;
	}
	
	// Update is called once per frame
	void Update () {
        if (mLifes <= 0)
        {
            Destroy(base.gameObject);
            Debug.Log("HE MUERTO!");
        }
	}

    internal void SetBusy(bool onGodDuty)
    {
        this.onGodDuty = onGodDuty;
    }

	public void moveTo(Vector3 target){
        movement.moveTo(target);
	}

	public void moveToWareHouse(){
        movement.moveTo(FindManager.getWarehousePosition());
	}

	public void moveToTotem(){
        movement.moveTo(FindManager.getTotemPosition());
	}

    internal bool isOnGodDuty()
    {
        return onGodDuty;
    }
}
