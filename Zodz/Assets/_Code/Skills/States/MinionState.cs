using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionState : State
{
    //Inimigo afetado vai atacar outros inimigos
    public float duration = 7f;
    public bool canAttackMaster = false;//um efeito tipo "Frenzy" pra atacar todos em volta
    public bool masterCanDamageMinion = true; //para evitar danos vindo do jogador e aliados do jogador
    public bool canOtherEnemiesAttackMinion = true;//se por algum motivo os demais inimigos tenham que ignorar o afetado

    public override void InitState(EntityStats receiver, EntityStats applier = null)
    {
        if(applier == null) return; //no minion master
        StateStack ss = receiver.GetStack(this);
        if(ss == null){
            ss = new StateStack(this, duration,1,1,applier);
            receiver.states.Add(ss);
            
            //decide enemies
            if(canAttackMaster){
                EntityRuntimeSet.CopySetArray(applier.enemyEntitySets,ref receiver.enemyEntitySets,false);
            }else{
                EntityRuntimeSet.CopySetArray(applier.enemyEntitySets,ref receiver.enemyEntitySets);
            }
            //deciding possible damage
            if(canOtherEnemiesAttackMinion){
                EntityRuntimeSet.CopySetArray(applier.myEntitySets,ref receiver.myEntitySets,false);
            }
            if(!masterCanDamageMinion){
                EntityRuntimeSet.RemoveSetOverlap(applier.enemyEntitySets,ref receiver.myEntitySets);
            }
        }else{
            ss.ResetTimer();
        }
    }

    public override void ConcludeState(EntityStats receiver){
        EntityRuntimeSet.CopySetArray(receiver.originalEnemySets,ref receiver.enemyEntitySets);
        EntityRuntimeSet.CopySetArray(receiver.originalMySets,ref receiver.myEntitySets);
    }
}
