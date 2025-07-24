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
        tmpMaterial.SetFloat(ShaderUtilities.ID_GlowPower, 0f); // �ʱ� ����: ��¦�� ����
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tmpMaterial.SetFloat(ShaderUtilities.ID_GlowPower, 1f); // ���콺 ���� �� ��¦�� Ȱ��ȭ
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tmpMaterial.SetFloat(ShaderUtilities.ID_GlowPower, 0f); // �ٽ� ��¦�� ��
    }
}