using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InputReader : MonoBehaviour
{
	public string clearMSG = "";
	public TextMeshProUGUI displayText;
	public string[] messages;

	public void DisplayMessage(string incomingMessage)
	{
		displayText.text = incomingMessage;
	}

	public void ClearMessage()
	{
		displayText.text = clearMSG;
	}
}
