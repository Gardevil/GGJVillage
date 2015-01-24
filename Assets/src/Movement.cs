using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{


    private NavMeshAgent navAgent;



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
        if (navAgent.velocity == Vector3.zero)
        {
            mStanding = true;
        }
        else
        {
            mStanding = false;
        }
    }

    public void moveTo(Vector3 target)
    {
        navAgent.SetDestination(target);
    }
}
