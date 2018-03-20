using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MenuQualityUI : GenericUI
{
	public Button[] presetButtons;

	public void Update()
	{
		int clevel = QualitySettings.GetQualityLevel();
		for (int i = 0; i < presetButtons.Length; i++)
		{
			presetButtons[i].interactable = i != clevel;
		}
	}

	public void LoadPreset(int level)
	{
		QualitySettings.SetQualityLevel(level, true);
	}
}
