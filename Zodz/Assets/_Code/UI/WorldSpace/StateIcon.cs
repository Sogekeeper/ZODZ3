using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateIcon : MonoBehaviour
{
    public Image iconImage;
    
    public void SetUpIcon(Sprite targetSprite){
        iconImage.sprite = targetSprite;
    }
}
