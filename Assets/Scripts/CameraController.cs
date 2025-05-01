using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform targetTr;
    public float damping = 0.1f;
    public float targetOffset = 2.0f;

    private Transform camTr;
    private Vector3 velocity = Vector3.zero;

    private void Start()
    {
        camTr = GetComponent<Transform>();
    }

    private void LateUpdate()
    {
        Vector3 pos = new Vector3(targetTr.transform.position.x, targetTr.transform.position.y, -1);

        // camTr.position = Vector3.Slerp(camTr.position, pos, Time.deltaTime * damping);

        camTr.position = Vector3.SmoothDamp(camTr.position, pos, ref velocity, damping);


        camTr.LookAt(targetTr.position);
    }
}
