using UnityEngine;

public class Manivelle : MonoBehaviour
{
	private int state;

	private int nbCrank;

	private int limitCrank = 10;

	private int maxCrank;

	private void Start()
	{
	}

	public void Crank()
	{
		if (state == 7)
		{
			state = 0;
		}
		else
		{
			state++;
		}
		if (nbCrank == maxCrank)
		{
			nbCrank = 0;
		}
		else
		{
			nbCrank++;
		}
	}

	public void ReverseCrank()
	{
		if (state == 0)
		{
			state = 7;
		}
		else
		{
			state--;
		}
		if (nbCrank == 0)
		{
			nbCrank = maxCrank;
		}
		else
		{
			nbCrank--;
		}
	}

	public int GetState()
	{
		return state;
	}

	public bool IsLimitPoint()
	{
		return nbCrank == limitCrank;
	}

	public bool IsBeginning()
	{
		return nbCrank == 0;
	}

	public void SetMaxCrank(int crank)
	{
		maxCrank = crank;
	}
}
