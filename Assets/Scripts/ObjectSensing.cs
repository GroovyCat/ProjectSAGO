using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSensing : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"{other.gameObject}");
            GameObject sesingObject = other.gameObject;
            {
                UIManager.instance.ShowCanvasText(sesingObject.tag);
            }
    }
}
