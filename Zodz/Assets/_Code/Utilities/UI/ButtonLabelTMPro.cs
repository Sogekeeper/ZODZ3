using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonLabelTMPro : MonoBehaviour
{
    public Color activeColor;
    public Color disabledColor;
    public Button targetButton;
    public TextMeshProUGUI targetText;

    private bool active;

    private void Update() {
      if(targetButton.interactable != active){
        active = targetButton.interactable;
        targetText.color = active ? activeColor : disabledColor;
      }
    }
}
