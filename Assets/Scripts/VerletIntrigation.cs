using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerletIntrigation : MonoBehaviour
{
   
    public float updateTime,width,height,gravity,friction;
    public float velRange;
    public Particle particle;
    public int numParticles;
    Particle[] totalParticle;

    // Start is called before the first frame update
    void Start()
    {
        if(numParticles%2!=0)
        {
            numParticles += 1;
        }
        totalParticle = new Particle[numParticles];
        for(int i=0;i<numParticles;i++)
        {
            Particle p = Instantiate(particle, transform.position, Quaternion.identity);
            totalParticle[i] = p;
            p.currentPos.x = Random.Range(-width, width);
            p.currentPos.y = Random.Range(-height, height);
            float initial_x = Random.Range(-velRange, velRange);
            float iniitial_y = Random.Range(-velRange, velRange);
            p.initialPos.x = p.currentPos.x + initial_x;
            p.initialPos.y = p.currentPos.y + iniitial_y;
            p.transform.position = p.currentPos;
            if(i!=0)
            {
                p.connectedParticle = totalParticle[i - 1];
                p.length = Vector3.Distance(p.currentPos, p.connectedParticle.currentPos);
            }
        }
        
       

    }

    // Update is called once per frame
    void Update()
    {
        for(int i=0;i<numParticles;i++)
        {
            Particle p = totalParticle[i];
            float diff_x = (p.currentPos.x - p.initialPos.x) * friction;
            float diff_y = (p.currentPos.y - p.initialPos.y) * friction;
            Vector2 newCurrent = p.currentPos;
            newCurrent.x += diff_x;
            newCurrent.y += diff_y;

            newCurrent.y -= gravity;

            p.initialPos = p.currentPos;
            p.currentPos = newCurrent;

            if (Mathf.Abs(newCurrent.x) > width)
            {
                p.currentPos.x = Mathf.Sign(p.currentPos.x) * width;
                p.initialPos.x = p.currentPos.x + diff_x;
            }

            if (Mathf.Abs(newCurrent.y) > height)
            {
                p.currentPos.y = Mathf.Sign(p.currentPos.y) * height;
                p.initialPos.y = p.currentPos.y + diff_y - (gravity * 0.5f);
            }

            p.transform.position = p.currentPos;

            if(i!=0)
            {
                Vector3 diffVector = (p.currentPos - p.connectedParticle.currentPos).normalized;
                
                float dist = p.length - Vector3.Distance(p.currentPos, p.connectedParticle.currentPos);
                float change = dist / 2;
                
                diffVector = diffVector * change;
                Debug.Log(diffVector);
                p.currentPos += diffVector;
                p.connectedParticle.currentPos -= diffVector;
                p.transform.position = p.currentPos;
                p.connectedParticle.transform.position = p.connectedParticle.currentPos;

            }
        }
        
    }
    
  
}
