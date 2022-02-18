using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipController : MonoBehaviour
{
    [SerializeField] float ShipSpeed;
    [SerializeField] GameObject ammo;
    [SerializeField] float ammoSpeed;
    [SerializeField] float FireAralik;
    private int health = 200;
    private float xMin;
    private float xMax;

    [SerializeField] AudioClip fireClip;
    [SerializeField] AudioClip deathClip;
    // Start is called before the first frame update
    void Start()
    {
        float uzaklik = transform.position.z - Camera.main.transform.position.z;
        Vector3 solUc = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, uzaklik));
        Vector3 sagUc = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, uzaklik));
        xMin = solUc.x + (transform.localScale.x*3 / 4);
        xMax = sagUc.x - (transform.localScale.x*3 / 4);
    }

    // Update is called once per frame
    void Update()
    {
        ShipControl();
        InvokeRepeating("Fire",0.001f,FireAralik);
        if (Input.GetKeyUp(KeyCode.Space))
        {
            CancelInvoke("Fire");
        }
    }

    void ShipControl()
    {
        float newX = Mathf.Clamp(transform.position.x, xMin, xMax);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
        
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            //transform.position += new Vector3(Mathf.Clamp(-ShipSpeed+gameObject.transform.position.x,-7.52f,7.52f), 0,0);
            transform.position += Vector3.left * ShipSpeed;
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            //transform.position += new Vector3(ShipSpeed, 0, 0);
            transform.position += Vector3.right * ShipSpeed;
        }
    }

    void Fire()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //InvokeRepeating("Fire", 0.001f, FireAralik);
            GameObject Ammo= Instantiate(ammo, transform.position, Quaternion.identity) as GameObject;
            Ammo.transform.parent = GameObject.Find("Fire").transform;
            Ammo.GetComponent<Rigidbody2D>().velocity = new Vector3(0, ammoSpeed,0);
            AudioSource.PlayClipAtPoint(fireClip, transform.position);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            CancelInvoke("Fire");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyAmmo"))
        {
            health -= 20;
            if (health <= 0)
            {
                Destroy(gameObject);
                AudioSource.PlayClipAtPoint(deathClip, transform.position);
                //GameObject.Find("EnemyShip").GetComponent<EnemyShipController>().score();
            }
        }
    }
}
