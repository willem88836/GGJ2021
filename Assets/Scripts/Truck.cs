using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Truck : MonoBehaviour
{
	[SerializeField] float _roundDuration; // is probably in a gamemanager or something
	[SerializeField] GameObject _post; // get from pool
	[SerializeField] Vector3 _spawnLocation;
	[Space]

	[SerializeField] float _moveTime;
	[SerializeField] float _moveRange;

	Vector3 _startPosition;
	Vector3 _yeetPosition;

	[Space]
	[SerializeField] float _yeetTime;
	[SerializeField] float _yeetSpread;
	[SerializeField] float _yeetpowerMin;
	[SerializeField] float _yeetpowerMax;

	float _nextYeet = 0.2f; // is probably in a gamemanager or something

	float timer = -1;
	int _currentPhase = 0;

	void Start()
	{
		_startPosition = transform.position;
		_yeetPosition = transform.position + Vector3.right * _moveRange;

		_moveTime = 1 / _moveTime;
	}

	void Update()
    {
		if (Input.GetKeyDown(KeyCode.Space)) // removve when its called from another script
		{
			StartNextRound();
		}

		if (_currentPhase == 1)
			MoveIn();
		if (_currentPhase == 2)
			YeetPhase();
		if (_currentPhase == 3)
			MoveOut();
	}

	public void StartNextRound()
	{
		_currentPhase++;
	}

	void MoveIn()
	{
		timer += Time.deltaTime * _moveTime;
		transform.position = Vector3.Lerp(_startPosition, _yeetPosition, timer);

		if (timer > 1)
		{
			timer = 0;
			_currentPhase++;
		}
	}

	void YeetPhase()
	{
		_nextYeet -= Time.deltaTime;

		if (_nextYeet <= 0)
		{
			_nextYeet = 0.2f;
			Yeet();
		}

		timer += Time.deltaTime;
		if (timer > _yeetTime)
		{
			timer = 0;
			_currentPhase++;
		}
	}

	void Yeet()
	{
		Vector3 direction = new Vector3();
		direction = Vector3.right;

		float randomUp = Random.Range(0, _yeetSpread);
		direction += Vector3.up * randomUp;

		float randomSide = Random.Range(-_yeetSpread, _yeetSpread);
		direction += Vector3.forward * randomSide;

		// TODO replace with pooling
		GameObject go = Instantiate(_post, _spawnLocation, Quaternion.identity);
		Rigidbody rb = go.GetComponent<Rigidbody>();
		rb.AddForce(direction * 10, ForceMode.Impulse);
	}

	void MoveOut()
	{
		timer += Time.deltaTime * _moveTime;
		transform.position = Vector3.Lerp(_yeetPosition, _startPosition, timer);

		if (timer > 1)
		{
			timer = 0;
			_currentPhase = 0;
		}
	}
}
