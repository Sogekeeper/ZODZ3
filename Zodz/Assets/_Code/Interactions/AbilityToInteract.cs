using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityToInteract : MonoBehaviour
{
	public EntityStats actorEntity;
	[SerializeField]
	Interactable currentInteractable;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		Interactable ite = collision.GetComponent<Interactable>();
		if(ite){
			currentInteractable = ite;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if(currentInteractable == collision.GetComponent<Interactable>())
		{
			currentInteractable = null;
		}
	}

	public void Interact()
	{	
		if(currentInteractable)
		{
			//Debug.Log("OnInteract");
			currentInteractable.actor = this;
			currentInteractable.BeginInteraction();
		}
	}
}
