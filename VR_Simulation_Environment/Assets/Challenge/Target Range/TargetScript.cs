using UnityEngine;
using System.Collections;
using System;

public class TargetScript : MonoBehaviour, IGvrGazeResponder
{
    private bool            selected;
    private float           currentTime = 0;
    private MeshRenderer    meshRenderer;
    private SphereCollider  sphereCollider;

    [SerializeField]
    private ParticleSystem  ParticleSystem;

    private float           STARE_TIME = 0.3f;

    public bool             dead { get; private set; }

    void Start () {
        meshRenderer = GetComponent<MeshRenderer>();
        sphereCollider = GetComponent<SphereCollider>();
    }
	
	void Update () {
        if (selected)
        {
            currentTime += Time.deltaTime;
            if(currentTime >= STARE_TIME)
            {
                ParticleSystem.transform.position = transform.position;
                ParticleSystem.Play();
                meshRenderer.enabled = false;
                sphereCollider.enabled = false;
                dead = true;
            }
        }
	}

    public void OnGazeEnter()
    {
        selected = true;
        Console.WriteLine("START()");
    }

    public void OnGazeExit()
    {
        selected = false;
        currentTime = 0;
    }

    public void OnGazeTrigger()
    {
        Console.WriteLine("OnGazeTrigger()");
    }
}
