using UnityEngine;

public class TargetScript : MonoBehaviour, IGvrGazeResponder
{
    private bool            selected;
    private float           currentTime = 0;
    private MeshRenderer    meshRenderer;
    private SphereCollider  sphereCollider;

    [SerializeField]
    private ParticleSystem  ParticleSystem;

    private float           STARE_TIME = 0.3f;

    public bool             Dead { get; private set; }

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
                Dead = true;
            }
        }
	}

    public void OnGazeEnter()
    {
        selected = true;
    }

    public void OnGazeExit()
    {
        selected = false;
        currentTime = 0;
    }

    public void OnGazeTrigger()
    {}
}
