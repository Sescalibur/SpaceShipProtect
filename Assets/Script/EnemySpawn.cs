using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] float enemySpeed;
    [SerializeField] float wight;
    [SerializeField] float hight;
    [SerializeField] float timeSpace;
    private float xMin;
    private float xMax;
    private bool right = true;
    //private static int enemyNumber;
    // Start is called before the first frame update
    void Start()
    {
        EnemyStepByStepIno();
        CameraBoyut();
    }
    public void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position,new Vector3(wight,hight));
    }
    // Update is called once per frame
    void Update()
    {
        MinMax();
        if (EnemyIsDead())
        {
            EnemyStepByStepIno();
        }
    }

    private void CameraBoyut()
    {
        float uzaklik = transform.position.z - Camera.main.transform.position.z;
        Vector3 solUc = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, uzaklik));
        Vector3 sagUc = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, uzaklik));
        xMin = solUc.x;
        xMax = sagUc.x;
    }

    private void EnemyIno()
    {
        foreach (Transform Child in transform)
        {
            GameObject Enemy = Instantiate(enemy, Child.transform.position, Quaternion.identity) as GameObject;
            Enemy.transform.localRotation = new Quaternion(180, 0, 0, 0);
            Enemy.transform.parent = Child.transform;
        }
    }

    private void MinMax()
    {
        if (right)
        {
            transform.position += Vector3.right * enemySpeed;
        }
        else
        {
            transform.position += Vector3.left * enemySpeed;
        }
        float sagSinir = transform.position.x + (wight / 2);
        float solSinir = transform.position.x - (wight / 2);
        if (sagSinir > xMax)
        {
            right = false;
        }

        if (solSinir < xMin)
        {
            right = true;
        }
    }

    public bool EnemyIsDead()
    {
        foreach (Transform Child in transform)
        {
            if (Child.childCount > 0)
            {
                return false;
            }
        }
        return true;
    }

    Transform nextEnemy()
    {
        foreach (Transform Child in transform)
        {
            if (Child.childCount == 0)
            {
                return Child;
            }
        }
        return null;
    }

    void EnemyStepByStepIno()
    {
        Transform applyPosition = nextEnemy();
        if (applyPosition)
        {
            GameObject Enemy = Instantiate(enemy, applyPosition.transform.position, Quaternion.identity) as GameObject;
            Enemy.transform.localRotation = new Quaternion(180, 0, 0, 0);
            Enemy.transform.parent = applyPosition.transform;
        }

        if (nextEnemy())
        {
            Invoke("EnemyStepByStepIno",timeSpace);
        }
    }
}
