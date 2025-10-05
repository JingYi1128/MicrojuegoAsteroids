using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float maxLifeTime = 3f;
    public Vector3 targetVector;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        Invoke("ReturnToPool", maxLifeTime);
    }

    void OnDisable()
    {
        CancelInvoke();
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(speed * targetVector * Time.deltaTime,Space.World);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Player.SCORE++;
            UpdateScoreText();

            Asteroid asteroid = collision.gameObject.GetComponent<Asteroid>();
            if (asteroid != null)
            {
               asteroid.SplitAsteroid(); 
            } 

            ReturnToPool();

            Destroy(collision.gameObject);
        }
    }

    private void ReturnToPool()
    {
        BulletPool.instance.ReturnBullet(gameObject);
    }

    /*private void IncreaseScore()
    {
        Player.SCORE++;
        Debug.Log(Player.SCORE);
        UpdateScoreText();
    }*/

    private void UpdateScoreText()
    {
        GameObject go = GameObject.FindGameObjectWithTag("UI");
        if (go != null)
        {
            Text scoreText = go.GetComponent<Text>();
            if (scoreText != null)
            {
                scoreText.text = "Puntos: " + Player.SCORE;
            }
        }
       
    }
    
}
