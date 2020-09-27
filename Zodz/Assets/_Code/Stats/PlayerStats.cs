using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SkillUser))]
public class PlayerStats : EntityStats
{
    public PlayerCharacterSettings characterSettings;
    public Race solarRace;
    public Race lunarRace;
    public Race ascendantRace;

    [System.Serializable]
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

        if(characterSettings.astralMapRaces[0] != solarRace){
            characterSettings.astralMapRaces = new Race[8];
            AddRaceToMap(solarRace);            
        }else{
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
        if(solarRace){
            skillUser.ReplaceIdleAnimationSet(solarRace.idleAnimations);
            skillUser.ReplaceWalkAnimationSet(solarRace.runningAnimations);
            skillUser.ReplaceDamageAnimation(solarRace.damageAnimations);
        }
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
        if(forward){
            selectedSkillIndex = (selectedSkillIndex + 1) % astralMapFiltered.Count;
        }else{
            selectedSkillIndex--;
            if(selectedSkillIndex < 0) selectedSkillIndex = astralMapFiltered.Count - 1;
        }
        selectedMagicSkill = astralMapFiltered[selectedSkillIndex].magicSkill;
        OnSkillSwaped?.Invoke();
    }

    public bool AddRaceToMap(Race targetRace){//retorna true se deu certo/tinha lugar
        bool added = false;
        for(int i = 0; i < characterSettings.astralMapRaces.Length; i++){
            if(characterSettings.astralMapRaces[i] == null){
                added = true;
                characterSettings.astralMapRaces[i] = targetRace;
                break;
            }
        }
        if(!added) return false;

        if(!AddToFilteredMap(targetRace)){
            //upgrade here
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
    }
    private void ApplyAllPermanentUpgrades(){
        for(int i = 0; i < characterSettings.possiblePermanentUpgrades.Length;i++){
            if(characterSettings.possiblePermanentUpgrades[i].amount > 0){
                ApplyState(characterSettings.possiblePermanentUpgrades[i]);
            }
        }
    }
}
