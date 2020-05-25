using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Death : MonoBehaviour
{
    public Transform checkpoint;

    private float timer;
    public float timeToActivate;
    private bool on;

    public Light avatarLight;

    public PlayerMovement move;
    public MouseLook mouse;
    public AvatarLighting avatar;

    public Text deathText;
    public Image deathImage;
    Color tempColor;
    Color tempColorImage;

    public Renderer crystal;

    public string text;

    private void Start()
    {
        deathText.text = text;
    }
    void Update()
    {
        if (on)
        {
            Color finalValue = avatarLight.color * avatarLight.intensity / 2;
            crystal.material.SetColor("_EmissionColor", finalValue);
            timer += Time.deltaTime;
            avatarLight.range = avatarLight.intensity * 5;
            avatarLight.intensity -= 5f * Time.deltaTime;
            tempColor = deathText.color;
            tempColorImage = deathImage.color;
            if (tempColor.a < 1)
            {
                tempColor.a += 0.2f * Time.deltaTime;
                deathText.color = tempColor;
            }
            if (tempColorImage.a < 1)
            {
                tempColorImage.a += 0.5f * Time.deltaTime;
                deathImage.color = tempColorImage;
            }

        }
        if (timer > timeToActivate)
        {
            transform.position = checkpoint.position;
            move.enabled = true;
            mouse.enabled = true;
            avatar.enabled = true;
            timer = 0;
            on = false;
        }
        if(!on && tempColor.a > 0)
        {
            tempColor.a -= 1f * Time.deltaTime;
            deathText.color = tempColor;
        }
        if (!on && tempColorImage.a > 0)
        {
            tempColorImage.a -= 1f * Time.deltaTime;
            deathImage.color = tempColorImage;
        }
    }

    public void DeathReset()
    {
        move.enabled = false;
        mouse.enabled = false;
        avatar.enabled = false;
        on = true;
    }
}
