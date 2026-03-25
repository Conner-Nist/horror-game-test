using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCharacter : MonoBehaviour
{

    private int health;

    public Vector3 playerPosition;

    // Start is called before the first frame update
    void Start()
    {
        health = 20;
    }

    /// MODIFICATION!!!!
    /// I added a game over sequence
    /// I wasn't entirely sure what to do since I barely understand the hud so it just writes
    /// "YOU DIED" and the player ascends to the heavens
    /// you can also press E to restart the game
    void Update()
    {
        /// oh yeah almost forgot BUG FIX!!!!
        /// negative health doesn't exist anymore
        if (health <= 0)
        {
            health = 0;

            transform.Translate(0, 400 * Time.deltaTime, 0);

            if(Input.GetKeyDown(KeyCode.E))
            {
                SceneManager.LoadScene("SampleScene");
            }

        }
    }

    public void Hurt(int damage)
    {
        health -= damage;
        Debug.Log($"Health: {health}");
    }

    /// MODIFICATION!!!!
    /// just like the thing with line of sight I had to make another script for the hud
    /// to tell you that you died
    void OnGUI()
    {
        if (health != 0)
        {
            return;
        }
        float posX = Screen.width / 2f;
        float posY = Screen.height / 2f;

        GUI.Label(new Rect(posX, posY - 30, 400, 36), "YOU DIED");
        GUI.Label(new Rect(posX, posY + 30, 400, 36), "Press E to restart");
    }

}
