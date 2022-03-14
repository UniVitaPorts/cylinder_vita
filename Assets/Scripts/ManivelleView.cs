using UnityEngine;

public class ManivelleView : MonoBehaviour
{
	private ManivelleController controller;

	private SpriteRenderer sr;

	private BoxCollider2D bc;

	public Sprite[] spriteList = new Sprite[8];

	public Sprite[] spriteListMan = new Sprite[8];

	public Vector2[] centerList = new Vector2[8];

	private int state;

	private bool autoMode = true;

	private bool move = true;

	private Color originalColor = Color.white;

	private Color mouseOverColor = Color.cyan;

	private bool dragging;

	private float distance;

	private Vector3 rotationPoint;

	private void Awake()
	{
		controller = GetComponent<ManivelleController>();
		sr = GetComponent<SpriteRenderer>();
		bc = GetComponent<BoxCollider2D>();
	}

	private void Start()
	{
		rotationPoint = GameObject.Find("RotationPoint").transform.position;
	}

	private void Update()
	{
		if (!autoMode && dragging && move)
		{
			Vector2 a = Camera.main.ScreenPointToRay(Input.mousePosition).GetPoint(distance);
			Vector2 vector = base.gameObject.transform.position;
			float num = Vector2.Distance(a, centerList[state] + vector);
			if (num > Vector2.Distance(a, centerList[Next()] + vector))
			{
				controller.Crank();
			}
			else if (num > Vector2.Distance(a, centerList[Previous()] + vector))
			{
				controller.ReverseCrank();
			}
		}
	}

	private int Next()
	{
		if (state == 7)
		{
			return 0;
		}
		return state + 1;
	}

	private int Previous()
	{
		if (state == 0)
		{
			return 7;
		}
		return state - 1;
	}

	public void OnMouseEnter()
	{
		if (move)
		{
			GetComponent<SpriteRenderer>().color = mouseOverColor;
		}
	}

	public void OnMouseExit()
	{
		if (move)
		{
			GetComponent<SpriteRenderer>().color = originalColor;
		}
	}

	public void OnMouseDown()
	{
		if (move)
		{
			if (!autoMode)
			{
				dragging = true;
				distance = Vector3.Distance(base.transform.position, Camera.main.transform.position);
			}
			else
			{
				GetComponent<SpriteRenderer>().color = originalColor;
				controller.AutoCrank();
			}
		}
	}

	public void OnMouseUp()
	{
		if (move && !autoMode)
		{
			dragging = false;
		}
	}

	public void SetMove(bool b)
	{
		move = b;
		dragging = false;
	}

	public void UpdateView(int state)
	{
		this.state = state;
		if (autoMode)
		{
			sr.sprite = spriteList[state];
		}
		else
		{
			sr.sprite = spriteListMan[state];
		}
		bc.offset = centerList[state];
	}

	public void SwitchMode()
	{
		autoMode = !autoMode;
	}
}
