using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    public Transform target;


    private void Start()
    {
        // target이 비어있다면 Main Camera로 자동 설정
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
