using UnityEngine;

public class EmplacementDent : MonoBehaviour
{
	private Dent dent;

	private Renderer r;

	private Color activeDragColor = Color.cyan;

	private Color mouseOverColor = Color.blue;

	private Color originalColor = Color.white;

	private bool dragging;

	private bool selected;

	private float distance;

	private Vector3 posDent;

	private void Awake()
	{
		r = GetComponent<Renderer>();
		posDent = GetComponent<BoxCollider2D>().bounds.center;
		base.gameObject.layer = 2;
	}

	private void Update()
	{
	}

	private void OnMouseEnter()
	{
		if (dragging)
		{
			selected = true;
			r.material.color = mouseOverColor;
		}
	}

	private void OnMouseExit()
	{
		if (dragging)
		{
			selected = false;
			r.material.color = activeDragColor;
		}
	}

	public void ActiveDrag()
	{
		dragging = true;
		base.gameObject.layer = 0;
		r.material.color = activeDragColor;
		r.sortingOrder = 2;
	}

	public void DisableDrag()
	{
		dragging = false;
		base.gameObject.layer = 2;
		r.material.color = originalColor;
		r.sortingOrder = 1;
	}

	public void SwitchDent(EmplacementDent empTarget)
	{
		empTarget.Unselect();
		Dent dent = empTarget.GetDent();
		empTarget.SetDent(this.dent);
		this.dent.SetEmp(empTarget);
		SetDent(dent);
		if (dent != null)
		{
			dent.SetEmp(this);
		}
	}

	public void Unselect()
	{
		selected = false;
	}

	public bool IsSelected()
	{
		return selected;
	}

	public void SetDent(Dent dent)
	{
		this.dent = dent;
	}

	public Dent GetDent()
	{
		return dent;
	}

	public Vector3 GetPos()
	{
		return posDent;
	}

	public void Play()
	{
		if (dent != null)
		{
			dent.Play();
		}
	}

	public int GetIdDent()
	{
		if (dent == null)
		{
			return 0;
		}
		return dent.GetId();
	}
}
