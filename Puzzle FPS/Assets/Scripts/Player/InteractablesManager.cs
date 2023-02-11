using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablesManager : MonoBehaviour
{
	public InputReader inputReader;

	public float ReachDistance = 5f;

	public int interactableLayer;

	public void FixedUpdate()
	{
		if (GameManager.Instance.PlayStarted())
		{
			Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit interactingHit;
			if (Physics.Raycast(inputRay, out interactingHit, ReachDistance, ~interactableLayer))
			{
				Interactable currentInteractable = interactingHit.collider.GetComponent<Interactable>();

				if (currentInteractable != null)
				{
					inputReader.DisplayMessage(interactingHit.collider.gameObject.name); // the line so that the item names appear on screen
					if (Input.GetKeyDown(PlayerPreferences.Instance.PLAYERINTERACTKEY))
					{
						currentInteractable.Interact();
						inputReader.ClearMessage();
					}
				}
			}
			else
			{
				inputReader.ClearMessage();

			}
		}
	}
}
