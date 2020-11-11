using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractionControls : MonoBehaviour
{
  public GameEvent skipDialogLineEvent;

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

    public void SkipDialog(InputAction.CallbackContext context){
    if(context.performed){//button down
      skipDialogLineEvent?.Raise();
    }
  }
}
