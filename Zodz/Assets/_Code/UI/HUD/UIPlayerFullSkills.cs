using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerFullSkills : MonoBehaviour
{
    public PlayerStats player;
    public RectTransform selectedIndicator;
    public UISkillIcon[] magicSkills;
    public UISkillIcon ultimateSkill;
    public Text ultCostText;

    private void Start() {
        
        UpdateSkillIcons();
        UpdateSelectedSkill();
        selectedIndicator.gameObject.SetActive(false);
        selectedIndicator.anchoredPosition = new Vector2(-9999, -9999);
        selectedIndicator.ForceUpdateRectTransforms();
        Invoke("UpdateSelectedSkill",0.1f);
    }

    private void Update() {
        UpdateSkillCooldowns();
    }

    public void UpdateSkillIcons(){
        if(player.solarRace == null || player.astralMapFiltered == null) return;
        
        ultimateSkill.InitializeSkillIcon(player.solarRace.ultimateSkill);
        ultCostText.text = "Cost: "+ultimateSkill.currentSkill.skillCost.ToString();
        for(int i = 0; i < player.astralMapFiltered.Count; i++){
            magicSkills[i].InitializeSkillIcon(player.astralMapFiltered[i].magicSkill);
        }
    }
    public void UpdateSelectedSkill(){
        selectedIndicator.gameObject.SetActive(true);
        for(int i = 0; i < magicSkills.Length; i++){
            if(magicSkills[i].currentSkill && magicSkills[i].currentSkill == player.selectedMagicSkill){
                magicSkills[i].ToggleSelection(true);
                selectedIndicator.transform.position = magicSkills[i].transform.position;
                selectedIndicator.anchoredPosition = new Vector2(selectedIndicator.anchoredPosition.x+45, selectedIndicator.anchoredPosition.y);
            }else{
                magicSkills[i].ToggleSelection(false);
            }
        }
    }
    public void UpdateSkillCooldowns(){
        ultimateSkill.UpdateCooldown(
            player.GetSkillCooldown(ultimateSkill.currentSkill),
            ultimateSkill.currentSkill.cooldown
        );
        for(int i = 0; i < magicSkills.Length; i++){
            if(magicSkills[i].currentSkill){
                magicSkills[i].UpdateCooldown(
                    player.GetSkillCooldown(magicSkills[i].currentSkill),
                    magicSkills[i].currentSkill.cooldown
                    );
            }
        }
    }

}
