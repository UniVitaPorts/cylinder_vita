using System;
using System.Collections.Generic;
using UnityEngine;

public class Ligne : MonoBehaviour
{
	public Tige[] tigeList = new Tige[9];

	public EmplacementDent emp;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void Read()
	{
		throw new NotImplementedException();
	}

	public void Update(List<bool> partition, int bornInf, int bornSup, int bornLim)
	{
		if (bornInf > bornSup)
		{
			for (int num = bornSup; num >= 0; num--)
			{
				tigeList[bornSup - num].SetActive(partition[num]);
			}
			for (int num2 = bornLim; num2 >= bornInf; num2--)
			{
				tigeList[bornLim - num2 + bornSup + 1].SetActive(partition[num2]);
			}
		}
		for (int num3 = bornSup; num3 >= bornInf; num3--)
		{
			tigeList[bornSup - num3].SetActive(partition[num3]);
		}
		if (partition[bornInf])
		{
			emp.Play();
		}
	}

	public int GetIdDent()
	{
		return emp.GetIdDent();
	}
}
