using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharacterSelectButtonFunction : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{

	[Header("Targets")]
	public Image bGImage;
	public Image downImage;

	[Space]
	[Header("Refs")]
	public Sprite unhighlighted;
	public Sprite highlighted;
	public Sprite selected;

	[Space]
	[Header("RacesAnim")]
	public RaceDependingAnimation idleAnim;
	public RaceDependingAnimation runAnim;
	public Race targetRaceAnim;
	public Animator character;

	[Space]
	[Header("Tween and SetActive")]
	public GameObject otherCharcter1;
	public GameObject otherCharcter2;
	public GameObject firstPos;
	public GameObject SelectLunarCanvas;

	[Space]
	[Header("ChooseHandler")]
	public Race race;
	public PlayerRaceChoice playerChoice;
	public PlayerCharacterSettings playerCharacterSettings;
	public SoundMenu clickSound;



	public void Start()
	{

	}

	public void OnEnable()
	{
		this.gameObject.GetComponent<RectTransform>().localScale = Vector3.one;
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		bGImage.sprite = selected;
		downImage.sprite = selected;
		playerChoice.ChooseRace(race);
		LeanTween.scale(otherCharcter1, Vector3.zero, 0.5f).setEaseInBack();
		LeanTween.scale(otherCharcter2, Vector3.zero, 0.5f).setEaseInBack();
		LeanTween.scale(this.gameObject, Vector3.zero, 0.5f).setEaseInBack();
		StartCoroutine(SetActive(SelectLunarCanvas, 0.5f, true));
		playerCharacterSettings.solarRace = PlayerRaceChoice.playerRaceChoice;
		clickSound.PlayClickSound();
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		bGImage.sprite = highlighted;
		downImage.sprite = highlighted;
		character.SetBool("Highlighted", true);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		bGImage.sprite = unhighlighted;
		downImage.sprite = unhighlighted;
		character.SetBool("Highlighted", false);
		
	}

	IEnumerator SetActive(GameObject obj, float seconds,  bool trueOrFalse)
	{
		yield return new WaitForSeconds(seconds);
		obj.SetActive(trueOrFalse);
		this.gameObject.transform.parent.gameObject.SetActive(false);
	}
}
