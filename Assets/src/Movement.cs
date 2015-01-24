using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {


	private NavMeshAgent navAgent ;



	public bool standing{
		get{
			return mStanding;
		}
	}


	private bool mStanding;
	// Use this for initialization
	void Start () {
		mStading = true;
		navAgent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
		if (navAgent.velocity == 0) {
			mStanding = true;		
		} else {
			mStanding = false;		
		}
	}


	public void moveTo(Vector3 target){
		navAgent.SetDestination (target);
	}


}
