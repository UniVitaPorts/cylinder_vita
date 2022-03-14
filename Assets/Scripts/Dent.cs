using UnityEngine;

public class Dent : MonoBehaviour
{
	public EmplacementDent emp;

	public AudioClip sound;

	private AudioSource source;

	public int id;

	private static bool move = true;

	private Color mouseOverColor = Color.yellow;

	private Color originalColor = Color.white;

	private bool dragging;

	private float distance;

	private Vector3 initialPos;

	private void Start()
	{
		if (emp == null)
		{
			emp = GameObject.Find("EmplacementDefaut").GetComponent<EmplacementDent>();
		}
		NewPosition(emp.GetPos());
		emp.SetDent(this);
		source = GameObject.Find("Main Camera").GetComponent<AudioSource>();
	}

	private void Update()
	{
		if (dragging && move)
		{
			Vector3 point = Camera.main.ScreenPointToRay(Input.mousePosition).GetPoint(distance);
			base.transform.position = point;
		}
	}

	public static void SetMove(bool b)
	{
		move = b;
	}

	public void OnMouseEnter()
	{
		if (move)
		{
			GetComponent<Renderer>().material.color = mouseOverColor;
		}
	}

	public void OnMouseExit()
	{
		if (move)
		{
			GetComponent<Renderer>().material.color = originalColor;
		}
	}

	public void OnMouseDown()
	{
		if (move)
		{
			dragging = true;
			distance = Vector3.Distance(base.transform.position, Camera.main.transform.position);
			EmplacementDent[] array = Object.FindObjectsOfType<EmplacementDent>();
			for (int i = 0; i < array.Length; i++)
			{
				array[i].ActiveDrag();
			}
			Dent[] array2 = Object.FindObjectsOfType<Dent>();
			for (int i = 0; i < array2.Length; i++)
			{
				array2[i].gameObject.layer = 2;
			}
		}
	}

	public void OnMouseUp()
	{
		if (!move)
		{
			return;
		}
		dragging = false;
		bool flag = false;
		EmplacementDent[] array = Object.FindObjectsOfType<EmplacementDent>();
		foreach (EmplacementDent emplacementDent in array)
		{
			emplacementDent.DisableDrag();
			if (emplacementDent.IsSelected())
			{
				SwitchEmp(emplacementDent);
				flag = true;
			}
		}
		if (!flag)
		{
			InitialPosition();
		}
		Dent[] array2 = Object.FindObjectsOfType<Dent>();
		for (int i = 0; i < array2.Length; i++)
		{
			array2[i].gameObject.layer = 0;
		}
	}

	public void InitialPosition()
	{
		base.gameObject.transform.position = initialPos;
	}

	public void NewPosition(Vector3 pos)
	{
		base.gameObject.transform.position = pos;
		initialPos = pos;
	}

	public void SwitchEmp(EmplacementDent emp2)
	{
		emp.SwitchDent(emp2);
	}

	public void SetEmp(EmplacementDent emp)
	{
		this.emp = emp;
		NewPosition(emp.GetPos());
	}

	public EmplacementDent GetEmp()
	{
		return emp;
	}

	public void Play()
	{
		source.PlayOneShot(sound, 1f);
	}

	public int GetId()
	{
		return id;
	}
}
