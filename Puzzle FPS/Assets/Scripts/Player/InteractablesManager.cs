using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablesManager : MonoBehaviour
{
	public InputReader inputReader;

	public float ReachDistance = 5f;

	public int interactableLayer;

	public void Update()
	{
		if (GameManager.Instance.PlayStarted() && GameManager.Instance.PlayerScript() != null)
		{
			Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit interactingHit;
			if (Physics.Raycast(inputRay, out interactingHit, ReachDistance, ~interactableLayer))
			{
				Interactable currentInteractable;
				if (interactingHit.collider != null)
					currentInteractable = interactingHit.collider.GetComponent<Interactable>();
				else
					currentInteractable = null;

				if (currentInteractable != null)
				{
					inputReader.DisplayMessage(interactingHit.collider.gameObject.name); // the line so that the item names appear on screen
					if (Input.GetButtonDown(PlayerPreferences.Instance.Button_Interact))
					{
						currentInteractable.Interact();
						inputReader.ClearMessage();
					}
				}
				else
				{
					inputReader.ClearMessage();
				}
			}
			else
			{
				inputReader.ClearMessage();

			}
		}
	}
}
