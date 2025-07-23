using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceneTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // �÷��̾� ������Ʈ�� �÷��̾� �±� �߰�
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; // ���忡 �� �߰� ���ּ���
            SceneManager.LoadScene(currentSceneIndex + 1); //������ �ҷ�����
        }
    }
}
