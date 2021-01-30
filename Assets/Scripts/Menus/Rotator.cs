using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
	[SerializeField] float _speed = 1;


    void Update()
    {
		transform.Rotate(Vector3.up * Time.deltaTime * _speed);
    }
}
