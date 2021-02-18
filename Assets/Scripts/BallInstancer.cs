using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallInstancer : MonoBehaviour
{
    public VerletIntrigation ball;
    public int numBall;
    // Start is called before the first frame update
    void Start()
    {
        for(int i=1;i<=numBall;i++)
        {
            VerletIntrigation v=  Instantiate(ball, transform.position, Quaternion.identity);
            if(i%2==0)
            {

            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
