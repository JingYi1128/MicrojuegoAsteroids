using UnityEngine;
using System.Collections.Generic;

public class BulletPool : MonoBehaviour
{
    public GameObject bulletPrefab;
    public static BulletPool instance;

    public int poolSize = 20;

    private List<GameObject> bulletList;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bulletList = new List<GameObject>();
        AddBulletToPool(poolSize);
    }

    private void AddBulletToPool(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.SetActive(false);
            bulletList.Add(bullet);
            bullet.transform.parent = transform;
        }
    }
    public GameObject RequestBullet()
    {
        for (int i = 0; i < bulletList.Count; i++)
        {
            if (!bulletList[i].activeSelf)
            {
                bulletList[i].SetActive(true);
                return bulletList[i];
            }
        }
        return null;
    }

    public void ReturnBullet(GameObject bullet) {
        bullet.SetActive(false);
    }
    
}
