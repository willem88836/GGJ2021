using System;
using UnityEngine;

public class PlayerVisions : MonoBehaviour
{
    [SerializeField] private LayerMask floorMask;

    // Update is called once per frame
    private void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        Vector3 mouse = Input.mousePosition;
        Ray r = Camera.main.ScreenPointToRay(mouse);

        RaycastHit hitInfo; 
        if (Physics.Raycast(r, out hitInfo, float.PositiveInfinity, floorMask))
        {
            Vector3 delta = hitInfo.point - transform.position;
            float rot = (float)Math.Atan2(delta.x, delta.z);
            rot = rot * Mathf.Rad2Deg;
            Vector3 rotation = transform.rotation.eulerAngles;
            rotation.y = rot;
            Debug.DrawLine(hitInfo.point, hitInfo.point + Vector3.up, Color.red);
            Debug.Log(delta);
            transform.rotation = Quaternion.Euler(rotation);
        }
    }
}
