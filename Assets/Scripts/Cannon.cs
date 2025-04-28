using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField]
    private Rigidbody cannonBallPrefab; //�e�ۃv���n�u
    [SerializeField]
    private Transform spawnPoint; //�ˏo�ʒu
    [SerializeField]
    private Vector3 shotVelocity=new Vector3(0.0f,0.0f,0.0f); //�ˏo�x�N�g��
    [SerializeField]
    private float shotTime=2.0f; //�ˏo�Ԋu
    private float timer; //�ˏo�^�C�}�[
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //���Ԋu���ɒe�ۂ��ˏo����
        timer += Time.deltaTime;
        if (timer>=shotTime)
        {
            timer = 0.0f;
            //�e�ۂ��ˏo�ʒu�ɐ���
            //Rigidbody cannonBall = Instantiate(cannonBallPrefab,spawnPoint.position-, spawnPoint.rotation);
            //cannonBall.velocity = shotVelocity; //�ˏo��������Ɨ͂�ݒ�

            //�ˏo���Ă���w�莞�Ԍ�ɔ�������
            //Destroy(cannonBall.gameObject, 2.0f);
        }
    }
}
