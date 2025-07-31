using UnityEngine;

public class ObjectSensing : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interactive"))
        {
            // 충돌한 오브젝트의 자식 중 Canvas 찾기 (비활성 포함)
            Canvas canvas = other.GetComponentInChildren<Canvas>(true);

            if (canvas != null)
            {
                canvas.gameObject.SetActive(true);
            }
        }


    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Interactive"))
        {
            Canvas canvas = other.GetComponentInChildren<Canvas>(true);

            if (canvas != null)
            {
                canvas.gameObject.SetActive(false);
            }
        }
    }
}