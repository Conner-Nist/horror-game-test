using UnityEngine;

public class AnimationEventRelay : MonoBehaviour
{
    public EnemyKillSequence killSequence;

    public void GrabPlayer()
    {
        if (killSequence != null) 
        {
            killSequence.GrabPlayer();
        }
    }

    public void FinishKillSequence()
    {
        if (killSequence != null)
        {
            killSequence.FinishKillSequence();
        }
    }
}