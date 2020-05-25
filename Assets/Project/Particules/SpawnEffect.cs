﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEffect : MonoBehaviour {

    public float spawnEffectTime = 2;
    public float pause = 1;
    public AnimationCurve fadeIn;

    float timer = 0;
    Renderer _renderer;

    int shaderProperty;

    public Waypoints way;

    public AnimPorteFinale cinematic;

    private bool enable = true;
	void Start ()
    {
        shaderProperty = Shader.PropertyToID("_cutoff");
        _renderer = GetComponent<Renderer>();


    }
	
	void Update ()
    {
        if(way.open)
        {
            if (timer < spawnEffectTime + pause)
            {
                timer += Time.deltaTime;
            }
            else
            {
                timer = 0;
            }
            _renderer.material.SetFloat(shaderProperty, fadeIn.Evaluate(Mathf.InverseLerp(0, spawnEffectTime, timer)));
        }
        if (timer > spawnEffectTime + 1 && enable)
        {
            cinematic.cinematic = true;
            cinematic.golf = true;
            enable = false;
        }
    }
}
