using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionControls : MonoBehaviour
{
	AbilityToInteract abilityToInteract;
    // Start is called before the first frame update
    void Start()
    {
		abilityToInteract = this.GetComponent<AbilityToInteract>();

	}

    // Update is called once per frame
    void Update()
    {
		if(Input.GetKeyDown(KeyCode.X))
		{
			abilityToInteract.Interact();
		}
    }
}
