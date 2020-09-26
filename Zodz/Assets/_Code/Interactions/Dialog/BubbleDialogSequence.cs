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

  public void StartSequence(AbilityToInteract abilityToInteract)
  {
		if (!dialogDone)
    	StartCoroutine(SpawnTexts(abilityToInteract));

  }
  public void StartSequence() //para cutscenes onde o jogador não interage manualmente
  {
    if (!dialogDone)
      StartCoroutine(SpawnTexts(null));
  }

  IEnumerator SpawnTexts(AbilityToInteract actor)
  {
    for (int i = 0; i < texts.Length; i++)
    {
      if (texts[i].isFromActor && actor != null)
      {
        SpawnBubbleOn(texts[i].bubbleText,actor.transform, texts[i].timeToReadLine);
      }
      else
      {
				if(conversationMembers != null && conversationMembers.Length > texts[i].targetMemberIndex
					&& texts[i].targetMemberIndex >= 0 && conversationMembers[texts[i].targetMemberIndex]){
					SpawnBubbleOn(texts[i].bubbleText,conversationMembers[texts[i].targetMemberIndex],texts[i].timeToReadLine);
				}else{
					SpawnBubbleOn(texts[i].bubbleText,transform,texts[i].timeToReadLine);
				}
      }

      yield return new WaitForSeconds(texts[i].timeToReadLine+0.5f); //offset pro prox text aparecer
    }
    OnDialogEnd?.Invoke();
    dialogDone = true;
  }

	private void SpawnBubbleOn(string dialogText,Transform target, float readTime){
		BubbleDialog b = bubblePool.SpawnTargetObject(bubbleObj,3).GetComponent<BubbleDialog>();
		b.transform.position = target.transform.position + new Vector3(0,bubbleOffsetY,0);
		b.InitBubble(dialogText,readTime);
	}

}
