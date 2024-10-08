using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//추상 클래스 
public abstract class Vehicle : MonoBehaviour
{
    public float speed = 10.0f;

    //가상 메서드
    public virtual void Move()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }


    //추상 메서드
    public abstract void Horn();
}
