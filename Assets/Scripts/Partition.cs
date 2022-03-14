using System;
using System.Collections.Generic;
using UnityEngine;

public class Partition : MonoBehaviour
{
	public Ligne[] ligne_list = new Ligne[8];

	private string filename = "Partition";

	private List<bool>[] partition = new List<bool>[8];

	private int borneInf;

	private int borneSup = 8;

	private int borneLim;

	private void Awake()
	{
		for (int i = 0; i < 8; i++)
		{
			partition[i] = new List<bool>();
		}
		Load();
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	private void Load()
	{
		try
		{
			string[] array = Resources.Load<TextAsset>(filename).ToString().Split('\n');
			int num = 0;
			string[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				string[] array3 = array2[i].Split('|');
				int num2 = 7;
				string[] array4 = array3;
				foreach (string text in array4)
				{
					if (!(text == "") && num2 >= 0)
					{
						bool item = ((text == "*") ? true : false);
						partition[num2].Add(item);
						num2--;
					}
				}
				num++;
			}
			borneLim = num - 1;
		}
		catch (Exception ex)
		{
			Debug.Log(ex.Message);
			Application.Quit();
		}
	}

	public void Read()
	{
		if (borneSup < borneLim)
		{
			borneSup++;
		}
		else
		{
			borneSup = 0;
		}
		if (borneInf < borneLim)
		{
			borneInf++;
		}
		else
		{
			borneInf = 0;
		}
	}

	public void ReverseRead()
	{
		if (borneInf > 0)
		{
			borneInf--;
		}
		else
		{
			borneInf = borneLim;
		}
		if (borneSup > 0)
		{
			borneSup--;
		}
		else
		{
			borneSup = borneLim;
		}
	}

	public Ligne[] GetLigneList()
	{
		return ligne_list;
	}

	public List<bool>[] GetPartition()
	{
		return partition;
	}

	public int GetBorneInf()
	{
		return borneInf;
	}

	public int GetBorneSup()
	{
		return borneSup;
	}

	public int GetBorneLim()
	{
		return borneLim;
	}

	public int[] GetDents()
	{
		int[] array = new int[8];
		int num = 0;
		Ligne[] array2 = ligne_list;
		foreach (Ligne ligne in array2)
		{
			array[num] = ligne.GetIdDent();
			num++;
		}
		return array;
	}
}
