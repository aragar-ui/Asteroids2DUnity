using UnityEngine;

public class MenuPausa : MonoBehaviour
{

    public GameObject container;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            container.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void BotonReanudar()
    {
        container.SetActive(false);
        Time.timeScale = 1;
    }
}
