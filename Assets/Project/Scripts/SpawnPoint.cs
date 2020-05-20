using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public Death death;
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Avatar")
        {
            death.checkpoint = gameObject.transform;
        }
    }
}
