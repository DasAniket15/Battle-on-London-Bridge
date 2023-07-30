using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    private float timer;
    public Rigidbody2D rb;
    
    // Start is called before the first frame update
    void Start()
    {
        timer = 0; 
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void FixedUpdate()
    {
        timer = timer +1;
        Debug.Log(timer);

        if (timer > 0 && timer <= 10)
        {
            rb.AddForce(new Vector2(40f,40f));
        }
        else if (timer > 10 && timer <= 200)
        {
            rb.AddForce(new Vector2(0, 0));
        }
       /*& else if (timer > 200 && timer <= 300)
        {
            rb.velocity = new Vector2(-4, 0.5f);
        }
        else if (timer > 300 && timer <= 400)
        {
            rb.velocity = new Vector2(-4, -0.5f);

        }
        else if (timer > 400) 
        {
            timer = 0;

        }*/

    }
}
