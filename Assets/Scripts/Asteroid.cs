using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public GameObject asteroidPrefab; // Prefab para asteroides pequeños
    public int splitCount = 2;        // Cuántos asteroides generar al dividirse

    // Esto se llama cuando la bala impacta
    public void SplitAsteroid()
    {
        for (int i = 0; i < splitCount; i++)
        {
            float offsetX = Random.Range(-0.5f, 0.5f);
            float offsetY = Random.Range(-0.5f, 0.5f);

            Vector3 spawnPos = transform.position + new Vector3(offsetX, offsetY, 0);
            GameObject newAsteroid = Instantiate(asteroidPrefab, spawnPos, Quaternion.identity);

            // Añadir fuerza aleatoria para que se muevan
            Rigidbody rb = newAsteroid.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector2 randomDir = Random.insideUnitCircle.normalized;
                rb.AddForce(new Vector3(randomDir.x, randomDir.y, 0) * 50f);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            SplitAsteroid();
            Destroy(collision.gameObject); // destruir la bala
            Destroy(gameObject);           // destruir este asteroide
        }
    }
}
