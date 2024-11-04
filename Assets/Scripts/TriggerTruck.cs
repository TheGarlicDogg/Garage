using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerTruck : MonoBehaviour
{
    // Start is called before the first frame update
    public LayerMask itemMask;
    private void OnTriggerEnter(Collider other)
    {
        if ((itemMask & (1 << other.gameObject.layer)) != 0)
        {
            ScoreManager.instance.AddScore();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if ((itemMask & (1 << other.gameObject.layer)) != 0)
        {
            ScoreManager.instance.DecreaseScore();
        }
    }
}
