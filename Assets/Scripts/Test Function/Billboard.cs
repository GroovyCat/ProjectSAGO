using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    public Transform target;


    private void Start()
    {
        // target�� ����ִٸ� Main Camera�� �ڵ� ����
        if (target == null && Camera.main != null)
        {
            target = Camera.main.transform;
        }
    }
    private void Update()
    {
        transform.forward = target.forward;
    }
}
