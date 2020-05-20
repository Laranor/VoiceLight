using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadLevel : MonoBehaviour
{
    LightScript LS;
    Light avatarLight;
    private float timer;
    public float timeToActivate;
    private bool on;
    private void Update()
    {
        if(on)
        {
            timer += Time.deltaTime;
            avatarLight.range = avatarLight.intensity * 5;
            avatarLight.intensity -= 1.5f * Time.deltaTime;
        }
        if(timer > timeToActivate)
        {
            SceneManager.LoadScene("Main");
        }
    }
    private void Start()
    {
        LS = GetComponent<LightScript>();
        avatarLight = GetComponent<Light>();
    }
    public void OnClick()
    {
        Destroy(LS);
        on = true;
    }
}
