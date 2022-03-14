using UnityEngine;

public class Tige : MonoBehaviour
{
	private bool active;

	public void SetActive(bool b)
	{
		active = b;
		if (b)
		{
			base.gameObject.SetActive(true);
		}
		else
		{
			base.gameObject.SetActive(false);
		}
	}
}
