﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    public bool canInput = true;
    public void CanInput(bool inputsAllowed){canInput = inputsAllowed;} //pra unity eventos
    
    public SkillUser skillUser;
    public PlayerStats playerStats;

    private bool pressing = false;

    private void Update() {
        if(pressing){
            ExecuteBasicSkill();
        }
    }

    public void SetSkillCooldown(Skill usedSkill){//isso para pra um UnityEvent em SKillUser, mas o msm metodo em PlayerStats já faz isso
        //playerStats.SetSkillCooldown(usedSkill, usedSkill.cooldown);
    }

    public void UseBasicSkill(InputAction.CallbackContext context){
        if(context.performed){
            pressing = true;
        }else if(context.canceled){
            pressing = false;
        }
    }
    private void ExecuteBasicSkill(){
        if(!skillUser.usingSkill && canInput){
            if(skillUser.userStats.currentMana >= playerStats.solarRace.basicSkill.skillCost)
                skillUser.InitializeSkill(playerStats.solarRace.basicSkill);
        }
    }
    public void UseMagicSkill(InputAction.CallbackContext context){
        if(context.performed && !skillUser.usingSkill && canInput){
            if(playerStats.GetSkillCooldown(playerStats.selectedMagicSkill) <= 0
                && skillUser.userStats.currentMana >= playerStats.selectedMagicSkill.skillCost){
               skillUser.InitializeSkill(playerStats.selectedMagicSkill);
            }
        }
    }
    public void UseUltimateSkill(InputAction.CallbackContext context){
        if(context.performed && !skillUser.usingSkill && canInput){
            if(playerStats.GetSkillCooldown(playerStats.solarRace.ultimateSkill) <= 0
                && skillUser.userStats.currentMana >= playerStats.solarRace.ultimateSkill.skillCost){
               skillUser.InitializeSkill(playerStats.solarRace.ultimateSkill);
            }
        }
    }

    public void SwapMagicSkillForwards(InputAction.CallbackContext context){
        if(context.performed && canInput){
            playerStats.SelectNextSkill(true);
        }
    }
    public void SwapMagicSkillBackwards(InputAction.CallbackContext context){
        if(context.performed && canInput){
            playerStats.SelectNextSkill(false);
        }
    }
}