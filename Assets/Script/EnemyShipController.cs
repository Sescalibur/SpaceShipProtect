using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyShipController : MonoBehaviour
{
    private int health = 100;
    [SerializeField] GameObject ammo;
    [SerializeField] float ammoSpeed;
    private float timeFire = 0.001f;
    public static int Score;
    [SerializeField] AudioClip enemyFire;
    [SerializeField] AudioClip enemyDeath;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Random.value < timeFire)
        {
            EnemyFire();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ammo"))
        {
            health -= 20;
            if (health <= 0)
            {
                Destroy(gameObject);
                health = 100;
                Score += 20;
                GameObject.Find("ScoreValue").GetComponent<Text>().text = Score.ToString();
                AudioSource.PlayClipAtPoint(enemyDeath, transform.position);
            }
        }
    }

    public void EnemyFire()
    {
        Vector3 basla = transform.position;
        GameObject Ammo = Instantiate(ammo, basla, Quaternion.identity)as GameObject;
        Ammo.transform.localRotation = new Quaternion(180, 0, 0, 0);
        Ammo.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, ammoSpeed);
        Ammo.transform.parent = GameObject.Find("EnemyFire").transform;
        AudioSource.PlayClipAtPoint(enemyFire, transform.position);
    }
    public void score()
    {
        Score=0;
        GameObject.Find("ScoreValue").GetComponent<Text>().text = Score.ToString();
    }
}
