using System;
using UnityEngine;

public class PlayerVisions : MonoBehaviour
{
    [SerializeField] private LayerMask floorMask;

    private Vector3 mouseWorldPoint; 

    public Vector3 GetMouseWorldPoint()
    {
        return mouseWorldPoint;
    }

    // Update is called once per frame
    private void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        mouseWorldPoint = Input.mousePosition;
        Ray r = Camera.main.ScreenPointToRay(mouseWorldPoint);

        RaycastHit hitInfo; 
        if (Physics.Raycast(r, out hitInfo, float.PositiveInfinity, floorMask))
        {
            mouseWorldPoint = hitInfo.point;
            Vector3 delta = mouseWorldPoint - transform.position;
            float rot = (float)Math.Atan2(delta.x, delta.z);
            rot = rot * Mathf.Rad2Deg;
            Vector3 rotation = transform.rotation.eulerAngles;
            rotation.y = rot;
            transform.rotation = Quaternion.Euler(rotation);
        }
    }
}
