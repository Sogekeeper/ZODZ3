using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : ScriptableObject
{
    //DONT FORGET TO CREATE THE STACK ON INIT

    public string stateName;
    public Sprite stateIcon;

    [System.Serializable] //apenas pra debug
    public class StateStack{
        public int stackAmount = 0;
        public float stateDuration = 4;
        public float tickRate = 1;
        public State currentState;
        public float stateTimer = 0;
        public float tickTimer = 0;
        public float duration = 0;
        public EntityStats stateOrigin;
        
        public StateStack(State targetState, float _duration, float tickEverySeconds = 1, int initialAmount = 1, EntityStats origin = null){
            stackAmount = initialAmount;
            stateDuration = _duration;
            duration = _duration;
            stateTimer = duration;
            currentState = targetState;
            tickRate = tickEverySeconds;
            tickTimer = tickRate;            
            stateOrigin = origin;
        }
        
        //vc ainda está dentro do StateStack
        public void TickState(EntityStats owner, float elapsedTime){
            if(currentState == null){
                Debug.Log("Trying to tick empty state.");
                return;
            }
            if(tickRate > 0){
                tickTimer -= elapsedTime;
                if(tickTimer <= 0){
                    tickTimer = tickRate;
                    currentState.TickState(owner,this);
                }
            }
            stateTimer -= elapsedTime;
            if(duration > 0 && stateTimer <= 0){
                EndState(owner);
            }
        }

        public void ResetTimer(){
            stateTimer = stateDuration;
        }

        public void EndState(EntityStats owner){
            currentState.ConcludeState(owner);
            owner.states.Remove(this);
            owner.OnStatesUpdate?.Invoke(owner);
        }

    }

    public abstract void InitState(EntityStats receiver, EntityStats applier = null);

    public virtual void TickState(EntityStats receiver, StateStack stack){}

    public virtual void OnTakeDamage(EntityStats receiver){}

    public virtual void OnDealDamage(EntityStats dealer, EntityStats target, DamageSource damageInfo = null){}
    
    public virtual void ConcludeState(EntityStats receiver){}
    
}
