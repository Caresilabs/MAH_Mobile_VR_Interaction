using UnityEngine;
using System.Collections;

public class RayCastMovement : MonoBehaviour {

    [SerializeField]
    GameObject pointer;

    private float MAX_DIST = 11f;//6f;
    private float STARE_TIME = 0.75f;//0.5f;
    private float MAX_GROW_SIZE = 5f;
    private float MAX_MOVE_DIST = 0.1f;//0.05f;

    private float currentTime = 0;
    private Vector3 lastPos;


    public void OnPointerHover(GameObject targetObject, Vector3 intersectionPosition, Ray intersectionRay, bool isInteractive)
    {
        if(lastPos == null)
        {
            lastPos = intersectionPosition;
            return;
        } 
        print(intersectionPosition);
        pointer.transform.position = intersectionPosition;
        Renderer render = pointer.GetComponent<Renderer>();
        if(targetObject.tag == "RaycastFloor")
        {
            render.enabled = true;
            if(Vector3.Distance(intersectionPosition, transform.position) <= MAX_DIST)
            {
                if(Vector3.Distance(intersectionPosition, lastPos) <= MAX_MOVE_DIST)
                {
                    currentTime += Time.deltaTime;
                    render.material.SetColor("_Color", Color.green);
                    if(currentTime >= STARE_TIME)
                    {
                        GetComponent<Rigidbody>().MovePosition(new Vector3(pointer.transform.position.x, 
                            GetComponent<Rigidbody>().transform.position.y, 
                            pointer.transform.position.z));
                    }
                }
                else
                {
                    currentTime = 0;
                    render.material.SetColor("_Color", Color.yellow);
                }
                float size = MAX_GROW_SIZE * (currentTime / STARE_TIME);
                pointer.transform.GetChild(0).transform.localScale = new Vector3(size, 0.1f, size);         
            }
            else
            {
                render.material.SetColor("_Color", Color.red);
            }
        }
        else
        {
            render.enabled = false;
        }
        lastPos = intersectionPosition;
    }

    // Update is called once per frame
    void Update () {
	
	}
}
