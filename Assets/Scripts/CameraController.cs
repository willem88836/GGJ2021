using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	[SerializeField] Transform _player;
	[SerializeField] Transform _sceneCenter;
	[SerializeField] float _strength;
	[SerializeField] float _zOffset;

	Vector3 _playerPos = new Vector3();

    void Start()
    {
        
    }

    void Update()
    {
		_playerPos = new Vector3(
			_player.position.x,
			_sceneCenter.position.y,
			_player.position.z) - _zOffset * Vector3.forward;

		transform.position =
			Vector3.Lerp(_sceneCenter.position, _playerPos, _strength);
    }
}
