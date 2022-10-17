using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    public float moveSpeed = 3;
    private Vector2 startPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Destroy();
    }

    private void Move()
    {
        if (startPos.x < 0)
        {
            transform.Translate(Vector2.right * Time.deltaTime * moveSpeed);
        }
        else if (startPos.x > 0)
        {
            transform.Translate(Vector2.left * Time.deltaTime * moveSpeed);
        }
    }

    private void Destroy()
    {
        if(startPos.x < 0 && transform.position.x >= 10)
        {
            Destroy(this.gameObject);
        }
        else if(startPos.x > 0 && transform.position.x <= -10)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            AudioManager.instance.PlaySFX(2);
            other.transform.position = new Vector2(other.transform.position.x, -4.295f);
        }
    }
}
