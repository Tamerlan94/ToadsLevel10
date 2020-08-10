using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UIElements;

public class EnemyLockMovement : MonoBehaviour
{
    private Animator anim;
    public float speed;    
    public bool isRight;
    void Start()
    {
        anim = GetComponent<Animator>();
        
    }
    private void FixedUpdate()
    {       
        //можно сделать по другому
        if (transform.localPosition.x < 0 && isRight)
            transform.Translate(Vector2.right * speed * Time.fixedDeltaTime, Space.Self);
        else if(isRight)
            isRight = !isRight;

        if (transform.localPosition.x > -1.9f && !isRight)
            transform.Translate(Vector2.left * speed * Time.fixedDeltaTime, Space.Self);
        else if(!isRight)
            isRight = !isRight;
    }    
}
