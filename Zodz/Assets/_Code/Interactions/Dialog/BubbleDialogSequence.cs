using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class BubbleDialogSequence : MonoBehaviour
{
  [System.Serializable]
  public class LineInfo
  {
    //por enquanto só tem o texto, mas pode ter mais coisas depois
    [TextArea]
    public string bubbleText = "";
    public bool isFromActor = false; //mostrar dialogo em cima do player
    public int targetMemberIndex = 0; //index do membro na conversa para a bolha aparecer em cima      
    public float timeToReadLine = 3; //tempo até linha acabar
  }

  public PoolObject bubbleObj;
  //public GameObject canvasWorldSpace;
	public PoolContainer bubblePool;
  public LineInfo[] texts;
  public Transform[] conversationMembers; // tudo menos o player
	public float bubbleOffsetY;
	public bool repeatOnCall = false; //se pode tocar de novo caso um código peça

	public UnityEvent OnDialogEnd;

  // private AbilityToInteract whoInitializedDialog;
  private bool dialogDone = false;
  private bool dialogBegan= false;
  private int currentDialogIndex = 0;
  private float dialogTimer;
  private AbilityToInteract actor = null;
  private BubbleDialog currentBubble = null;

  private float offsetTimer = 0.6f; //para dar tempo do texto subir, precisa ser private pq não quero que seja alterado

  public void StartSequence(AbilityToInteract abilityToInteract)
  {
    if(dialogBegan) return;
    if((!repeatOnCall && dialogDone)) return;

    dialogBegan = true;
    currentDialogIndex = 0;
    actor = abilityToInteract;
    	ProgressDialog();

  }
  public void StartSequence() //para cutscenes onde o jogador não interage manualmente
  {
    if(dialogBegan) return;
    if((!repeatOnCall && dialogDone)) return;

    dialogBegan = true;
    currentDialogIndex = 0;
    actor = null;
      ProgressDialog();
  }

  private void Update() {
    if(dialogTimer > 0){
      dialogTimer -= Time.deltaTime;
      if(dialogTimer <= 0){
        ProgressDialog();
      }
    }
  }

  public void SkipLine(){
    //acabar timer e 
    if(dialogTimer > offsetTimer){
      dialogTimer = offsetTimer;
      currentBubble?.SkipReadTime();
    } 
  }

  private void ProgressDialog(){
    if(currentDialogIndex >= texts.Length){
      OnDialogEnd?.Invoke();
      dialogDone = true;
      dialogBegan = false;
      currentBubble = null;
      return;
    }

    if (texts[currentDialogIndex].isFromActor && actor != null)
    {
      SpawnBubbleOn(texts[currentDialogIndex].bubbleText,actor.transform, texts[currentDialogIndex].timeToReadLine);
    }
    else
    {
			if(conversationMembers != null && conversationMembers.Length > texts[currentDialogIndex].targetMemberIndex
				&& texts[currentDialogIndex].targetMemberIndex >= 0 && conversationMembers[texts[currentDialogIndex].targetMemberIndex]){
				SpawnBubbleOn(texts[currentDialogIndex].bubbleText,conversationMembers[texts[currentDialogIndex].targetMemberIndex],texts[currentDialogIndex].timeToReadLine);
			}else{
				SpawnBubbleOn(texts[currentDialogIndex].bubbleText,transform,texts[currentDialogIndex].timeToReadLine);
			}
    }

    currentDialogIndex++;
    if(currentDialogIndex == texts.Length){
      dialogTimer = texts[currentDialogIndex-1].timeToReadLine + offsetTimer;
    }else{
      dialogTimer = texts[currentDialogIndex-1].timeToReadLine + offsetTimer;      
    }

  }


	private BubbleDialog SpawnBubbleOn(string dialogText,Transform target, float readTime){
		BubbleDialog b = bubblePool.SpawnTargetObject(bubbleObj,3).GetComponent<BubbleDialog>();
		b.transform.position = target.transform.position + new Vector3(0,bubbleOffsetY,0);
		b.InitBubble(dialogText,readTime);
    currentBubble = b;
    return b;
	}

}
