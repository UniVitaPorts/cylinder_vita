using System.Collections.Generic;
using UnityEngine;

public class PartitionView : MonoBehaviour
{
	public void RefreshView(Ligne[] ligneList, List<bool>[] partition, int bornInf, int bornSup, int bornLim)
	{
		for (int i = 0; i < 8; i++)
		{
			ligneList[i].Update(partition[i], bornInf, bornSup, bornLim);
		}
	}
}
