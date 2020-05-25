using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Intro : MonoBehaviour
{
    public Image blackScreen;
    Color tempColorImage;

    public float fadingTime;
    private bool fade;

    private float timer;
    public float timeToFade;

    public Text introText;

    public PlayerMovement move;
    public MouseLook mouse;
    public AvatarLighting avatar;

    private void Start()
    {
        move.enabled = false;
        mouse.enabled = false;
    }

    void Update()
    {
        if(!fade)
            timer += Time.deltaTime;
        if (timer > timeToFade)
            fade = true;
        if(fade)
        {
            tempColorImage = blackScreen.color;
            if (tempColorImage.a > 0)
            {
                tempColorImage.a -= 1 / fadingTime * Time.deltaTime;
                blackScreen.color = tempColorImage;
            }
            if(tempColorImage.a <= 0)
            {
                move.enabled = true;
                mouse.enabled = true;
                Destroy(this);
            }
        }

    }
}
