using UnityEngine;

public class RotateAnimation : MonoBehaviour
{
    [SerializeField] 
    private float speed;
    [SerializeField]
    private Vector3 axisFactors = Vector3.forward;

    float randomOffset;

    // Start is called before the first frame update
    void Start()
    {
        transform.Rotate(axisFactors, Random.Range(0, 360));
        //transform.rotation = Quaternion.Euler(axisFactors * random);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(axisFactors, speed * Time.deltaTime);
    }
}
