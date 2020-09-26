using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISkillIcon : MonoBehaviour
{
    //outras frescuras no script da interface
    public Image skillFrame;
    public Image skillIcon;
    public Sprite blankIcon;
    
    [Header("Selection Settings")]
    public Sprite selectedFrame;
    public Sprite notSelectedFrame;

    [Header("Cooldown Settings")]
    public Text cooldownText;
    public Image cooldownFill;

    //private set
    public Skill currentSkill{get; private set;}

    private void Awake() {
        skillIcon.sprite = blankIcon;
    }

    public void InitializeSkillIcon(Skill targetSkill){
        skillIcon.sprite = targetSkill.skillIcon;
        currentSkill = targetSkill;
    }        

    public void ToggleSelection(bool selected){
        if(selected){
            skillFrame.sprite = selectedFrame;
        }else{
            skillFrame.sprite = notSelectedFrame;
        }
    }

    public void UpdateCooldown(float currentTime, float totalTime){
        if(cooldownFill)
            cooldownFill.fillAmount = currentTime/totalTime;

        if(cooldownText !=null){
            if(currentTime <= 0){
                cooldownText.gameObject.SetActive(false);
            }else{
                cooldownText.gameObject.SetActive(true);
                cooldownText.text = Mathf.Ceil(currentTime).ToString();
            }
        }            
    }
}

