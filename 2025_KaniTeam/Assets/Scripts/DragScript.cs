using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragScript : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{

    private Vector2 prevPos; //保存しておく初期position
    private RectTransform rectTransform; // 移動したいオブジェクトのRectTransform
    private RectTransform parentRectTransform; // 移動したいオブジェクトの親(Panel)のRectTransform

    [Header("Drag Settings")]
    [SerializeField] private float minX = -350f;
    [SerializeField] private float maxX = 350f;
    [SerializeField] private float minY = 0f;
    [SerializeField] private float maxY = 1310f;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float rectTransformX = 90f;
    [SerializeField] private bool isDragging = false;
    [SerializeField] private bool hasDraggingOnce = false;
    [SerializeField] private bool isFalling = false;
    [SerializeField] private float Bigsize = 3f;
    [SerializeField] private GameObject pazuruObject;


    [SerializeField] public bool isGameOver = false;





    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        parentRectTransform = rectTransform.parent as RectTransform;
    }

    void Start()
    {
        rectTransform.anchoredPosition = new Vector2(rectTransformX,0);
        prevPos = rectTransform.anchoredPosition;
    }

    void Update()
    {

        /*if (!isDragging)
        {
            rectTransform.anchoredPosition -= new Vector2(0, speed);
            float clampedX = Mathf.Clamp(rectTransform.anchoredPosition.x, minX, maxX);
            float clampedY = Mathf.Clamp(rectTransform.anchoredPosition.y, minY, maxY);
            rectTransform.anchoredPosition = new Vector2(clampedX, clampedY);
        }*/

        if (isFalling && !isDragging)
        {
            // 落下させる
            rectTransform.anchoredPosition -= new Vector2(0, speed);

            // 横方向だけClamp、縦方向と横方向は範囲外も許可
            float clampedX = Mathf.Clamp(rectTransform.anchoredPosition.x, minX, maxX);
            float clampedY = Mathf.Clamp(rectTransform.anchoredPosition.y, minY, maxY);
            rectTransform.anchoredPosition = new Vector2(clampedX, rectTransform.anchoredPosition.y);
        }

    }

    // ドラッグ開始時の処理
    public void OnBeginDrag(PointerEventData eventData)
    {
        // パネル外に出た後は再ドラッグ禁止
        if (hasDraggingOnce) return;

        isDragging = true;
        prevPos = rectTransform.anchoredPosition;
    }


    // ドラッグ中の処理
    public void OnDrag(PointerEventData eventData)
    {
        if (hasDraggingOnce) return;
        // eventData.positionから、親に従うlocalPositionへの変換を行う
        // オブジェクトの位置をlocalPositionに変更する

        Vector2 localPosition = GetLocalPosition(eventData.position);
        localPosition.x = Mathf.Clamp(localPosition.x, minX, maxX);
        localPosition.y = Mathf.Clamp(localPosition.y, minY, maxY);
        rectTransform.anchoredPosition = localPosition;
    }

    //ドラッグ終了時の処理
    public void OnEndDrag(PointerEventData eventData)
    {
        // スクリーン座標を親のローカル座標に変換
        Vector2 localPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRectTransform, eventData.position, Camera.main, out localPos);
        //Debug.Log("event"+ eventData.position, this);

        // パネルの高さから上下半分の距離を出す
        float halfHeight = parentRectTransform.rect.height;
        bool isInsidePanel = localPos.y <= halfHeight;

        //Debug.Log("half "+ halfHeight, this);
        //Debug.Log(Mathf.Abs(localPos.y), this);
        //Debug.Log(isInsidePanel, this);


        if (isInsidePanel)
        {
            // パネル内 → 元に戻して再ドラッグ可能に
            rectTransform.anchoredPosition = prevPos;
            isDragging = false;
            hasDraggingOnce = false; // 再ドラッグを許可！
            //Debug.Log("OnEndDrag: isInsidePanel = " + isInsidePanel);
            //Debug.Log("hasDraggingOnce = " + hasDraggingOnce);

            isFalling = false;
        }
        else
        {
            // パネル外 → 落下開始、一度きり
            isDragging = false;
            hasDraggingOnce = true;
            isFalling = true;

            transform.localScale = new Vector3(Bigsize, Bigsize, 0);
        }

    }


    // ScreenPositionからlocalPositionへの変換関数
    private Vector2 GetLocalPosition(Vector2 screenPosition)
    {
        Vector2 result = Vector2.zero;

        // screenPositionを親の座標系(parentRectTransform)に対応するよう変換する.
        RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRectTransform, screenPosition, Camera.main, out result);

        return result;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*if(collision.gameObject.tag == "Panel")
        {
            isDragging = false;
            hasDraggingOnce = false;
            isFalling = false;
            Debug.Log("当たり");
            transform.localScale = new Vector3(1f, 1f, 0);
        }*/


        if (collision.gameObject.CompareTag("GameOver"))
        {
            if(pazuruObject != null)
            {
                Destroy(pazuruObject);
                Debug.Log("GameOver");
                isGameOver = true;
            }
        }
    }
}


