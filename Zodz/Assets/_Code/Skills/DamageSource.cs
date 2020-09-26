using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using MilkShake;
using Hellmade.Sound;

public class DamageSource : MonoBehaviour
{
    public List<EntityRuntimeSet> hostileTo;
    public int damageValue = 10;
    public float knockbackForce = 0;
    public State[] statesToApply;
    public EntityStats owner = null;
    [Header("Mana Replenish Settings")]
    public int manaReplenishAmount = 10;
    [Header("Other Settings")]
    public SkillType skillType = SkillType.None; //setado por codigo ou por inspector
    public DamageType damageType = DamageType.None; //mesmo do de cima
    public UnityEvent OnHitEntity;
    public ShakePreset onHitShake;
    public AudioClip onHitSound;

    private void OnTriggerEnter2D(Collider2D other) {
        EntityStats entityStats = other.GetComponent<EntityStats>();
        if(entityStats && EntityRuntimeSet.DetectArrayOverlap(hostileTo,entityStats.myEntitySets)
            && entityStats != owner){
            OnHitEntity?.Invoke();
            if(onHitShake){
                //Debug.Log("Shaking");
                Shaker.ShakeAll(onHitShake);
            }
            if(onHitSound){
                EazySoundManager.PlaySound(onHitSound,0.13f);
            }
            ApplyDamage(entityStats);
            if(owner != null){
                owner.ChangeMana(manaReplenishAmount);
            }
        }
    }

    public void ApplyDamage(EntityStats target){
        target.TakeDamage(this);
        target.ApplyKnockback(transform.position,knockbackForce);
        ApplyStatesToEntity(target);
        if(owner && owner.states != null){
            for(int i = 0; i < owner.states.Count; i++){
                owner.states[i].currentState.OnDealDamage(owner,target,this);
            }
        }
    }
    

    public void ApplyStatesToEntity(EntityStats target){        
        if(statesToApply != null && statesToApply.Length > 0){
            for(int i = 0; i < statesToApply.Length;i++){
                target.ApplyState(statesToApply[i],owner);                
            }
        }
    }
}
