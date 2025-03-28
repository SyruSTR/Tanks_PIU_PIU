﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitBar : MonoBehaviour {

    #region peremenya
    public float maxValue = 100;
    public Color color = Color.red;
    public int widht = 4;
    public Slider slider;
    public bool isRight;

    private static float current;
    #endregion

    void Start () {
        slider.fillRect.GetComponent<Image>().color = color;

        slider.maxValue = maxValue;
        slider.minValue = 0;
        current = maxValue;

        UpdateUI();
	}
	
    public static float currentValue
    {
        get { return current; }
    }

	void Update () {
        if (current < 0) current = 0;
        if (current > maxValue) current = maxValue;
        slider.value = current;
    }
    void UpdateUI()
    {
        RectTransform rect = slider.GetComponent<RectTransform>();

        int rectDeltaX = Screen.width / widht;
        float rectPosX = 0;

        if (isRight)
        {
            rectPosX = rect.position.x - (rectDeltaX - rect.sizeDelta.x) / 2;
            slider.direction = Slider.Direction.RightToLeft;
        }
        else
        {
            rectPosX = rect.position.x + (rectDeltaX - rect.sizeDelta.x) / 2;
            slider.direction = Slider.Direction.LeftToRight;
        }

        rect.sizeDelta = new Vector2(rectDeltaX, rect.sizeDelta.y);
        rect.position = new Vector3(rectPosX, rect.position.y, rect.position.z);
    }

    public static void AdjustCurentValue (float adjust)
    {
        current += adjust;
    }
}
