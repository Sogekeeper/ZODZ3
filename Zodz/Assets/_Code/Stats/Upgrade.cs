using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class Upgrade : State
{
    public int maxAmount = 5;
    public int amount = 0;
    public bool dirty = false;
    [TextArea]public string upgradeDescription;

    public abstract void SetDescriptionText(TextMeshProUGUI text);
}
