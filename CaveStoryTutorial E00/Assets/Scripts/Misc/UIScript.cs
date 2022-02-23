using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    public static UIScript instance { get; private set; }

    public Image mask;
    Player player;
    float originalSize;

    void Awake()
    {
        player = Player.instance;
        instance = this;
    }

    void Start()
    {
       originalSize = mask.rectTransform.rect.width;
        
    }

    public void SetValue(float value)
    {
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * value);
    }
}