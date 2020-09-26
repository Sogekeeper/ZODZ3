using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillUser : MonoBehaviour
{
    /*
        Animações de skills chamarão callbacks/métodos nessa classe
        essa classe chamará os métodos correspondentes no scriptableObj da Skill
    */

    public Animator userAnim; //para tocar animações que as skills precisarem
    public AnimatorOverrideController userAnimOverride; //para tocar animações que as skills precisarem
    public Aim userAim; //para mirar skill
    public PoolContainer userPool;//pra spawn de objetos/projeteis, o que a skill precisar
    public EntityStats userStats;
    [Header("Optional Components")]
    public Transform skillSpawnPoint;

    [Header("Flags")]
    public bool usingSkill = false;
    public bool canCastSkills = true;

    [Header("Events")]
    public UnityEventAlternatives.SkillEvent OnSkillUsed;

    //abaixo são variáveis que não aparecem no inspector
    
    [HideInInspector]
    public int skillStep = 0;

    public AnimationClipOverrides clipOverrides;
    public Skill currentSkill{get; private set;}
    public Skill lastUsedSkill{get; private set;} //para skills de combo ou multi-uso
    public float timeSinceLastSkill{get; private set;} //para skills de combo

    private bool canStep = true; //evitar Blend Tree animação chamar eventos várias vezes no mesmo frame

    [Header("Other Stuff")]
    public float startVerticalParam = -1;
    public float startHorizontalParam = 0;

    private void Awake() {
        clipOverrides = new AnimationClipOverrides(userAnimOverride.overridesCount);
        userAnimOverride.GetOverrides(clipOverrides);   
        userAnim.SetFloat("vertical",startVerticalParam);     
        userAnim.SetFloat("horizontal",startHorizontalParam); 
          
    }

    private void Start() {
        canStep = true;
        usingSkill = false;
        userStats.canMove = true;
    }

    private void Update() {
        if(usingSkill && currentSkill != null){
            currentSkill.FrameUpdate(this);
        }
        if(lastUsedSkill){
            timeSinceLastSkill += Time.deltaTime;
        }
    }

    public bool InitializeSkill(Skill targetSkill){ //retorne se o usuário conseguiu inicializar skill (falhar se faltar mana)
        //checar custo depois
       // if(skillSpawnPoint) skillSpawnPoint.localPosition = new Vector3(0,0,0);
        if(!canCastSkills || !userStats.canAct)return false;
        if(targetSkill.Initialize(this)){
            //PRA DEPOIS: verificar se skill é afetada por atk speed e alterar param do anim
            currentSkill = targetSkill;
            lastUsedSkill = targetSkill;
            timeSinceLastSkill = 0;
            skillStep = 0;
            usingSkill = true;
            return true;
        }
        else{
            return false;
        }
    }

    public void StepSkill(){ //multi uso, forma de dar suporte a qualquer efeito
        if(!canStep) return;
        skillStep++;
        canStep = false;
        StartCoroutine(StepFrameDelay());
        currentSkill?.StepSkill(this);
    }
    private IEnumerator StepFrameDelay(){
        yield return null;
        canStep = true;
    }

    public void ComputeSkill(Skill usedSkill){ //para setar cooldowns ou oq precisar
        OnSkillUsed?.Invoke(usedSkill);
    }

    public void InterruptSkill(){
        currentSkill?.InterruptSkill(this);
    }

    public void ReplaceSkillAnimationSet(AnimationSet targetSet){
        if(targetSet == null) return;
        clipOverrides["skill_up"] = targetSet.upClip;
        clipOverrides["skill_down"] = targetSet.downClip;
        clipOverrides["skill_left"] = targetSet.leftClip;
        clipOverrides["skill_right"] = targetSet.rightClip;
        userAnimOverride.ApplyOverrides(clipOverrides);
    }

    public void ReplaceWalkAnimationSet(AnimationSet targetSet){
        if(targetSet == null) return;
        clipOverrides["walk_up"] = targetSet.upClip;
        clipOverrides["walk_down"] = targetSet.downClip;
        clipOverrides["walk_left"] = targetSet.leftClip;
        clipOverrides["walk_right"] = targetSet.rightClip;
        userAnimOverride.ApplyOverrides(clipOverrides);
    }

    public void ReplaceIdleAnimationSet(AnimationSet targetSet){
        if(targetSet == null) return;
        clipOverrides["idle_up"] = targetSet.upClip;
        clipOverrides["idle_down"] = targetSet.downClip;
        clipOverrides["idle_left"] = targetSet.leftClip;
        clipOverrides["idle_right"] = targetSet.rightClip;
        userAnimOverride.ApplyOverrides(clipOverrides);
    }

    public void ReplaceDeathAnimation(AnimationClip targetClip){
        if(targetClip == null) return;
        clipOverrides["Dummy_Death"] = targetClip;        
        userAnimOverride.ApplyOverrides(clipOverrides);
    }

    public void ReplaceDamageAnimation(AnimationSet targetSet){
        if(targetSet == null) return;
        clipOverrides["Leo_damage"] = targetSet.rightClip;
        clipOverrides["Leo_damage_left"] = targetSet.leftClip;
        userAnimOverride.ApplyOverrides(clipOverrides);
    }
    
}
