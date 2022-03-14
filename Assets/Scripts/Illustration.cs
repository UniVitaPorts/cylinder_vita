using UnityEngine;

public class Illustration : MonoBehaviour
{
	private Vector3 startPosition;

	private Vector3 endPosition;

	private float xSize;

	private int nbTrans;

	private float speed;

	private int pos;

	private int remaining;

	private int waitingTime;

	private void Awake()
	{
		startPosition = base.transform.position;
		xSize = GetComponent<SpriteRenderer>().bounds.size.x;
	}

	private void Update()
	{
		if (remaining > 0)
		{
			base.transform.position = Vector3.Lerp(startPosition, endPosition, (float)(waitingTime - remaining) / (float)waitingTime);
			remaining--;
		}
		else if (remaining < 0)
		{
			base.transform.position = Vector3.Lerp(startPosition, endPosition, (float)(waitingTime + remaining) / (float)waitingTime);
			remaining++;
		}
	}

	public void Read()
	{
		pos++;
		startPosition = endPosition;
		endPosition = startPosition + Vector3.left * speed;
		remaining = waitingTime;
	}

	public void ReverseRead()
	{
		pos--;
		startPosition = endPosition;
		endPosition = startPosition + Vector3.right * speed;
		remaining = -waitingTime;
	}

	public void FastReverseRead()
	{
		startPosition = endPosition;
		endPosition = startPosition + Vector3.right * speed * 6.005f;
		remaining = -waitingTime;
	}

	public void SetTrans(int nb)
	{
		nbTrans = nb;
		speed = xSize * 3f / (float)(nbTrans + 1);
	}

	public void SetWait(int wait)
	{
		waitingTime = wait;
	}
}
