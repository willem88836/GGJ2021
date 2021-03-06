﻿using UnityEngine;

public class OscillatorAnimation : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] private float speed;
    [SerializeField] private Transform rakeModel;


    [SerializeField] private bool isAnimating;
    [SerializeField] private float progressOffset;
    private float progress;

    private Vector3 pivot;
    private Quaternion rot;

    private void Start()
    {
        this.pivot = this.transform.position;
        this.rot = this.transform.rotation;
    }

    
    private void Update()
    {
        if (!isAnimating)
            return;

        progress += Time.deltaTime * speed;
        UpdateAnimation();
    }

    private void UpdateAnimation()
    {
        float c = Mathf.Cos(progress) * radius;
        float s = Mathf.Sin(progress) * radius;
        Vector3 rot = new Vector3(0, s, c);
        rakeModel.localPosition = rot;
    }

    public void ToggleAnimation(bool toggle)
    {
        if (isAnimating != toggle)
        {
            progress = progressOffset;
            isAnimating = toggle;
            UpdateAnimation();
        }
    }
}
