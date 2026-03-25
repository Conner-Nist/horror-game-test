using UnityEngine;
using UnityEngine.SceneManagement;

public class GateLeave : MonoBehaviour
{
    
    public float leavedistance;
    public Transform player;
    public string winSceneName = "YouWin";

    void Start()
    {
        
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= leavedistance)
        {
            SceneManager.LoadScene(winSceneName);
            return;
        }

    }

}
