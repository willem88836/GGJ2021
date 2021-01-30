using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Hatch : MonoBehaviour
{
	[SerializeField] Transform _leftHatch;
	[SerializeField] Transform _rightHatch;

	[SerializeField] float _openTime;
	[SerializeField] float _waitTime;

	[SerializeField] float _hatchType;
	[SerializeField] HatchPit _pit; 

	float _hatchAngle = 0;
	float _timer = 0;

	int _currentPhase = 0;

	LevelManager _manager;

	public void setLevelManager(LevelManager manager)
	{
		_manager = manager;
	}

	void Start()
    {
		_openTime = 1 / _openTime;
		_pit.SetHatch(this);
	}

	void Update()
    {
		// debug remove later
		if (Input.GetKeyDown(KeyCode.Q))
		{
			StartHatches();
		}
		// debug remove later

		MoveHatches();

		if (_currentPhase == 1)
			Open();
		if (_currentPhase == 2)
			Hold();
		if (_currentPhase == 3)
			Close();
	}

	public void OnObjectCaught(IObjectPoolable poolableObject)
	{
		// TODO: something with colors and types.
		_manager.AddScore(1);
	}

	public void StartHatches()
	{
		_currentPhase++;
	}

	void MoveHatches()
	{
		_leftHatch.rotation = Quaternion.Euler(
			_leftHatch.eulerAngles.x,
			_leftHatch.eulerAngles.y,
			-_hatchAngle);

		_rightHatch.rotation = Quaternion.Euler(
			_rightHatch.eulerAngles.x,
			_rightHatch.eulerAngles.y,
			_hatchAngle);
	}

	void Open()
	{
		_timer += Time.deltaTime * _openTime;
		_hatchAngle = Mathf.Lerp(0, 90, _timer);

		if (_timer > 1)
		{
			_timer = 0;
			_currentPhase++;
		}
	}

	void Hold()
	{
		_timer += Time.deltaTime;

		if (_timer > _waitTime)
		{
			_timer = 0;
			_currentPhase++;
		}
	}

	void Close()
	{
		_timer += Time.deltaTime * _openTime;
		_hatchAngle = Mathf.Lerp(90, 0, _timer);

		if (_timer > 1)
		{
			_timer = 0;
			_currentPhase = 0;
		}
	}
}
