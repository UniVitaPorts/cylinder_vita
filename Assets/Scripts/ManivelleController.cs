using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ManivelleController : MonoBehaviour
{
	private Manivelle model;

	private ManivelleView view;

	private PartitionController p_controller;

	private Illustration illustration;

	private int wait_final = 120;

	private int final_img = 1;

	private bool final;

	private bool end;

	private bool pause;

	private int wait;

	private int waitingTime = 8;

	private bool reset;

	private bool isPlaying;

	private bool auto;

	private bool autoMode = true;

	public Animator animator;

	public Button auto_button;

	public AudioClip badAnswer;

	private AudioSource source;

	private void Start()
	{
		model = GetComponent<Manivelle>();
		view = GetComponent<ManivelleView>();
		p_controller = Object.FindObjectOfType<PartitionController>();
		illustration = Object.FindObjectOfType<Illustration>();
		illustration.SetTrans(p_controller.GetBorneLim());
		illustration.SetWait(waitingTime);
		model.SetMaxCrank(p_controller.GetBorneLim());
		source = GameObject.Find("Main Camera").GetComponent<AudioSource>();
		auto_button.gameObject.SetActive(false);
	}

	private void Update()
	{
		if (wait > 0)
		{
			wait--;
		}
		if (wait < 0)
		{
			wait = 0;
		}
		if (reset)
		{
			ReverseCrank();
		}
		else if (autoMode && auto)
		{
			Crank();
		}
		else if (!final && GameManager.Instance().IsEnd())
		{
			final = true;
			Dent.SetMove(false);
			view.SetMove(false);
		}
		else if (final && wait_final != 0)
		{
			wait_final--;
		}
		else if (final && final_img != 0 && !end)
		{
			ReverseCrank();
		}
		else if (!end && final_img == 0)
		{
			end = true;
			Dent.SetMove(true);
			view.SetMove(true);
			animator.SetTrigger("End");
			auto_button.gameObject.SetActive(true);
		}
	}

	private IEnumerator waitForError()
	{
		pause = true;
		source.Stop();
		source.PlayOneShot(badAnswer, 1f);
		while (source.isPlaying)
		{
			yield return null;
		}
		pause = false;
	}

	public void Crank()
	{
		if (wait != 0 || pause)
		{
			return;
		}
		if (model.IsBeginning())
		{
			if (!end)
			{
				Dent.SetMove(false);
				if (autoMode)
				{
					view.SetMove(false);
				}
			}
			isPlaying = true;
		}
		else if (model.IsLimitPoint() && !end && !GameManager.Instance().Proceed(p_controller.GetDents()))
		{
			StartCoroutine(waitForError());
			ResetCrank();
			return;
		}
		model.Crank();
		view.UpdateView(model.GetState());
		p_controller.Read();
		if (!end)
		{
			illustration.Read();
		}
		if (model.IsBeginning())
		{
			isPlaying = false;
			GameManager.Instance().NextLevel();
			Dent.SetMove(true);
			auto = false;
			if (autoMode)
			{
				view.SetMove(true);
			}
		}
		wait = waitingTime;
	}

	public void ReverseCrank()
	{
		if (wait != 0 || pause || (model.IsBeginning() && !end && (!final || end)))
		{
			return;
		}
		model.ReverseCrank();
		view.UpdateView(model.GetState());
		p_controller.ReverseRead();
		if (!end)
		{
			if (!final)
			{
				illustration.ReverseRead();
			}
			else
			{
				illustration.FastReverseRead();
			}
		}
		if (model.IsBeginning())
		{
			if (!final || end)
			{
				isPlaying = false;
				Dent.SetMove(true);
				view.SetMove(true);
				reset = false;
				auto = false;
			}
			else if (!end && final_img != 0)
			{
				final_img--;
			}
		}
		wait = waitingTime;
	}

	public void ResetCrank()
	{
		view.SetMove(false);
		reset = true;
	}

	public void AutoCrank()
	{
		view.SetMove(false);
		auto = true;
	}

	public void SwitchMode()
	{
		autoMode = !autoMode;
		view.SwitchMode();
		auto = false;
		view.SetMove(true);
		view.UpdateView(model.GetState());
	}

	public void SetSpeed(Slider slider)
	{
		if (!(illustration == null))
		{
			int num = 10 - (int)slider.value;
			ChangeWait(num);
		}
	}

	public void ChangeWait(int wait)
	{
		waitingTime = wait;
		illustration.SetWait(waitingTime);
	}
}
