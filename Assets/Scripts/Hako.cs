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

    void Explode()
    {
        // ���a2.0f���̃R���C�_�[���擾
        Collider[] colliders = Physics.OverlapSphere(transform.position, 2.0f);

        foreach (Collider col in colliders)
        {
            Hako hako = col.GetComponent<Hako>();
            if (hako != null)
            {
                hako.TakeDamage(100); // ��x����100�_���[�W
            }
        }

        // �����G�t�F�N�g����ʉ��������ɒǉ����Ă�OK

        Destroy(gameObject); // ���e���g������
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
