using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionCondition : EventCondition
{
    public PuzzleButton Objective;
    public TaskListUI_Interaction ConditionUI;

    public override bool CheckCompletion()
    {
        if (Objective.Interacted)
            satisfied = true;
        else
            satisfied = false;

        return base.CheckCompletion();
    }
    public void UpdateInteractionUI(InteractionCondition interaction)
    {
        interaction.ConditionUI.ConditionToggle.isOn = satisfied;
        interaction.ConditionUI.ConditionalUIText.text = "Interact with: " + interaction.Objective.gameObject.name;
    }
}
