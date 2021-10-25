/*
 * Full Name        : Zijie Wang
 * StudentID        : 101283316
 * Date Modified    : October 24, 2021
 * File             : EnemyController
 * Description      : This is the Enemy Controller Script
 * Revision History : v0.1
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject Explosion;

    public float horizontalSpeed;
    public float horizontalBoundary;
    public float direction;

    // Update is called once per frame
    void Update()
    {
        _Move();
        //_CheckBounds();
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        if(transform.position.y < min.y)
        {
            Destroy(gameObject);
        }
    }

    private void _Move()
    {
        //transform.position += new Vector3(horizontalSpeed * direction * Time.deltaTime, 0.0f, 0.0f);
        transform.position += new Vector3(0.0f, horizontalSpeed * direction * Time.deltaTime, 0.0f);
    }

    //private void _CheckBounds()
    //{
    //    // check right boundary
    //    if (transform.position.x >= horizontalBoundary)
    //    {
    //        direction = -1.0f;
    //    }

    //    // check left boundary
    //    if (transform.position.x <= -horizontalBoundary)
    //    {
    //        direction = 1.0f;
    //    }
    //}

    void OnTriggerEnter2D(Collider2D col)
    {
        if((col.tag == "PlayerShipTag") || (col.tag == "PlayerBulletTag"))
        {

            PlayExplosion();
            Destroy(gameObject);
        }
    }

    void PlayExplosion()
    {
        GameObject explosion = (GameObject)Instantiate(Explosion);

        explosion.transform.position = transform.position;
    }
}
