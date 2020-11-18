using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Hellmade.Sound;
using MilkShake;

public class EntityStats : MonoBehaviour
{
    [Header("Base Stats")]
    public Race baseRace;
    // CharacterStat precisa ser criado por script, entao aqui fica a base pra digitar no inspector
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
    public ShakePreset onFallDamageShake;
    
    private Vector3 lastGroundPos;
    private Vector3 originalScale;
    private int holeLayer;

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
    public void ToggleMoveAndAct(bool active){CanMove(active);CanAct(active);}

    [Header("Other Info")]
    public bool recoverFall = false;
    public bool knockbackImmune = false;
    public bool stunned = false;
    public bool airBorne = false;
    public bool startWithMana = false;
    public bool falling{get; private set;}
    public bool closeToFallArea {get; private set;}
    public float downTimer {get; private set;}
    [Header("States")]
    public List<State.StateStack> states;

    protected virtual void Awake() {
        states = new List<State.StateStack>();
        strength = new CharacterStat(baseStrength);
        mind = new CharacterStat(baseMind);
        spirit = new CharacterStat(baseSpirit);
        constitution = new CharacterStat(baseConstitution);
        totalLife = new CharacterStat(Formulas.CalculateLifePoints((int)constitution.Value));
        totalMana = new CharacterStat(Formulas.CalculateManaPoints((int)spirit.Value));
        originalScale = transform.localScale;
        holeLayer = LayerMask.NameToLayer("Hole");
        
        falling = false;
        closeToFallArea = false;
    }
    
    protected virtual void OnEnable() {
        if(myEntitySets != null){
            for(int i = 0; i < myEntitySets.Count; i++){
                myEntitySets[i].Add(this);
            }
        }
        //backup original sets to recover after changes
        originalMySets = EntityRuntimeSet.CopySetArray(myEntitySets);
        originalEnemySets = EntityRuntimeSet.CopySetArray(enemyEntitySets);
    }
    protected virtual void OnDisable() {
        if(myEntitySets != null){
            for(int i = 0; i < myEntitySets.Count; i++){
                myEntitySets[i].Remove(this);
            }
        }
    }

    protected virtual void Start() {        
        currentLife = (int)totalLife.Value;
        currentMana = startWithMana ? (int)totalMana.Value : 0;
        lastGroundPos = transform.position;
        StartCoroutine(GroundedRoutine());
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

    private IEnumerator GroundedRoutine(){
        while(true){
            if(!airBorne && !closeToFallArea && !Physics2D.BoxCast(transform.position,new Vector2(1f,1f),0,Vector2.zero,0,holeLayer)){
                if(canAct && canMove)lastGroundPos = transform.position;
            }
            yield return new WaitForSeconds(0.6f);
        }
    }

    #region STATES
    public void ApplyState(State targetState, EntityStats applier = null){
        targetState.InitState(this,applier);
        UpdateMaxLifeAndMana();
        OnStatesUpdate?.Invoke(this);
    }
    public bool RemoveState(State targetState){
        State.StateStack stack = GetStack(targetState);
        if(stack == null) return false;
        stack.EndState(this);
        UpdateMaxLifeAndMana();
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

    public void Fall(){
        //tocar animacao
        falling = true;
        CanMove(false);
        CanAct(false);
        LeanTween.scale(gameObject,transform.localScale*0.8f,0.2f).setOnComplete(FallCallbackDamage);
    }
    public void FallCallbackDamage(){        
        if(anim){
            anim.Play("Fall");
        }
        if(onFallDamageShake) Shaker.ShakeAll(onFallDamageShake);
        TakeDamage((int)(totalLife.Value * 0.10f));
        if(!recoverFall) Die();   
    }
    public void FallCallbackReposition(){
        if(currentLife<=0) return;
        //anim.Play("Movement");
        transform.localScale = originalScale;
        transform.position = lastGroundPos;
        CanMove(true);
        CanAct(true);
        falling = false;
    }

    protected virtual void Die(){
        OnDeath?.Invoke();
        if(deathSound) EazySoundManager.PlaySound(deathSound,0.4f);
    }

    protected virtual void UpdateMaxLifeAndMana(){
        int previousMaxLife = (int)totalLife.Value;
        int previousMaxMana = (int)totalMana.Value;
        totalLife = new CharacterStat(Formulas.CalculateLifePoints((int)constitution.Value));
        totalMana = new CharacterStat(Formulas.CalculateManaPoints((int)spirit.Value));
        currentLife += (int)(totalLife.Value - previousMaxLife);
        currentMana += (int)(totalMana.Value - previousMaxMana);
        if(currentLife <= 0) currentLife = 1;
        else if(currentLife > totalLife.Value) currentLife = (int)totalLife.Value;
        if(currentMana <= 0) currentMana = 0;
        else if(currentMana > totalMana.Value) currentMana = (int)totalMana.Value;
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
            anim.ResetTrigger("dmg");
            anim.SetTrigger("dmg");
            anim.SetBool("down",true);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(CompareTag("Hole")){
            closeToFallArea = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(CompareTag("Hole")){
            closeToFallArea = false;
        }
    }
}
