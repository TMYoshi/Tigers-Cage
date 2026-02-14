using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class HeartbeatMinigame : MonoBehaviour
{

	[Header("Essentials")]
	[SerializeField] List<GameObject> gameObjectsToShow;

	[SerializeField] Slider safeSlider;
	[SerializeField] RectTransform safeZoneTransform;
	[SerializeField] Slider heartSlider;
	[SerializeField] RectTransform heartTransform;
	[SerializeField] RectTransform safeZone;

	const float durationOffset = 0.001f;

	[Header("Settings")]
	[SerializeField] float initSafe;
	[SerializeField] float safeTimerInit;
	[SerializeField] float loseMinigameTimer;
	[SerializeField] float winMinigameTimer;
	[Tooltip("change this for the init duration given")]
	[SerializeField] float safeZoneTimerCurrent;
	[Tooltip("do not set this to anything it's only for viewing purposes")]
	[SerializeField] float safeZoneVelocity;
	[SerializeField] float safeZoneAcceleration;
	[SerializeField, Range(0f, 1f)] float DirectionalChangeProbability;

	[Header("Heart")]
	[SerializeField] float heartDropRate;
	[SerializeField] float heartIncrease;

	[Header("Events")]
	[SerializeField] UnityEvent LoseEvent;
	[SerializeField] UnityEvent SurviveEvent;

	void Start()
	{
		safeZoneVelocity = 0;
		//tad bit dangerous code (startin to reach the int overflow)
		safeZoneAcceleration *= durationOffset * 0.1f;
		StartHeartBeatMinigame();
	}

	void StartHeartBeatMinigame()
	{
		for(int I = 0; I < gameObjectsToShow.Count; I++)
			gameObjectsToShow[I].SetActive(true);

		StartCoroutine(StartSafeZoneMoves());
	}

	void EndMinigame()
	{
		for(int I = 0; I < gameObjectsToShow.Count; I++)
			gameObjectsToShow[I].SetActive(false);
		this.gameObject.SetActive(false);
	}

	bool _startDanger = false;
	
	void Update()
	{
		if(!_startDanger) return;

		if (Input.GetKeyDown(KeyCode.F))
			heartSlider.value -= heartIncrease;

		if (AreTouching(safeZoneTransform, heartTransform))
		{
			safeZoneTimerCurrent += Time.deltaTime;

			if(winMinigameTimer < safeZoneTimerCurrent)
				SurviveEvent.Invoke();
		}
		else
		{
			if(safeZoneTimerCurrent >= 0f)
				safeZoneTimerCurrent -= Time.deltaTime;
			else if(safeZoneTimerCurrent < 0f)
				LoseEvent.Invoke();
		}
	}

	public bool AreTouching(RectTransform a, RectTransform b)
	{
		Rect rectA = GetWorldRect(a);
		Rect rectB = GetWorldRect(b);

		return rectA.Overlaps(rectB);
	}

	Rect GetWorldRect(RectTransform rt)
	{
		Vector3[] corners = new Vector3[4];
		rt.GetWorldCorners(corners);

		Vector3 bottomLeft = corners[0];
		Vector3 topRight = corners[2];

		return new Rect(
			bottomLeft.x,
			bottomLeft.y,
			topRight.x - bottomLeft.x,
			topRight.y - bottomLeft.y
		);
	}

	IEnumerator StartSafeZoneMoves()
	{
		if (!_startDanger)
		{
			yield return new WaitForSeconds(safeTimerInit);
			_startDanger = true;
		}

		float direction = 1f;
		float AccDirection = 1f;

		while (_startDanger)
		{
			if (Random.value < DirectionalChangeProbability * durationOffset * 10)
				AccDirection *= -1f;

			if (safeSlider.value >= 0.99f)
			{
				AccDirection = -1f;
				direction = -1f;
			}
			else if (safeSlider.value <= 0.01f)
			{
				AccDirection = 1f;
				direction = 1f;
			}
			
			safeZoneVelocity += safeZoneAcceleration * AccDirection;
			safeSlider.value += safeZoneVelocity * direction;
			safeSlider.value = Mathf.Clamp01(safeSlider.value);

			yield return new WaitForSeconds(durationOffset);
		}
	}

	void FixedUpdate()
	{
		if (!_startDanger)
			return;
		heartSlider.value += heartDropRate * Time.fixedDeltaTime;
	}

	public void Win()
	{
		Debug.Log("<color=yellow>Win</color>");
		EndMinigame();
	}

	public void lose()
	{
		Debug.Log("<color=red>lose</color>");
		EndMinigame();
	}
}
