using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float smooth = 3f;

    public bool followActive = true;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(transform.position.x, (target.position + offset).y, (target.position + offset).z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if(followActive)
         transform.position = new Vector3(transform.position.x, Vector3.Lerp(transform.position, target.position + offset, Time.deltaTime * smooth).y, transform.position.z);
    }
}
