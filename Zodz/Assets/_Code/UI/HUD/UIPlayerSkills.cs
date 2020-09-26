using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerSkills : MonoBehaviour
{
    public PlayerStats targetPlayer;
    public Sprite emptySkillIcon; //usado só quando jogador começa com 1 mágica
    [Space()]
    public Image basicSkillIcon;
    public Image ultimateSkillIcon;
    public Image currentMagicSkillIcon;
    public Image nextMagicSkillIcon;
    public Image previousMagicSkillIcon;
    [Space()]
    public Text magicSkillCostText;
    public Text ultimateSkillCostText;

    [Header("Cooldown Related")]
    public Text ultimateCooldownText;
    public Image ultimateCooldownCover;
    public Text magicSkillCooldownText;
    public Image magicSkillCooldownCover;

    //private PlayerStats.SkillCooldown magicCooldown;
    //private PlayerStats.SkillCooldown ultimateCooldown;

    private void Start() {
        basicSkillIcon.sprite = targetPlayer.solarRace.basicSkill.skillIcon;
        ultimateSkillIcon.sprite = targetPlayer.solarRace.ultimateSkill.skillIcon;
        
        UpdateMagicSkills();
    }

    private void Update() {
        UpdateCooldownVisuals();
    }

    public void UpdateMagicSkills(){ //pra ser chamado pelo evento de PlayerStats
        currentMagicSkillIcon.sprite = targetPlayer.selectedMagicSkill.skillIcon;
        nextMagicSkillIcon.sprite = GetNextMagicSkill().skillIcon;
        previousMagicSkillIcon.sprite = GetNextMagicSkill(false).skillIcon;
        if(currentMagicSkillIcon.sprite == nextMagicSkillIcon.sprite){
            nextMagicSkillIcon.sprite = emptySkillIcon;
        }
        if(currentMagicSkillIcon.sprite == previousMagicSkillIcon.sprite)
            previousMagicSkillIcon.sprite = emptySkillIcon;
        
        //magicSkillCostText.text = "M: "+targetPlayer.selectedMagicSkill.skillCost.ToString();
        //ultimateSkillCostText.text = "M: "+targetPlayer.solarRace.ultimateSkill.skillCost.ToString();
        //magicCooldown = targetPlayer.GetSkillCooldownClass(targetPlayer.selectedMagicSkill);
        //ultimateCooldown = targetPlayer.GetSkillCooldownClass(targetPlayer.solarRace.ultimateSkill);
    }

    
    public void UpdateCooldownVisuals(){
        float magicCD = targetPlayer.GetSkillCooldown(targetPlayer.selectedMagicSkill);
        float ultCD = targetPlayer.GetSkillCooldown(targetPlayer.solarRace.ultimateSkill);

        magicSkillCooldownCover.fillAmount = 
            magicCD/targetPlayer.selectedMagicSkill.cooldown;
        if(magicCD <= 0){
            magicSkillCooldownText.gameObject.SetActive(false);
        }else{
            magicSkillCooldownText.gameObject.SetActive(true);
            magicSkillCooldownText.text = Mathf.Ceil(magicCD).ToString();
        }
        
        
        ultimateCooldownCover.fillAmount =
            ultCD/targetPlayer.solarRace.ultimateSkill.cooldown;
        if(ultCD <= 0){
            ultimateCooldownText.gameObject.SetActive(false);
        }else{
            ultimateCooldownText.gameObject.SetActive(true);
            ultimateCooldownText.text = Mathf.Ceil(ultCD).ToString();
        }
    }

    /*
    public void UpdateCooldownVisuals(){
        if(magicCooldown != null){
            magicSkillCooldownCover.fillAmount = 
                magicCooldown.skillTimer/targetPlayer.selectedMagicSkill.cooldown;
            if(magicCooldown.skillTimer <= 0){
                magicSkillCostText.gameObject.SetActive(false);
            }else{
                magicSkillCostText.gameObject.SetActive(true);
                magicSkillCooldownText.text = Mathf.Ceil(magicCooldown.skillTimer).ToString();
            }
        }
        if(ultimateCooldown != null){
            ultimateCooldownCover.fillAmount =
                ultimateCooldown.skillTimer/targetPlayer.solarRace.ultimateSkill.cooldown;
            if(ultimateCooldown.skillTimer <= 0){
                ultimateCooldownText.gameObject.SetActive(false);
            }else{
                ultimateCooldownText.gameObject.SetActive(true);
                ultimateCooldownText.text = Mathf.Ceil(ultimateCooldown.skillTimer).ToString();
            }
            
        }
    }*/

    public Skill GetNextMagicSkill(bool forward = true){
        int index = targetPlayer.selectedSkillIndex;
        if(forward){
            index = (index + 1) % targetPlayer.astralMapFiltered.Count;
        }else{
            index--;
            if(index < 0) index = targetPlayer.astralMapFiltered.Count - 1;
        }
        return targetPlayer.astralMapFiltered[index].magicSkill;
    }
}
