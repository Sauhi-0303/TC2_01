using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class LeanBridge : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    //フレーム更新処理の前に一度だけ呼ばれる関数
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    
    //衝突時に呼ばれる関数
    private void OnCollisionEnter(Collision collision)
    {
        //衝撃が一定以上なら橋架ける
        if(collision.impulse.magnitude>0.01f)
        {
            animator.SetTrigger("Lean");
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
