using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundToggle : MonoBehaviour
{
    public Toggle toggle;               // Toggle ������Ʈ
    public Image iconImage;            // ������ �̹���
    public Sprite soundOnSprite;       // ���� On ������
    public Sprite soundOffSprite;      // ���� Off ������

    void Start()
    {
        // ó�� ���� �ݿ�
        UpdateIcon(toggle.isOn);

        // ���� ���� �� �����ܸ� ��ü
        toggle.onValueChanged.AddListener(UpdateIcon);
    }

    void UpdateIcon(bool isOn)
    {
        iconImage.sprite = isOn ? soundOnSprite : soundOffSprite;
        // ���� ����� ���� �� ��
    }
}