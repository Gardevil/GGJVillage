using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{


    private NavMeshAgent navAgent;

    private GameObject targetObject;

    public bool standing
    {
        get
        {
            return mStanding;
        }
    }


    private bool mStanding;
    // Use this for initialization
    void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        mStanding = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (mStanding)
        {
            return;
        }
        if (targetObject!=null)
        {
            Vector3 vaux = navAgent.destination - gameObject.transform.position;
            vaux.y = 0;
            float sqrDistance = vaux.sqrMagnitude;
            float myRadius = GetComponent<CapsuleCollider>().radius;
            float otherRadius = 0;
            if (targetObject.GetComponent<Villager>()!=null)
            {///es un villager
                otherRadius=targetObject.GetComponent<CapsuleCollider>().radius;
            }
            else
            {///es un obstaculo
                otherRadius=targetObject.GetComponent<NavMeshObstacle>().radius;
            }

            float minDistance = (myRadius + otherRadius) * (myRadius + otherRadius);
            if (minDistance >= sqrDistance)
            {
                mStanding = true;
            }
        }
    }

    public void moveTo(Vector3 target)
    {
        navAgent.SetDestination(target);
        targetObject = null;
        mStanding = false;
    }

    public void moveTo(GameObject targetObject)
    {
        navAgent.SetDestination(targetObject.transform.position);
        this.targetObject = targetObject;
        mStanding = false;
    }

    public void stop()
    {
        navAgent.Stop(true);
        targetObject = null;
        mStanding = false;
    }
}
