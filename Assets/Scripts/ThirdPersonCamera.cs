using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ThirdPersonCamera : MonoBehaviour {

    public Vector3 offset;
    private Transform target;
    [Range (0,1)] public float lerpValue = 1;
    public float sensiblidad = 3;

    // Start is called before the first frame update
    void Start(){
        target = GameObject.Find("Player").transform;
        offset.y = 3;
        offset.z = 9;
        offset.x = 5;
    }

    // Update is called once per frame
    void LateUpdate(){
        transform.position = Vector3.Lerp(transform.position, target.position + offset, lerpValue);
        offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * sensiblidad, Vector3.up) * offset;
        transform.LookAt(target);
    }
}
