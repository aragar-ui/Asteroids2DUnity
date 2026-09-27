using System.Collections.Generic; // Necesario para usar List<>
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float thrustForce = 5f;
    public float rotationSpeed = 10f;

    public GameObject gun, bulletPrefab;

    // --- VARIABLES DEL POOLING ---
    public int poolSize = 20;                     // Cantidad de balas precreadas
    private List<GameObject> bulletPool;          // Lista que almacena las balas
    // -----------------------------

    private Rigidbody _rigid;

    public static int SCORE = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rigid = GetComponent<Rigidbody>();

        // Inicializamos el almacén de balas
        bulletPool = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.SetActive(false); // Las dejamos apagadas
            bulletPool.Add(bullet);  // Las guardamos en la lista
        }
    }

    // Update is called once per frame
    void Update()
    {
        float rotation = Input.GetAxis("Horizontal") * Time.deltaTime;
        float thrust = Input.GetAxis("Vertical") * Time.deltaTime;

        Vector3 thrustDirection = transform.right;

        _rigid.AddForce(thrustForce * thrustDirection * thrust);

        transform.Rotate(Vector3.forward, -rotation * rotationSpeed);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    // Función simple para pedir una bala al pool
    private void Shoot()
    {
        for (int i = 0; i < bulletPool.Count; i++)
        {
            // Buscamos la primera bala que esté apagada
            if (!bulletPool[i].activeInHierarchy)
            {
                bulletPool[i].transform.position = gun.transform.position; // La movemos al cañón
                bulletPool[i].transform.rotation = Quaternion.identity;

                Bullet balaScript = bulletPool[i].GetComponent<Bullet>();
                balaScript.targetVector = transform.right;

                bulletPool[i].SetActive(true); // La encendemos para disparar
                return; // Salimos para disparar solo una bala por pulsación
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            SCORE = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            Debug.Log("He colisionado con otra cosa");
        }
    }
}
