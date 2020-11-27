using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
  public float moveSpeed = 4f;
  public bool canInput = true;

  [Header("Dash Settings")]
  public float dashSpeed = 8f;
  public float dashIFramesTime = 0.15f;
  public float dashTime= 0.5f;
  public float dashCooldown = 1f;

  [Header("Components")]
  public SkillUser skillUser; //para saber se pode andar
  public Rigidbody2D rb; //andar
  public Animator moveAnimOutput; //setar parametos

  [Header("Optional")]
  public Image dashCooldownImage;

  //privates
  private Vector2 movementDirection;

  private Vector2 dashDirection;
  private bool dashing = false;
  private int playerLayer;
  private int damageLayer;
  private int enemyLayer;
  private int holeLayer;
  private float dashTimer = 0;  

  private void Start() {
    playerLayer = LayerMask.NameToLayer("Player");
    damageLayer = LayerMask.NameToLayer("Damage");
    enemyLayer = LayerMask.NameToLayer("Enemies");
    holeLayer = LayerMask.NameToLayer("Hole");
  }

  private void FixedUpdate() {
    if(skillUser.userStats.canMove){
      if(!dashing){
        //rb.MovePosition(rb.position + movementDirection * moveSpeed * Time.fixedDeltaTime);
        //rb.AddForce(movementDirection * moveSpeed * Time.fixedDeltaTime);
        Vector3 targetVelocity = Vector3.Lerp(rb.velocity,movementDirection * moveSpeed * Time.fixedDeltaTime,0.2f);
        rb.velocity = targetVelocity;
      }
      else{
        //rb.MovePosition(rb.position + dashDirection * dashSpeed * Time.fixedDeltaTime);
        //rb.AddForce(dashDirection * dashSpeed * Time.fixedDeltaTime);
        rb.velocity = dashDirection * dashSpeed * Time.fixedDeltaTime;
      }
    }
  }

  private void Update() {
    UpdateMovementAnimator();  
    if(dashTimer > 0){
      dashTimer -= Time.deltaTime;
      if(dashCooldownImage){
        dashCooldownImage.gameObject.SetActive(true);
        dashCooldownImage.fillAmount = dashTimer/dashCooldown;
      } 
    } else{
      if(dashCooldownImage) dashCooldownImage.gameObject.SetActive(false);
    }
  }

  public void SetMovementInput(InputAction.CallbackContext context){
    if(canInput)movementDirection = context.ReadValue<Vector2>().normalized;    //input do jogador
    else movementDirection = Vector2.zero;
    //if(!skillUser.userStats.canMove) return;    
  }

  public void Dash(InputAction.CallbackContext context){
    if(context.performed && dashTimer <= 0 && !skillUser.usingSkill && skillUser.userStats.canMove && canInput){//button down
      StartCoroutine(DashRoutine());
      StartCoroutine(IFramesRoutine());
      dashTimer = dashCooldown;
    }
  }

  public bool CheckIfCanUpdateAnimatorInput(){
    if(movementDirection.y < -0.1f || movementDirection.y > 0.1f || 
      movementDirection.x < -0.1f || movementDirection.x > 0.1f){
        return true;
    }else
      return false;
  }

  public void SetMoveAnimParamX(float amount){
    moveAnimOutput.SetFloat("horizontal",Mathf.Clamp01(amount));
  }
  public void SetMoveAnimParamY(float amount){
    moveAnimOutput.SetFloat("vertical",Mathf.Clamp01(amount));
  }

  private void UpdateMovementAnimator(){
    if(!skillUser.userStats.canMove){
      moveAnimOutput.SetFloat("speed",0);
      return;
    }
    //quando o valor do parametro no animator é 0, ele pode não mostrar a anim correta na blend tree
    //então a gente checa se pode colocar o valor    
    moveAnimOutput.SetFloat("speed",Mathf.Clamp01(Mathf.Abs(movementDirection.magnitude)));
    if(CheckIfCanUpdateAnimatorInput()) 
      moveAnimOutput.SetFloat("horizontal",movementDirection.x);
    if(CheckIfCanUpdateAnimatorInput()){
      if(movementDirection.y > -0.8f && movementDirection.y < 0.8f) //caso o jogador estiver apertando 2 direções (diagonal), os dois viram 0.7, eu quero com certeza mostrar o andar pro lado 
        moveAnimOutput.SetFloat("vertical",0);
      else
        moveAnimOutput.SetFloat("vertical",movementDirection.y);
    }
  }

  public IEnumerator DashRoutine(){
    dashing = true; skillUser.userStats.airBorne =true;
    moveAnimOutput.SetBool("dashing",dashing);
    Physics2D.IgnoreLayerCollision(playerLayer,damageLayer,true);
    Physics2D.IgnoreLayerCollision(playerLayer,enemyLayer,true);
    Physics2D.IgnoreLayerCollision(playerLayer,holeLayer,true);
    dashDirection = new Vector2(movementDirection.x, movementDirection.y);
    yield return new WaitForSeconds(dashTime);
    dashing = false; skillUser.userStats.airBorne =false;
    moveAnimOutput.SetBool("dashing",dashing);
    Physics2D.IgnoreLayerCollision(playerLayer,damageLayer,false);
    Physics2D.IgnoreLayerCollision(playerLayer,enemyLayer,false);
    Physics2D.IgnoreLayerCollision(playerLayer,holeLayer,false);
  }
  public IEnumerator IFramesRoutine(){
    //int currentFrame = 0;
    Physics2D.IgnoreLayerCollision(playerLayer,damageLayer, true);
    Physics2D.IgnoreLayerCollision(playerLayer,holeLayer, true);
    yield return new WaitForSeconds(dashIFramesTime);
    Physics2D.IgnoreLayerCollision(playerLayer,damageLayer, false);
    Physics2D.IgnoreLayerCollision(playerLayer,holeLayer, false);
  }

}
