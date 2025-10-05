//using System.Collections;
//using System.Collections.Generic;
using UnityEngine.SceneManagement;

using UnityEngine;

public class Player : MonoBehaviour
{

    //Parametros que definen la velocidad del nave, necesitamos configurar tanto la velocidad de empuje como la velocidad de rotación
    public float thrustForce = 5f; // Velocidad de empuje
    public float rotationSpeed = 55f; //Velocidad de rotacion

    public GameObject gun, bulletPrefab;

    private Rigidbody _rigid;

    public static int SCORE = 0;
    public float xBorderLimit = 8f;
    public float yBorderLimit = 4f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        var newPos = transform.position;
        if (newPos.x > xBorderLimit)
        {
            newPos.x = -xBorderLimit + 1;
        }
        else if (newPos.x < -xBorderLimit)
        {
            newPos.x = xBorderLimit - 1;
        }
        else if (newPos.y > yBorderLimit)
        {
            newPos.y = -yBorderLimit + 1;
        }
        else if (newPos.y < -yBorderLimit)
        {
            newPos.y = yBorderLimit - 1;   
        }

        transform.position = newPos;

       /* if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject bullet = Instanciate(bulletPrefab, gun.transform.position, Quaternion.identity);
            bullet balaScript = bullet.GetComponent<bullet>();
            balaScript.targetVector = transform.right();
        }*/

        float rotation = Input.GetAxis("Rotate") * Time.deltaTime;
        float thrust = Input.GetAxis("Thrust") * Time.deltaTime * thrustForce;

        //Direccion hacia donde se mueve la nave,hacia donde esta apuntada la cabeza, hacia la derecha
        Vector3 thrustDirection = transform.right;

        _rigid.AddForce(thrustForce * thrustDirection * thrust);

        transform.Rotate(Vector3.forward, -rotation * rotationSpeed);

         if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject bullet = BulletPool.instance.RequestBullet();
            if (bullet != null)
            {
                bullet.transform.position = gun.transform.position;
                bullet.transform.rotation = gun.transform.rotation;

                Bullet bulletScript = bullet.GetComponent<Bullet>();
                bulletScript.targetVector = transform.right;
            }
        }
    }

    //Funcion para colisones 
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            SCORE = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            Debug.Log("He colisionado con otra cosa...");
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //Funcion para atravesamientos
    /*private OnTriggerEnter(Collider other)
    {
        Debug.log("Atravesamiento");
    }*/
}
