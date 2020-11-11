using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Hellmade.Sound;

[RequireComponent(typeof(SkillUser))]
public class PlayerStats : EntityStats
{
    public PlayerCharacterSettings characterSettings;
    public Race solarRace;
    public Race lunarRace;
    public Race ascendantRace;

    public class SkillCooldown{
        public Skill targetSkill;
        public float skillTimer;

        public SkillCooldown(Skill skill, float currentTimer){
            targetSkill = skill;
            skillTimer = currentTimer;
        }
    }

    public List<SkillCooldown> skillCooldowns;
    
    public List<Race> astralMapFiltered{get;private set;} //mapa sem raças repetidas, para seleção de skills
    public Skill selectedMagicSkill{get; private set;}
    public int selectedSkillIndex{get;private set;}

    public AugmentMenu augmentMenu;
    public AudioClip skillSwitchSound;

    public UnityEvent OnSkillSwaped;

    private SkillUser skillUser;

    protected override void Awake() {
        base.Awake();
        skillUser = GetComponent<SkillUser>();

        skillCooldowns = new List<SkillCooldown>();
        astralMapFiltered = new List<Race>();
        
        solarRace = characterSettings.solarRace;                            
        baseRace = solarRace;
        lunarRace = characterSettings.lunarRace;
        ascendantRace = characterSettings.ascendantRace;

        //check if race is the same as setup race (ONLY NECESSARY INSIDE UNITY EDITOR)
        //If not, remake astral map
        if(characterSettings.astralMapRaces[0] != solarRace){
            characterSettings.astralMapRaces = new Race[8];
            AddRaceToMap(solarRace);            
        }else{ //if it is, add any other magic abilities to the filtered map (this is to load abilities between maps)
            for(int i = 0; i < characterSettings.astralMapRaces.Length; i++){
                if(characterSettings.astralMapRaces[i] != null){
                    AddToFilteredMap(characterSettings.astralMapRaces[i]);
                    //check for upgrade
                }
            }
        }

        selectedMagicSkill = characterSettings.astralMapRaces[0].magicSkill;
        selectedSkillIndex = 0;

        ApplyAllPermanentUpgrades();

    }

    protected override void Start() {
        base.Start();
        if(characterSettings.lifeSet){
            currentLife = characterSettings.currentLife;
            if(currentLife > totalLife.Value)
                currentLife = (int)totalLife.Value;
        }
        if(solarRace){
            skillUser.ReplaceIdleAnimationSet(solarRace.idleAnimations);
            skillUser.ReplaceWalkAnimationSet(solarRace.runningAnimations);
            skillUser.ReplaceDamageAnimation(solarRace.damageAnimations);
        }
        characterSettings.SetLife(currentLife);
    }

    protected override void OnDisable() {
        base.OnDisable();
        if(currentLife > 0)
            characterSettings.SetLife(currentLife);
    }

    protected override void Update() {
        base.Update();
        if(skillCooldowns.Count > 0){
            for(int i = 0; i < skillCooldowns.Count; i++){
                if(skillCooldowns[i].skillTimer > 0){
                    skillCooldowns[i].skillTimer -= Time.deltaTime;
                }
            }
        }
        if(Input.GetKeyDown(KeyCode.O)){
            totalLife.AddModifier(new StatModifier(9000,StatModType.Flat));
            Heal(9000);
        }
        if(Input.GetKeyDown(KeyCode.Alpha5)){
            Debug.Log("Strength :"+strength.Value+" Mind :"+mind.Value);
        }
        if(Input.GetKeyDown(KeyCode.Alpha9)){
            strength.AddModifier(new StatModifier(800,StatModType.Flat));
            mind.AddModifier(new StatModifier(800,StatModType.Flat));
            Heal(9000);
        }
         
    }

    public float GetSkillCooldown(Skill skill){
        if(skillCooldowns.Count > 0){
            for(int i = 0; i < skillCooldowns.Count; i++){
                if(skillCooldowns[i].targetSkill == skill){
                    return skillCooldowns[i].skillTimer;
                }
            }
        }
        return 0;
    }
    public SkillCooldown GetSkillCooldownClass(Skill skill){//pra permitir o cache da SkillCd no player
        if(skillCooldowns.Count > 0){
            for(int i = 0; i < skillCooldowns.Count; i++){
                if(skillCooldowns[i].targetSkill == skill){
                    return skillCooldowns[i];
                }
            }
        }
        return null;
    }
    public void SetSkillCooldown(Skill skill){
        if(skillCooldowns.Count > 0){
            for(int i = 0; i < skillCooldowns.Count; i++){
                if(skillCooldowns[i].targetSkill == skill){
                    skillCooldowns[i].skillTimer = skill.cooldown;
                    return;
                }
            }
        }
        //só chega aqui se não tinha skill na lista
        skillCooldowns.Add(new SkillCooldown(skill, skill.cooldown));
    }

    public void SelectNextSkill(bool forward = true){
        int lastIndex = selectedSkillIndex;
        if(forward){
            selectedSkillIndex = (selectedSkillIndex + 1) % astralMapFiltered.Count;
        }else{
            selectedSkillIndex--;
            if(selectedSkillIndex < 0) selectedSkillIndex = astralMapFiltered.Count - 1;
        }
        if(selectedSkillIndex != lastIndex && skillSwitchSound){
            EazySoundManager.PlaySound(skillSwitchSound);
        }
        selectedMagicSkill = astralMapFiltered[selectedSkillIndex].magicSkill;
        OnSkillSwaped?.Invoke();
    }

    public bool AddRaceToMap(Race targetRace){//retorna true se deu certo/tinha lugar
        bool added = false;
        int addedIndex = 0; //to set the augment later if duplicate pickup
        for(int i = 0; i < characterSettings.astralMapRaces.Length; i++){
            if(characterSettings.astralMapRaces[i] == null){
                added = true;
                characterSettings.astralMapRaces[i] = targetRace;
                addedIndex = i;
                break;
            }
        }
        if(!added) return false;

        if(!AddToFilteredMap(targetRace)){
            //openUpgradeInterface
            augmentMenu.SetupMenu(targetRace,addedIndex,this);
        }
        
        OnSkillSwaped?.Invoke();
        return true;
    }

    private bool AddToFilteredMap(Race targetRace){
        bool alreadyFiltered = false;
        //check if sign is already here
        for(int i = 0; i < astralMapFiltered.Count; i++){
            if(astralMapFiltered[i] == targetRace){
                alreadyFiltered = true;
            }
        }
        //if not, add it
        if(!alreadyFiltered){
            astralMapFiltered.Add(targetRace);
            return true;
        }
        return false;
    }

    public void UpdatePermanentUpgrades(){
        for(int i = 0; i < characterSettings.possiblePermanentUpgrades.Length;i++){
            if(characterSettings.possiblePermanentUpgrades[i].dirty){
                characterSettings.possiblePermanentUpgrades[i].dirty = false;
                RemoveState(characterSettings.possiblePermanentUpgrades[i]);
                if(characterSettings.possiblePermanentUpgrades[i].amount <= 0) 
                    continue;
                ApplyState(characterSettings.possiblePermanentUpgrades[i]);
            }
        }
        characterSettings.SetLife(currentLife);
    }
    private void ApplyAllPermanentUpgrades(){
        for(int i = 0; i < characterSettings.possiblePermanentUpgrades.Length;i++){
            if(characterSettings.possiblePermanentUpgrades[i].amount > 0){
                ApplyState(characterSettings.possiblePermanentUpgrades[i]);
            }
        }
    }
}
