using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DestroyByContact : MonoBehaviour
{
    public GameObject explosion;
    public GameObject playerExplosion;
    public int scoreValue;
    private GameController gameController;
    private GCH gch;
    private Player1Controller p1c;
    private Player1Controller p1c2;

    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            GameObject gameControllerObject = GameObject.FindWithTag("GameController");
            if (gameControllerObject != null)
            {
                gameController = gameControllerObject.GetComponent<GameController>();
            }
            if (gameController == null)
            {
                Debug.Log("Cannot find 'GameController' Script");
            }
            GameObject playerControllerObject = GameObject.FindWithTag("Player");
            if (playerControllerObject != null)
            {
                p1c = playerControllerObject.GetComponent<Player1Controller>();
            }
        }
        else
        {
            GameObject gameControllerObject2 = GameObject.FindWithTag("GCH");
            if (gameControllerObject2 != null)
            {
                gch = gameControllerObject2.GetComponent<GCH>();
            }
            if (gch == null)
            {
                Debug.Log("Cannot find 'GCH' Script");
            }

            GameObject playerControllerObject2 = GameObject.FindWithTag("Player");
            if (playerControllerObject2 != null)
            {
                p1c2 = playerControllerObject2.GetComponent<Player1Controller>();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boundary") || other.CompareTag("Enemy") || other.CompareTag("PowerUp"))
        {
            return;
        }

        if (explosion != null)
        {
            Instantiate(explosion, transform.position, transform.rotation);
        }

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            if (gameController.score < 100)
            {
                if (other.tag == "Player" && this.tag != "PowerUp")
                {
                    Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
                    gameController.GameOver();
                    gameController.AddScore(scoreValue);
                    Destroy(other.gameObject);
                    Destroy(gameObject);

                }

                else if (other.tag == "Player" && this.tag == "PowerUp")
                {
                    p1c.power = 5;
                    Destroy(gameObject);
                }
                else
                {
                    gameController.AddScore(scoreValue);
                    Destroy(other.gameObject);
                    Destroy(gameObject);
                }
            }
        }
        else
        {
            if (gch.score < 100)
            {
                if (other.tag == "Player" && this.tag != "PowerUp")
                {
                    Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
                    gch.GameOver();
                    gch.AddScore(scoreValue);
                    Destroy(other.gameObject);
                    Destroy(gameObject);
                }
                
                else if (other.tag == "Player" && this.tag == "PowerUp")
                {
                    p1c2.power = 5;
                    Destroy(gameObject);
                }
                else
                {
                    gch.AddScore(scoreValue);
                    Destroy(other.gameObject);
                    Destroy(gameObject);
                }
            }
        }
    }
    
}
