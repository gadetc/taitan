﻿using UnityEngine;
using System.Collections;

public class BadgerController : MonoBehaviour {
    #region 可配置参数
    private float moveSpeed = 2f;
    private float m_life = 15;
    private int m_attack = 20;
    #endregion
    public GameObject[] points;

    private int currentIndex = 0;
    private GameObject currentPoint;
    private GameObject nextPoint;

    private bool dead = false;
    void Start()
    {
        for (var i = 0; i <= 11; i++)
        {
            points[i] = GameObject.Find("Point" + i);
        }
        //target = GameObject.Find("End");
        //monster = GetComponent<NavMeshAgent>();

    }

    void Update()
    {
        //var p = transform.position;
        //Vector3 target = new Vector3(p.x, p.y - moveSpeed, p.z);
        //transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime);
        if (!dead)
        {
            currentPoint = points[currentIndex];
            nextPoint = points[currentIndex + 1];
            var direction = nextPoint.transform.position - transform.position;
            direction.z = 0;
            direction.Normalize();
            Vector3 target = direction * moveSpeed + transform.position;
            transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime);
            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 180;

            //transform.rotation =Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, targetAngle), 5 * Time.deltaTime);
            //transform.rotation = Quaternion.Euler(0, 0, targetAngle);
            //transform.LookAt(new Vector3(0,0,nextPoint.transform.position.y));

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, targetAngle), 15 * Time.deltaTime);
        }
        //monster.SetDestination(target.transform.position);     
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Bullet")
        {
            Destroy(col.gameObject);
            audio.Play();
            m_life =m_life -  bunnyController.m_attack;
            if (m_life <= 0) {
                dead = true;
                Invoke("DestoryO", audio.clip.length);
            }
        }
        else if (col.tag == "End")
        {
            bunnyController.m_maxLife -= m_attack;
            if (bunnyController.m_maxLife <= 0) {
                DestroyObject(GameObject.Find("bunny_1"));
            }
            Destroy(gameObject);
        }
        else if (col.tag == "Point")
        {
            var colIndex = System.Convert.ToInt32(col.name.Replace("Point", ""));
            //
            //Debug.Log("col" + colIndex);
            //Debug.Log("curr" + currentIndex);
            if (colIndex != currentIndex)
            {
                currentIndex++;
            }
        }
    }

    void DestoryO()
    {
            Destroy(gameObject);

    }
}
