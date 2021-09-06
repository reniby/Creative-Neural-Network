using UnityEngine;

using System.Collections;

using System.IO;

using System.Text;
using UnityEngine.SceneManagement;

public class AudioTest : MonoBehaviour
{
	public float coreNum = 0f; //01, 23, 45, 678
	public float currRoom = 1f; //0e, 1b, 2p, 3c
	public float deaths = 0f; //2, 4, 8, 16
	public float buns = 0f; //0, 1, 2, 3
	public float peratio = 1f; //<0.5, 0.5-1, 1.1-2, >2
	public float villages = 0f; //1, 2, 4, 8
	public float[] inputs;

	private float wait = 15f;
	private float next = 0f;
	private float currIndex;

	public int pcores = 1;
	public int ecores = 1;
	public int numHelped = 0;

	private bool flip = false;

	private void Awake()
	{
		DontDestroyOnLoad(this.gameObject);
	}

	private void OnLevelWasLoaded()
	{
		if (SceneManager.GetActiveScene().name == "MainGame") { villages += 1; }

		coreNum = 0;
		currRoom = 1;
		deaths = 0;
	}

	void Start ()
	{
		KalimbaPd.Init();
		
		KalimbaPd.OpenFile("connected.pd", "pd");

		inputs = new float[] {coreNum, currRoom, deaths, buns, peratio, villages};
	}

	private void Update()
	{
		if (Time.time > next)
		{
			next = Time.time + wait;

			if (flip)
			{
				for (int i = 0; i < 6; i++)
				{
					KalimbaPd.SendFloat(i, "indexValue");
					KalimbaPd.SendFloat(inputs[i], "inputValue");
				}
				flip = false;
			}
			else
			{
				KalimbaPd.SendBangToReceiver("retrain");
				flip = true;
			}
			
		}

		if (coreNum < 2) inputs[0] = 0f;
		else if (coreNum < 4) inputs[0] = 0.33f;
		else if (coreNum < 6) inputs[0] = 0.66f;
		else inputs[0] = 1f;

		inputs[1] = (currRoom - 1) / 3;

		if (deaths < 2) inputs[2] = 0f;
		else if (deaths < 4) inputs[2] = 0.33f;
		else if (deaths < 8) inputs[2] = 0.66f;
		else inputs[2] = 1f;

		if (buns < 1) inputs[3] = 0f;
		else if (buns < 2) inputs[3] = 0.33f;
		else if (buns < 3) inputs[3] = 0.66f;
		else inputs[3] = 1f;

		if (peratio < 0.5) inputs[4] = 0f;
		else if (peratio < 1) inputs[4] = 0.33f;
		else if (peratio < 2) inputs[4] = 0.66f;
		else inputs[4] = 1f;

		if (villages < 1) inputs[5] = 0f;
		else if (villages < 2) inputs[5] = 0.33f;
		else if (villages < 4) inputs[5] = 0.66f;
		else inputs[5] = 1f;

		peratio = pcores / ecores;

		if (villages != 0)
		{
			buns = numHelped / villages;
		}

	}

	///////////////////////////////////////////

	public void coreUpdate(bool pcore)
	{
		coreNum += 1;
		if (pcore) { pcores += 1; }
		else { ecores += 1; }
	}

	public void died()
	{
		deaths += 1;
	}

	public void bun()
	{
		numHelped += 1;
	}

	public void room(int type)
	{
		currRoom = type;
	}

	///////////////////////////////////////////

}
