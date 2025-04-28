using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class LeanBridge : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    //�t���[���X�V�����̑O�Ɉ�x�����Ă΂��֐�
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    
    //�Փˎ��ɌĂ΂��֐�
    private void OnCollisionEnter(Collision collision)
    {
        //�Ռ������ȏ�Ȃ狴�˂���
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
