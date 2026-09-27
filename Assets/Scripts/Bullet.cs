using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float maxLifetime = 2.5f;
    public Vector3 targetVector;

    // Temporizador propio para medir el tiempo de vida
    private float timer;

    // Se ejecuta CADA VEZ que la bala se reactiva desde el pool
    private void OnEnable()
    {
        timer = 0f; // Reiniciamos el reloj cada vez que se dispara
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(speed * targetVector * Time.deltaTime);

        // Contamos el tiempo y la apagamos al expirar (en lugar de Destroy)
        timer += Time.deltaTime;
        if (timer >= maxLifetime)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            IncreaseScore();
            Destroy(collision.gameObject);

            // En lugar de Destroy(gameObject), la desactivamos
            gameObject.SetActive(false);
        }
    }

    private void IncreaseScore()
    {
        Player.SCORE++;
        Debug.Log(Player.SCORE);
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        GameObject go = GameObject.FindGameObjectWithTag("UI");
        go.GetComponent<Text>().text = "Puntos :" + Player.SCORE;
    }
}