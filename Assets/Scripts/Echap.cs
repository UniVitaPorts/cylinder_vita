using UnityEngine;

public class Echap : MonoBehaviour
{
	public void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}
	}
}
