using UnityEngine;

public class Truck : MonoBehaviour
{
	[SerializeField]
	private ObjectSpawner _spawner;
	[SerializeField] Transform _spawnLocation;
	[SerializeField] ObjectPool _boxPool;
	[SerializeField] ObjectPool _mailPool;
	[Space]

	[SerializeField] float _moveTime;
	[SerializeField] float _moveRange;

	Vector3 _startPosition;
	Vector3 _yeetPosition;

	[Space]
	[SerializeField] float _yeetInterval;
	[SerializeField] float _yeetSpread;
	[SerializeField] float _yeetpowerMin;
	[SerializeField] float _yeetpowerMax;

	float _nextYeet = 0.2f; // is probably in a gamemanager or something

	float timer = -1;
	int _currentPhase = 0;
	SpawnConfig _spawnConfig;
	float _realYeetTime;
	float _yeeted;

	bool startedYeet = false;

	void Start()
	{
		_startPosition = transform.position;
		_yeetPosition = transform.position + Vector3.right * _moveRange;

		_moveTime = 1 / _moveTime;
	}

	void Update()
    {
		if (_currentPhase == 1)
			MoveIn();
		if (_currentPhase == 2)
			YeetPhase();
		if (_currentPhase == 3)
			MoveOut();
	}

	public void StartNextRound(SpawnConfig spawnConfig)
	{
		_spawnConfig = spawnConfig;
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

			_realYeetTime = _yeetInterval * (_spawnConfig.letterCount + _spawnConfig.packageCount);
			_yeeted = 0;
		}
	}

	void YeetPhase()
	{
		if (!startedYeet)
		{
			startedYeet = true;
			StartCoroutine(_spawner.StartSpawnSequence());
		}

		if (!_spawner.IsSpawning)
		{
			timer = 0;
			_currentPhase++;
			startedYeet = false;
		}

		/*
		_nextYeet -= Time.deltaTime;

		if (_nextYeet <= 0)
		{
			_nextYeet = _yeetInterval;

			if(_yeeted < _spawnConfig.letterCount)
			{
				Yeet(_mailPool);
			}
			else
			{
				Yeet(_boxPool);
			}

			_yeeted++;
			
			if(_yeeted > (_spawnConfig.letterCount + _spawnConfig.packageCount))
			{
				timer = 0;
				_currentPhase++;
			}
		}

		timer += Time.deltaTime;
		*/
	}

	void Yeet(ObjectPool pool)
	{
		Vector3 direction = Vector3.right;

		float randomUp = Random.Range(0, _yeetSpread);
		direction += Vector3.up * randomUp;

		float randomSide = Random.Range(-_yeetSpread, _yeetSpread);
		direction += Vector3.forward * randomSide;

		IObjectPoolable op = pool.GetAvailableObject();
		op.Activate();
		GameObject go = op.GetGameObject();
		go.transform.position = _spawnLocation.position;
		Rigidbody rb = go.GetComponent<Rigidbody>();
		float randomPower = Random.Range(_yeetpowerMin, _yeetpowerMax);
		rb.AddForce(direction * randomPower, ForceMode.Impulse);
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
