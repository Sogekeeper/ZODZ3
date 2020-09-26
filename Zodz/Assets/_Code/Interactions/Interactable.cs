using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class AbilityToInteractEvent : UnityEvent<AbilityToInteract>
{

}

public class Interactable : MonoBehaviour
{
	public AbilityToInteractEvent OnInteract;
	public bool interactOnlyOnce = false;
	[Header("PopUp")]
	public GameObject pressToInteractPopUp;
	public bool showPopUpOnlyOnce = false;
	public bool canInteract = true; public void CanInteract(bool canIt){canInteract = canIt;} //pra unity eventos
	
	[HideInInspector]public AbilityToInteract actor; //setado pelo próprio Ability

	private int numberOfInteractions = 0;

	private void Start() {
		if(pressToInteractPopUp)pressToInteractPopUp.SetActive(false);
	}

	public void BeginInteraction()
	{
		if(interactOnlyOnce && numberOfInteractions > 0) return;
		if(!canInteract) return;

		numberOfInteractions++;
		OnInteract.Invoke(actor);
		if(pressToInteractPopUp)pressToInteractPopUp.SetActive(false);
		//Debug.Log("BeginInteraction");
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if(other.CompareTag("Player") && canInteract)
			TogglePopUp(true);
	}

	private void OnTriggerExit2D(Collider2D other) {
		if(other.CompareTag("Player")&& canInteract)
			TogglePopUp(false);
	}

	private void TogglePopUp(bool isVisible){
		if(showPopUpOnlyOnce && numberOfInteractions > 0) return;

		if(pressToInteractPopUp)pressToInteractPopUp.SetActive(isVisible);
	}
}
