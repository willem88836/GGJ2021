using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField]
    OscillatorAnimation handBobber;

    [SerializeField]
    OscillatorAnimation leftFeetBobber;

    [SerializeField]
    OscillatorAnimation rightFeetBobber;

    public void Move()
	{
        handBobber.ToggleAnimation(true);
        leftFeetBobber.ToggleAnimation(true);
        rightFeetBobber.ToggleAnimation(true);
    }

    public void Idle()
	{
        handBobber.ToggleAnimation(false);
        leftFeetBobber.ToggleAnimation(false);
        rightFeetBobber.ToggleAnimation(false);
    }
}
