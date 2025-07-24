using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class TMPTextGlowOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI textMesh;
    private Material tmpMaterial;

    private void Start()
    {
        tmpMaterial = textMesh.fontMaterial;
        tmpMaterial.SetFloat(ShaderUtilities.ID_GlowPower, 0f); // 초기 상태: 반짝임 없음
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tmpMaterial.SetFloat(ShaderUtilities.ID_GlowPower, 1f); // 마우스 오버 시 반짝임 활성화
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tmpMaterial.SetFloat(ShaderUtilities.ID_GlowPower, 0f); // 다시 반짝임 끔
    }
}