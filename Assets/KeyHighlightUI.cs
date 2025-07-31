using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyHighlightUI : MonoBehaviour
{
    [System.Serializable]
    public class KeyImage
    {
        public string keyName;
        public KeyCode keyCode;
        public Image keyImage;
        public Color defaultColor = new Color(1f, 1f, 1f, 0.3f);
        public Color highlightColor = Color.white;
    }

    public KeyImage[] keyImages;

    void Start()
    {
        foreach (var key in keyImages)
        {
            if (key.keyImage != null)
                key.keyImage.color = key.defaultColor;
        }
    }

    void Update()
    {
        foreach (var key in keyImages)
        {
            if (Input.GetKey(key.keyCode))
            {
                key.keyImage.color = key.highlightColor;
            }
            else
            {
                key.keyImage.color = key.defaultColor;
            }
        }
    }
}