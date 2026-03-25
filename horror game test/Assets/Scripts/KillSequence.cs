using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyKillSequence : MonoBehaviour
{
    [Header("References")]
    public Animator enemyAnimator;
    public Transform stabPoint;
    public Transform facePoint;
    public Transform player;

    public Transform playerCamera;

    // private TankControls tankControls;

    [Header("Scene")]
    public string gameOverSceneName = "youDied";

    private bool killSequenceActive;
    

    void Awake()
    {
        // tankControls = player.GetComponent<TankControls>();
    }

    public void StartKillSequence()
    {
        
        if (killSequenceActive || player == null || stabPoint == null) return;
        killSequenceActive = true;

        // Stop player control
        // tankControls.KillPlayer();

        // Face the enemy
        Vector3 dir = transform.position - player.position;
        dir.y = 0f;
        if (dir.sqrMagnitude > 0.001f)
        {
            player.rotation = Quaternion.LookRotation(dir.normalized);
        }

       
    }

    // Called by an Animation Event on the enemy kill clip
    public void GrabPlayer()
    {
        if (player == null || stabPoint == null) return;

        playerCamera.SetParent(stabPoint, false);
        playerCamera.localPosition = new Vector3(0, .1f, 0);;
        playerCamera.localRotation = Quaternion.Euler(90f, 0f, 0f);
        playerCamera.LookAt(facePoint);
    }

    // Called by another Animation Event near the end of the clip
    public void FinishKillSequence()
    {
        SceneManager.LoadScene(gameOverSceneName);
    }
}