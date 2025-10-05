using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public GameObject panelPausa;
    private bool estaPausado = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PausaGame()
    {
        estaPausado = !estaPausado;

        if (panelPausa != null)
        {
            panelPausa.SetActive(estaPausado);
        }
        
        Time.timeScale = estaPausado ? 0f : 1f;
    }

    public void RestaurarGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
