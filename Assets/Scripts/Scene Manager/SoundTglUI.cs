using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundToggle : MonoBehaviour
{
    public Toggle toggle;               // Toggle 컴포넌트
    public Image iconImage;            // 아이콘 이미지
    public Sprite soundOnSprite;       // 사운드 On 아이콘
    public Sprite soundOffSprite;      // 사운드 Off 아이콘

    void Start()
    {
        // 처음 상태 반영
        UpdateIcon(toggle.isOn);

        // 상태 변경 시 아이콘만 교체
        toggle.onValueChanged.AddListener(UpdateIcon);
    }

    void UpdateIcon(bool isOn)
    {
        iconImage.sprite = isOn ? soundOnSprite : soundOffSprite;
        // 사운드 제어는 전혀 안 함
    }
}