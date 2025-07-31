using UnityEngine;

public class ObjectSensing : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interactive"))
        {
            // �浹�� ������Ʈ�� �ڽ� �� Canvas ã�� (��Ȱ�� ����)
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