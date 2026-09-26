
using UnityEngine;

public class Mazewin : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collided with: " + collision.gameObject.name);
        Debug.Log("Collided with tag: " + collision.gameObject.tag);
        if (collision.CompareTag("Finish"))
        {
            WinGame();
        }
    }


    private void WinGame()
    {
        Debug.Log("You win!");
    }
}
