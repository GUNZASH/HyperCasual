﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGCtrl : MonoBehaviour
{
    public float Speed;

    private float offset;
    private Material mat;

    private void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    void Update()
    {
        offset -= (Time.deltaTime * Speed) / 10;
        
        mat.SetTextureOffset("_MainTex", new Vector2(0, offset));
    }
}