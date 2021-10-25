﻿/*
 * Full Name        : Zijie Wang
 * StudentID        : 101283316
 * Date Modified    : October 24, 2021
 * File             : PlayerController
 * Description      : This is the Player Controller Script
 * Revision History : v0.1
 */


using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    //public GameObject GameManagerGO;

    public GameObject PlayerBullet;
    public GameObject bulletPosition01;
    public GameObject bulletPosition02;
    public GameObject Explosion;

    public Text LivesUIText;

    //const int MaxLives = 3;
    //int lives;

    //public void Init()
    //{
    //    lives = MaxLives;

    //    LivesUIText.text = lives.ToString();

    //    gameObject.SetActive(true);

    //}


    public BulletManager bulletManager;

    [Header("Boundary Check")]
    public float horizontalBoundary;

    [Header("Player Speed")]
    public float horizontalSpeed;
    public float maxSpeed;
    public float horizontalTValue;

    [Header("Bullet Firing")]
    public float fireDelay;

    // Private variables
    private Rigidbody2D m_rigidBody;
    private Vector3 m_touchesEnded;

    // Start is called before the first frame update
    void Start()
    {
        m_touchesEnded = new Vector3();
        m_rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        GameObject bullet01 = (GameObject)Instantiate(PlayerBullet);
        bullet01.transform.position = bulletPosition01.transform.position;

        GameObject bullet02 = (GameObject)Instantiate(PlayerBullet);
        bullet02.transform.position = bulletPosition02.transform.position;

        _Move();
        _CheckBounds();
        _FireBullet();
    }

     private void _FireBullet()
    {
        // delay bullet firing 
        if(Time.frameCount % 60 == 0 && bulletManager.HasBullets())
        {
            bulletManager.GetBullet(transform.position);
        }
    }

    private void _Move()
    {
        float direction = 0.0f;

        // touch input support
        foreach (var touch in Input.touches)
        {
            var worldTouch = Camera.main.ScreenToWorldPoint(touch.position);

            if (worldTouch.x > transform.position.x)
            {
                // direction is positive
                direction = 1.0f;
            }

            if (worldTouch.x < transform.position.x)
            {
                // direction is negative
                direction = -1.0f;
            }

            m_touchesEnded = worldTouch;

        }

        // keyboard support
        if (Input.GetAxis("Horizontal") >= 0.1f) 
        {
            // direction is positive
            direction = 1.0f;
        }

        if (Input.GetAxis("Horizontal") <= -0.1f)
        {
            // direction is negative
            direction = -1.0f;
        }

        if (m_touchesEnded.x != 0.0f)
        {
           transform.position = new Vector2(Mathf.Lerp(transform.position.x, m_touchesEnded.x, horizontalTValue), transform.position.y);
        }
        else
        {
            Vector2 newVelocity = m_rigidBody.velocity + new Vector2(direction * horizontalSpeed, 0.0f);
            m_rigidBody.velocity = Vector2.ClampMagnitude(newVelocity, maxSpeed);
            m_rigidBody.velocity *= 0.99f;
        }
    }

    private void _CheckBounds()
    {
        // check right bounds
        if (transform.position.x >= horizontalBoundary)
        {
            transform.position = new Vector3(horizontalBoundary, transform.position.y, 0.0f);
        }

        // check left bounds
        if (transform.position.x <= -horizontalBoundary)
        {
            transform.position = new Vector3(-horizontalBoundary, transform.position.y, 0.0f);
        }

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if((col.tag == "EnemyShipTag") || (col.tag == "EnemyBulletTag"))
        {

            PlayExplosion();
            Destroy(gameObject);
            //lives--;
            //LivesUIText.text = lives.ToString();

            //if (lives == 0)
            //{
            //    //GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.GameOver);
            //    //gameObject.SetActive(false);
            //    Destroy(gameObject);
            //}

            
        }
    }

    void PlayExplosion()
    {
        GameObject explosion = (GameObject)Instantiate(Explosion);

        explosion.transform.position = transform.position;
    }

}
