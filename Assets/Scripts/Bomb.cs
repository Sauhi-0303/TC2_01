using System.Collections;

using System.Collections.Generic;

using UnityEngine;

public class Bomb : MonoBehaviour

{

    public GameObject bombPrefab; // ���e�̃v���n�u

    private Camera mainCamera;

    public BombType bombType; // ���e�̎��

    private static Dictionary<BombType, int> bombCounts = new Dictionary<BombType, int>();


    void Start()

    {

        if (!bombCounts.ContainsKey(bombType))

        {

            bombCounts[bombType] = 0;

        }

        bombCounts[bombType]++;

        mainCamera = Camera.main;

    }

    // ���e�̎�ނ�񋓂���

    void OnDestroy()

    {

        bombCounts[bombType]--;

    }

    // ���e�̎�ނ�񋓂���

    public static int GetBombCount(BombType type)

    {

        return bombCounts.ContainsKey(type) ? bombCounts[type] : 0;

    }

    // ���e�̔�������

    void OnTriggerEnter(Collider other)

    {

        Bomb otherBomb = other.GetComponent<Bomb>();

        if (otherBomb != null && otherBomb.bombType == bombType)

        {

            int count = GetBombCount(bombType);

            if (count >= 4)

            {

                Explode();

            }

        }

    }

    void SpawnBomb()

    {

        Vector3 mousePos = Input.mousePosition;

        mousePos.z = 10f; // �J��������̋�����ݒ�

        Vector3 worldPos = mainCamera.ScreenToWorldPoint(mousePos);

        Instantiate(bombPrefab, worldPos, Quaternion.identity);

    }

    void KeepBombOffScreen()

    {

        Vector3 offScreenPos = new Vector3(-1000f, -1000f, 0f); // ��ʊO�̍��W

        Instantiate(bombPrefab, offScreenPos, Quaternion.identity);

    }

    void Explode()
    {
        // ���a2.0f�͈͓̔��ɂ���I�u�W�F�N�g�����o
        Collider[] colliders = Physics.OverlapSphere(transform.position, 2.0f);

        foreach (Collider nearbyObject in colliders)
        {
            Hako hako = nearbyObject.GetComponent<Hako>();
            if (hako != null)
            {
                hako.TakeDamage(100); // ����100�_���[�W
            }
        }

        // �G�t�F�N�g�Ȃǂ������Œǉ����邱�Ƃ��\

        Destroy(gameObject); // ������Ɏ��g��j��
    }

    void Update()

    {

        if (Input.GetMouseButtonDown(0)) // ���N���b�N

        {

            SpawnBomb();

        }

        else if (Input.GetMouseButtonDown(1)) // �E�N���b�N

        {

            KeepBombOffScreen();

        }

    }

}