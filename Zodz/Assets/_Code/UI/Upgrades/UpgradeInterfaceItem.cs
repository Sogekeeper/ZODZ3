using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UpgradeInterfaceItem : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    public Upgrade targetUpgrade;
    public UpgradeInterface upgradeInterface;
    public Image fillbar;
    public Text amountText;

    private void Start() {
        UpdateThisItem();
    }

  public void OnPointerClick(PointerEventData eventData)
  {
    if(eventData.button == PointerEventData.InputButton.Left){
        upgradeInterface.AllocateUpgradePoint(targetUpgrade);
    }else if(eventData.button == PointerEventData.InputButton.Right){
        upgradeInterface.RemoveUpgradePoint(targetUpgrade);
    }
  }

  public void OnPointerEnter(PointerEventData eventData)
    {
        //play sound
        upgradeInterface.OpenToolTip(targetUpgrade,transform.position);
    }

  public void OnPointerExit(PointerEventData eventData)
  {
      //play sound
      upgradeInterface.CloseToolTip();
  }

  public void UpdateThisItem(){ //game event
      fillbar.fillAmount = (float)targetUpgrade.amount/(float)targetUpgrade.maxAmount;
      amountText.text = targetUpgrade.amount+"/"+targetUpgrade.maxAmount;
  }
}
