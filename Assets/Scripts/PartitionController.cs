using UnityEngine;

public class PartitionController : MonoBehaviour
{
	private Partition model;

	private PartitionView view;

	private void Awake()
	{
		model = GetComponent<Partition>();
		view = GetComponent<PartitionView>();
	}

	private void Start()
	{
		view.RefreshView(model.GetLigneList(), model.GetPartition(), model.GetBorneInf(), model.GetBorneSup(), model.GetBorneLim());
	}

	private void Update()
	{
	}

	public void Read()
	{
		model.Read();
		view.RefreshView(model.GetLigneList(), model.GetPartition(), model.GetBorneInf(), model.GetBorneSup(), model.GetBorneLim());
	}

	public void ReverseRead()
	{
		model.ReverseRead();
		view.RefreshView(model.GetLigneList(), model.GetPartition(), model.GetBorneInf(), model.GetBorneSup(), model.GetBorneLim());
	}

	public int[] GetDents()
	{
		return model.GetDents();
	}

	public int GetBorneLim()
	{
		return model.GetBorneLim();
	}
}
