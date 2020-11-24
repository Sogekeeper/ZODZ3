using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BubbleRaceDialogSequence : MonoBehaviour
{
   
  public PoolObject bubbleObj;
  //public GameObject canvasWorldSpace;
	public PoolContainer bubblePool;
  public RaceDependingDialog raceDialogAsset;
  public EntityStats defaultActor;
  public Race[] racesForEvent;
  public Transform[] conversationMembers; // tudo menos o player
	public float bubbleOffsetY;
	public bool repeatOnCall = false; //se pode tocar de novo caso um código peça

	public UnityEvent OnDialogEnd;
  public UnityEvent OnValidRace;
  public UnityEvent OnInvalidRace;

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
      BubbleDialogSequence.LineInfo[] texts = null;
      if(actor) texts = raceDialogAsset.GetRaceLines(actor.actorEntity.baseRace);
      else if(defaultActor) texts = raceDialogAsset.GetRaceLines(defaultActor.baseRace);

    for (int i = 0; i < texts.Length; i++)
    {
      if (texts[i].isFromActor && actor != null)
      {
        SpawnBubbleOn(texts[i].bubbleText,actor.transform, texts[i].timeToReadLine, texts[i].bubbleTitle);
      }
      else
      {
				if(conversationMembers != null && conversationMembers.Length > texts[i].targetMemberIndex
					&& texts[i].targetMemberIndex >= 0 && conversationMembers[texts[i].targetMemberIndex]){
					SpawnBubbleOn(texts[i].bubbleText,conversationMembers[texts[i].targetMemberIndex],texts[i].timeToReadLine, texts[i].bubbleTitle);
				}else{
					SpawnBubbleOn(texts[i].bubbleText,transform,texts[i].timeToReadLine, texts[i].bubbleTitle);
				}
      }

      yield return new WaitForSeconds(texts[i].timeToReadLine+0.5f); //offset pro prox text aparecer
    }
    OnDialogEnd?.Invoke();
    //if(actor)
    dialogDone = true;
  }

	private void SpawnBubbleOn(string dialogText,Transform target, float readTime, string title=""){
		BubbleDialog b = bubblePool.SpawnTargetObject(bubbleObj,3).GetComponent<BubbleDialog>();
		b.transform.position = target.transform.position + new Vector3(0,bubbleOffsetY,0);
		b.InitBubble(dialogText,readTime,title);
	}

  private bool CheckRaceForEvent(Race currentRace){
    if(racesForEvent == null || racesForEvent.Length <= 0) return false;
    for (int i = 0; i < racesForEvent.Length; i++)
    {
        if(currentRace == racesForEvent[i]) return true;
    }
    return false;
  }

}
