using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Hellmade.Sound;

public class BubbleDialog : MonoBehaviour
{
  public float upSpeed;
  public float timeTilFade = 2;

  [Header("Components")]
  public Animator anim;
  public TextMeshProUGUI bubbleText;

	[Header("Optional")]
	public RectTransform textPanel; //quadrado ou imagem atrás do texto
  public TextMeshProUGUI textTitle;
  public RectTransform textTitlePanel;
	public float spacingX = 50;
	public float spacingY;
  public float titleSpacingX = 5;
  public float titleSpacingY = 5;
  public AudioClip popSound;
  public Vector3 originalScale;

  private bool canMove;
  private float fadeTimer;
  private float timeStopped;
  private float originalUpSpeed;


  [ContextMenu("Set Original Scale")]private void SetOriginalScale(){ originalScale = transform.localScale;}

  private void Awake() {
    originalUpSpeed = upSpeed;
  }

  private void OnEnable() {
    upSpeed = originalUpSpeed;
    transform.localScale = originalScale/3;
    transform.LeanScale(originalScale,0.3f).setEaseOutBack();
  }

  private void Update()
  {
    if (canMove)
      transform.Translate(0,upSpeed*Time.deltaTime,0);

		if(fadeTimer > 0){
			fadeTimer-= Time.deltaTime;
			if(fadeTimer <= 0){
				anim.SetTrigger("out");
			}
		}
  }

  public void InitBubble(string textToDisplay,float timeToRead,string title = "")
  {
    StopAllCoroutines();
    upSpeed = originalUpSpeed;
		fadeTimer = timeTilFade + timeToRead;
		canMove = false;
		anim.Play("Idle",0,0);
    if(popSound != null) EazySoundManager.PlayUISound(popSound,0.15f);
		bubbleText.text = textToDisplay;
		if(textPanel){
			Vector2 textSize = bubbleText.GetPreferredValues() + new Vector2(spacingX,spacingY);
      textPanel.sizeDelta = textSize;
		}
    if(title == null || title.Length <= 1) textTitlePanel?.gameObject.SetActive(false);
    else{
      if(textTitle != null) textTitle.text = title;
      if(textTitlePanel){
        Vector2 titleSize = textTitle.GetPreferredValues() + new Vector2(titleSpacingX,titleSpacingY);
        textTitlePanel.sizeDelta = titleSize;
      }
    }
		timeStopped = timeToRead;
    StartCoroutine(waitToMove());
  }

  private IEnumerator waitToMove()
  {
    yield return new WaitForSeconds(Mathf.Max(timeStopped,0.01f));
    StartToMove();
  }

  public void SkipReadTime(){
    StopAllCoroutines();
    StartToMove();
  }
  private void StartToMove(){
    canMove = true;
    upSpeed = LeanTween.easeInSine(upSpeed,upSpeed*2f,0.4f);
    //transform.LeanScale(originalScale*0.9f,0.6f).setEaseInSine();
  }

  public void EndBubble(){
    gameObject.SetActive(false);//anim event
  }
}
