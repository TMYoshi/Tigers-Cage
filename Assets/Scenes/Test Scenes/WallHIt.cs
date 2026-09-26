using UnityEngine;

public class WallHit : MonoBehaviour
{
    //set up Hit counters
    public int MaxHits = 3;
    public float HitCoolDown = 0.5f;

    private int HitCount = 0;
    private float LastHitTime = -9999f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Wall"))
        {
            return;
        }
        
    
        if(Time.time - LastHitTime < HitCoolDown)
        {
            return;//Ignore hits during cooldown
        }

        LastHitTime = Time.time;
        HitCount++;

        Debug.Log("Player hit the wall:" + HitCount + " hits");

        if(HitCount >= MaxHits)
        {
            GameOver();
        }

        void GameOver()
        {
            Debug.Log("Game Over! Player hit the max count.");
            
        }
    }


}
