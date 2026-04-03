using UnityEngine;

public class WallHit : MonoBehaviour
{
    //set up Hit counters
    public int MaxHits = 3;
    private int HitCount = 0;

    void OnColisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            HitCount++;

            Debug.Log("Wall hit: " + HitCount);

            if(HitCount >= MaxHits)
            {
                GameOver(); //will have to replace it with a gameover cutscene
            }
        }
    }

    void GameOver()
    {
        //Game over to put in with game over cut scene
    }
}
