using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndDoor : MonoBehaviour
{
    public EndCrystal hero;
    public EndCrystal pitch;

    private bool open;

    public GameObject door;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(hero.enable && pitch.enable)
        {
            open = true;
        }

        if(open)
        {
            door.transform.position += new Vector3(0, -0.05f, 0);
        }
    }
}
