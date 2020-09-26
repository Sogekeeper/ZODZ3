using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Hellmade.Sound;

public class EntityStats : MonoBehaviour
{
    [Header("Base Stats")]
    public Race baseRace;
    // CharacterStat precisa ser criado por script, entao aqui fica a base pra digitar no inspector
    public int baseLife;
    public int baseMana;
    public int baseStrength;
    public int baseMind;
    public int baseConstitution;
    public int baseSpirit;

    public CharacterStat totalLife; 
    public CharacterStat totalMana; 
    public CharacterStat strength;
    public CharacterStat mind;
    public CharacterStat constitution;
    public CharacterStat spirit;

    public int currentLife{get; protected set;}
    public int currentMana{get; protected set;}

    [Header("Non Visible Stats")]
    [Min(1)]public float knockbackReduction = 15; //leva impulso menor das porradas
    [Min(0)]public float recoveryTime = 0.5f; //tempo para poder atacar de novo depois de golpe
    public CharacterStat critMultiplier = new CharacterStat(2f);
    public CharacterStat elementMultiplier = new CharacterStat(1.5f);
    public CharacterStat critChance = new CharacterStat(0.2f);
    public CharacterStat speedMultiplier = new CharacterStat(1f); //velocidade - nao implementado
    public CharacterStat attackSpeed = new CharacterStat(1f); //velocidade de anims de ataque fisico - nao implementado - colocar param novo em SkillUser para mudar de acordo
    public CharacterStat castingSpeed = new CharacterStat(1f); //velocidade de anims de projeteis - nao implementado
    public List<EntityRuntimeSet> myEntitySets; //remove later
    public List<EntityRuntimeSet> enemyEntitySets;
    public List<EntityRuntimeSet> originalMySets {get; protected set;}
    public List<EntityRuntimeSet> originalEnemySets {get; protected set;}

    [Header("Events")] //utilidades para o resto do jogo
    public UnityEventAlternatives.IntEvent OnTakeDamage;
    public UnityEvent OnDeath;
    public UnityEventAlternatives.EntityEvent OnStatesUpdate;

    [Header("Components")]
    public Rigidbody2D rb;
    public SpriteRenderer entityGraphic;

    [Header("Optional")]
    public Animator anim;
    public AudioClip takeDamageSound;
    public AudioClip deathSound;
    
    public bool canMove{
        get{
            if(stunned || downTimer > 0){
                return false;
            }else{
                return _canMove;
            }
        }
        set{
            _canMove = value;
        }
    }

    public bool canAct{ //não incluiria coisas como rooted
        get{
            if(stunned || downTimer > 0){
                return false;
            }else{
                return _canAct;
            }
        }
        set{
            _canAct = value;
        }
    }

    private bool _canMove = true;
    private bool _canAct = true;
    public void CanMove(bool canItMove){canMove = canItMove;rb.velocity = Vector3.zero;} //pra unity eventos
    public void CanAct(bool canItAct){canAct = canItAct;}

    [Header("Other Info")]
    public bool knockbackImmune = false;
    public bool stunned = false;
    public float downTimer {get; private set;}
    [Header("States")]
    public List<State.StateStack> states;

    protected virtual void Awake() {
        states = new List<State.StateStack>();
        totalLife = new CharacterStat(baseLife);
        totalMana = new CharacterStat(baseMana);
        strength = new CharacterStat(baseStrength);
        mind = new CharacterStat(baseMind);
        spirit = new CharacterStat(baseSpirit);
        constitution = new CharacterStat(baseConstitution);
    }
    
    private void OnEnable() {
        if(myEntitySets != null){
            for(int i = 0; i < myEntitySets.Count; i++){
                myEntitySets[i].Add(this);
            }
        }
        //backup original sets to recover after changes
        originalMySets = EntityRuntimeSet.CopySetArray(myEntitySets);
        originalEnemySets = EntityRuntimeSet.CopySetArray(enemyEntitySets);
    }
    private void OnDisable() {
        if(myEntitySets != null){
            for(int i = 0; i < myEntitySets.Count; i++){
                myEntitySets[i].Remove(this);
            }
        }
    }

    protected virtual void Start() {        
        currentLife = (int)totalLife.Value;      
        currentMana = (int)totalMana.Value;        
    }

    protected virtual void Update() {
        UpdateStates();

        if (downTimer > 0){
            downTimer -= Time.deltaTime;
            if(downTimer <=0 && anim && canAct){
                anim.SetBool("down",false);
            }
        }
    }

    #region STATES
    public void ApplyState(State targetState, EntityStats applier = null){
        targetState.InitState(this,applier);
        OnStatesUpdate?.Invoke(this);
    }
    public bool RemoveState(State targetState){
        State.StateStack stack = GetStack(targetState);
        if(stack == null) return false;
        stack.EndState(this);
        return true;
    }

    public State.StateStack GetStack(State targetState){
        if(states == null || states.Count <= 0)
            return null; //não existe estado
        for(int i = 0; i < states.Count; i++){
            if(states[i].currentState == targetState){
                return states[i]; //achou o primeiro stack daquele estado
            }
        }
        return null; //não existe stack daquele estado
    }
    
    private void UpdateStates(){
        if(states != null && states.Count >0){
            for(int i = 0; i < states.Count; i++){
                states[i].TickState(this,Time.deltaTime);
            }
        }
    }

    private void TriggerDamageStates(){
        if(states != null && states.Count >0){
            for(int i = 0; i < states.Count; i++){
                states[i].currentState.OnTakeDamage(this);
            }
        }
    }
    #endregion

    public void TakeDamage(int damage){
        int totalDmg = damage;
        //calcular dano
        ComputateDamage(totalDmg);
    }        
    public void TakeDamage(DamageSource damage){
        int totalDmg = damage.damageValue;
        float distance = damage.transform.position.x - transform.position.x;
        if(anim){
            anim.SetFloat("damageSide",distance);
        }
        //calcular dano
        ComputateDamage(totalDmg);
    }

    private void ComputateDamage(int damage){
        if(currentLife <= 0) return;
        if(takeDamageSound) EazySoundManager.PlaySound(takeDamageSound,0.2f);
        OnTakeDamage?.Invoke(damage);
        currentLife -= damage;
        StartDownTime();
        rb.velocity = Vector3.zero;
        if(currentLife <= 0){
            currentLife = 0;
            Die();
        }
    }

    public void Heal(int amount){
        amount = (int)Mathf.Clamp(amount,0,99999);
        currentLife += amount;
        if(currentLife > (int)totalLife.Value)
            currentLife = (int)totalLife.Value;
    }

    public void ChangeMana(int amountToAdd){
        currentMana += amountToAdd;
        if(currentMana < 0) currentMana = 0;
        else if(currentMana > (int)totalMana.Value) currentMana = (int)totalMana.Value;
    }

    protected virtual void Die(){
        OnDeath?.Invoke();
        if(deathSound) EazySoundManager.PlaySound(deathSound,0.4f);
    }

    public void ApplyKnockback(Vector3 source, float force){
        if(knockbackImmune || force <= 0) return;
        StopAllCoroutines();
        Vector2 knDirection = transform.position - source;
        knDirection.Normalize();
        //Debug.Log(knDirection.x.ToString()+" / "+knDirection.y);
        if(gameObject.activeInHierarchy)StartCoroutine(KnockbackRoutine(new Vector2(knDirection.x, knDirection.y), force));
    }
    public void ApplyKnockback(Vector2 direction, float force){
        if(knockbackImmune || force <= 0) return;
        StopAllCoroutines();
        Vector2 knDirection = direction;
        //Debug.Log(direction.x.ToString()+" / "+direction.y);
        //knDirection.Normalize();
        if(gameObject.activeInHierarchy)StartCoroutine(KnockbackRoutine(new Vector2(knDirection.x, knDirection.y), force));
    }

    private IEnumerator KnockbackRoutine(Vector2 direction, float force){
        rb.velocity = Vector3.zero;
        float currentForce = force;
        float friction = (100 - knockbackReduction)/100;
        canMove = false;

        //rb.AddForce(direction * currentForce);
        
        while(currentForce > 0.1f){
            yield return null;
            currentForce *= friction;            
            rb.MovePosition(rb.position + (direction * currentForce * Time.fixedDeltaTime));
            
            //rb.velocity = ( direction * currentForce * Time.fixedDeltaTime);       
        }
        canMove = true;
    }
    
    private void StartDownTime(){
        downTimer = recoveryTime;
        if(anim){
            anim.Play("Damage",0,0); //mudar para hash por performance
            anim.SetBool("down",true);
        }
    }
}
