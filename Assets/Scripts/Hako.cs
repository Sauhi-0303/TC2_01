using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hako : MonoBehaviour
{
    public int durability = 1000;

    // ���e�ɂ��_���[�W���󂯂�֐�
    public void TakeDamage(int damage)
    {
        durability -= damage;
        Debug.Log("���̑ϋv�l: " + durability);

        if (durability <= 0)
        {
            Destroy(gameObject);
            Debug.Log("�������܂����I");
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
