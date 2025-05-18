using UnityEngine;
using UnityEngine.UI;

public class LevelSelection1 : MonoBehaviour
{
	public Button[] lvlButtons;

	private void Start()
	{
		int @int = PlayerPrefs.GetInt("levelAt", 2);
		for (int i = 0; i < lvlButtons.Length; i++)
		{
			if (i + 2 > @int)
			{
				lvlButtons[i].interactable = false;
			}
		}
	}
}
