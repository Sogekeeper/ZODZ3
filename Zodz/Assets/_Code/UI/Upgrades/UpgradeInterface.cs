using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeInterface : MonoBehaviour
{
    public PlayerCharacterSettings playerCharacterSettings;
    public GameEvent upgradeUpdate;
    public TextMeshProUGUI crystalsText;
    [Header("ToolTip")]
    public float verticalOffset = -10;
    public TextMeshProUGUI tooltipText;
    public RectTransform tooltipBox;

    public void ToggleInterface(bool active){
      gameObject.SetActive(active);
      Time.timeScale = active ? 0 : 1;
      UpdateCrystalText();
      CloseToolTip(); //just to be sure against bugs
    }

    public void AllocateUpgradePoint(Upgrade targetUpgrade){
      if(playerCharacterSettings.crystals > 0 && targetUpgrade.amount < targetUpgrade.maxAmount){
        targetUpgrade.amount++;
        playerCharacterSettings.crystals--;
        targetUpgrade.dirty = true;
        UpdateCrystalText();
        OpenToolTip(targetUpgrade,tooltipBox.position);
        upgradeUpdate.Raise();
      }
    }
    public void RemoveUpgradePoint(Upgrade targetUpgrade){
      if(targetUpgrade.amount > 0){
        targetUpgrade.amount--;
        playerCharacterSettings.crystals++;
        targetUpgrade.dirty = true;
        UpdateCrystalText();
        OpenToolTip(targetUpgrade,tooltipBox.position);
        upgradeUpdate.Raise();
      }else{
        //mostrar erro
      }
    }

    public void OpenToolTip(Upgrade targetUpgrade,Vector3 textPos){
      targetUpgrade.SetDescriptionText(tooltipText);
      tooltipBox.sizeDelta = tooltipText.GetPreferredValues();
      tooltipBox.gameObject.SetActive(true);
      tooltipBox.transform.position = new Vector2(textPos.x,textPos.y);
      tooltipBox.ForceUpdateRectTransforms();
      tooltipBox.anchoredPosition = new Vector2(tooltipBox.anchoredPosition.x,tooltipBox.anchoredPosition.y+verticalOffset);
      tooltipBox.sizeDelta = tooltipText.GetPreferredValues();
    }

    public void CloseToolTip(){
      tooltipBox.gameObject.SetActive(false);
    }

    private void UpdateCrystalText(){
      crystalsText.text = "Crystals: "+playerCharacterSettings.crystals;
    }


}
