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

	void Start()
	{
		_startPosition = transform.position;
		_yeetPosition = transform.position + Vector3.right * _moveRange;
	}

	void Update()
    {
		if (Input.GetKeyDown(KeyCode.Space)) // removve when its called from another script
		{
			StartNextRound();
		}

		if (timer >= 0)
			timer += Time.deltaTime;

		MoveIn();
		YeetPhase();
		MoveOut();
	}

	public void StartNextRound()
	{
		timer = 0;
	}

	void MoveIn()
	{
		if (timer >= 0 && timer < _moveTime)
		{
			transform.position = Vector3.Lerp(_startPosition, _yeetPosition, timer);
		}
	}

	void YeetPhase()
	{
		if (timer >= _moveTime &&
			timer < _moveTime + _yeetTime)
		{
			_nextYeet -= Time.deltaTime;

			if (_nextYeet <= 0)
			{
				_nextYeet = 0.2f;
				Yeet();
			}
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
		if (timer > _moveTime + _yeetTime &&
			timer < _moveTime + _yeetTime + _moveTime)
		{
			transform.position = Vector3.Lerp(_yeetPosition, _startPosition, timer - _moveTime - _yeetTime);
		}

		if (timer >= _moveTime + _yeetTime + _moveTime)
			timer = -1;
	}

	
}
