using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    private float smoothness = 0.1f;]

    [SerializeField]
    private Vector3 targetPosition;

    // カメラ位置のプリセット
    private Vector3 titlePos = new Vector3(1280f + 500f, 0f, 0f);
    private Vector3 descriptionPos = new Vector3(0f, 0f, 10f);
    private Vector3 gamePos = new Vector3(0f, 0f, 0f);
    private Vector3 resultPos = new Vector3(-500f, 0f, 0f);

    [SerializeField] private Renderer titleRenderer;
    [SerializeField] private Renderer descriptionRenderer;
    [SerializeField] private Renderer gameRenderer;
    [SerializeField] private Renderer resultRenderer;

    private enum CameraState
    {
        Title,
        Description,
        Game,
        Result
    }

    private CameraState currentState = CameraState.Title;

    // Start is called before the first frame update
    void Start()
    {
        targetPosition = titlePos;

        // 各Rendererにマテリアルを設定
        titleRenderer.material = titleMaterial;
        descriptionRenderer.material = descriptionMaterial;
        gameRenderer.material = gameMaterial;
        resultRenderer.material = resultMaterial;
    }

    // カメラの状態に応じてRendererを切り替える
    void SetActiveRenderer(Renderer active)
    {
        titleRenderer.enabled = (active == titleRenderer);
        descriptionRenderer.enabled = (active == descriptionRenderer);
        gameRenderer.enabled = (active == gameRenderer);
        resultRenderer.enabled = (active == resultRenderer);
    }

    // Update is called once per frame
    void Update()
    {
        if(target!=null)
        {
            transform.position = Vector3.Lerp(transform.position, target.position, smoothness);
            
        }

        // カメラの移動処理
        //transform.position = Vector3.Lerp(transform.position, targetPosition, smoothness);
        
        // 入力によるカメラ切り替え
        //if (Input.GetKeyDown(KeyCode.Tab))
        //{
        //    if (currentState == CameraState.Title)
        //    {
        //        currentState = CameraState.Description;
        //        targetPosition = descriptionPos;
        //    }
        //    else if (currentState == CameraState.Game)
        //    {
        //        currentState = CameraState.Description;
        //        targetPosition = descriptionPos;
        //    }
        //}
        //
        //if (Input.GetMouseButtonDown(0)) // 左クリック
        //{
        //    if (currentState == CameraState.Title)
        //    {
        //        currentState = CameraState.Game;
        //        targetPosition = gamePos;
        //    }
        //}
        //
        // プレイ中にHakoの耐久がゼロになったらResultに移行
        if (currentState == CameraState.Game && Hako.IsAllHakoDestroyed())
        {
            currentState = CameraState.Result;
            targetPosition = resultPos;
        }
    }
}