using UnityEngine;
using System.Collections;
using Assets.Scripts;

using System.Collections.Generic;

public class TargetChallenge : MonoBehaviour, IChallenge {

    [SerializeField]
    private List<BoxCollider>   SpawnBoxColliders;

    [SerializeField]
    private GameObject          Target;

    private int                 currentChild;

    private GameObject          currentTarget;


    public void StartChallenge()
    {
        currentChild = 0;
        spawnTarget();
    }

    public void StopChallenge()
    {

    }

    private void spawnTarget()
    {
        if (currentChild == transform.childCount)
            currentChild = 0;

        BoxCollider bx = transform.GetChild(currentChild).GetComponent<BoxCollider>();
        Vector3 posSpawn = bx.transform.position + new Vector3(
            Random.Range(-bx.size.x / 2, bx.size.x / 2),
            Random.Range(-bx.size.y / 2, bx.size.y / 2),
            Random.Range(-bx.size.z / 2, bx.size.z / 2));

        currentTarget = (GameObject)Instantiate(Target, posSpawn, Quaternion.identity);
        currentChild++;
    }

    void Start () {
        StartChallenge();
    }
	
	void Update () {
	    if(currentTarget != null)
        {
            TargetScript ts = currentTarget.GetComponent<TargetScript>();
            if(ts.dead)
            {
                Destroy(currentTarget);
                spawnTarget();
            }
        }
	}
}
