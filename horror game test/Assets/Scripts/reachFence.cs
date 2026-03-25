using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class reachFence : MonoBehaviour
{

    public Transform player;
    public float finishDistance;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer <= finishDistance)
        {
            SceneManager.LoadScene("youWin");
        }
    }
}
