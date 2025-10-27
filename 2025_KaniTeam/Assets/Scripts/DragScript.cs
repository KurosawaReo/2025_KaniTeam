using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragScript : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private Vector2 prevPos; //�ۑ����Ă�������position
    private RectTransform rectTransform; // �ړ��������I�u�W�F�N�g��RectTransform
    private RectTransform parentRectTransform; // �ړ��������I�u�W�F�N�g�̐e(Panel)��RectTransform

    [SerializeField] private float minX = -6f;
    [SerializeField] private float maxX = 6f;
    [SerializeField] private float minY = -5f;
    [SerializeField] private float maxY = 5f;
    [SerializeField] private float speed = 0.05f;
//  [SerializeField] private float rectTransformX = -300f;
    [SerializeField] private bool isDragging = false;
    [SerializeField] private bool hasDraggingOnce = false;
    [SerializeField] private bool isFalling = false;
    [SerializeField] private float Bigsize = 3f;
    [SerializeField] private GameObject pazuruObject;
    [SerializeField] private GameObject fish;
    [SerializeField] private int Maxfishcount = 0;

    [SerializeField] public bool isGameOver = false;

    [Header("script")]
    [SerializeField] GManager gameMng;
    public bool isDragged = false; //�h���b�O�ς݂��ǂ���.

    private int fishCount = 0;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        parentRectTransform = rectTransform.parent as RectTransform;
    }

    void Start()
    {
        prevPos = rectTransform.anchoredPosition;

        fishCount = Maxfishcount;
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
            // ����������
            rectTransform.anchoredPosition -= new Vector2(0, speed);

            // ����������Clamp�A�c�����Ɖ������͔͈͊O������
            float clampedX = Mathf.Clamp(rectTransform.anchoredPosition.x, minX, maxX);
            float clampedY = Mathf.Clamp(rectTransform.anchoredPosition.y, minY, maxY);
            rectTransform.anchoredPosition = new Vector2(clampedX, rectTransform.anchoredPosition.y);
        }
    }

    // �h���b�O�J�n���̏���
    public void OnBeginDrag(PointerEventData eventData)
    {
        // �p�l���O�ɏo����͍ăh���b�O�֎~
        if (hasDraggingOnce) return;

        isDragging = true;
        prevPos = rectTransform.anchoredPosition;
    }

    // �h���b�O���̏���
    public void OnDrag(PointerEventData eventData)
    {
        if (hasDraggingOnce) return;
        // eventData.position����A�e�ɏ]��localPosition�ւ̕ϊ����s��
        // �I�u�W�F�N�g�̈ʒu��localPosition�ɕύX����

        Vector2 localPosition = GetLocalPosition(eventData.position);
        localPosition.x = Mathf.Clamp(localPosition.x, minX, maxX);
        localPosition.y = Mathf.Clamp(localPosition.y, minY, maxY);
        rectTransform.anchoredPosition = localPosition;
    }

    //�h���b�O�I�����̏���
    public void OnEndDrag(PointerEventData eventData)
    {
        // �X�N���[�����W��e�̃��[�J�����W�ɕϊ�
        Vector2 localPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRectTransform, eventData.position, Camera.main, out localPos);
        //Debug.Log("event"+ eventData.position, this);

        // �p�l���̍�������㉺�����̋������o��
        float halfHeight = parentRectTransform.rect.height;
        bool isInsidePanel = localPos.y <= halfHeight;

        //Debug.Log("half "+ halfHeight, this);
        //Debug.Log(Mathf.Abs(localPos.y), this);
        //Debug.Log(isInsidePanel, this);

        if (isInsidePanel)
        {
            // �p�l���� �� ���ɖ߂��čăh���b�O�\��
            rectTransform.anchoredPosition = prevPos;
            isDragging = false;
            hasDraggingOnce = false; // �ăh���b�O�����I
            //Debug.Log("OnEndDrag: isInsidePanel = " + isInsidePanel);
            //Debug.Log("hasDraggingOnce = " + hasDraggingOnce);

            isFalling = false;
        }
        else
        {
            // �p�l���O �� �����J�n�A��x����
            isDragging = false;
            hasDraggingOnce = true;
            isFalling = true;

            transform.localScale = new Vector3(Bigsize, Bigsize, 0);
        }
        //prefab������.
        GameObject prefab = Instantiate(fish);
        //��������prefab�̍��W��ݒ�.
        prefab.transform.position = transform.position;

        fishCount -= 1; //�u����c�萔-1.
        //�����u���Ȃ��Ȃ�.
        if(fishCount <= 0)
        {
            gameMng.FishCount--;
            isDragged = true;
            gameObject.SetActive(false); //����.
        }
        //�܂��u����Ȃ�.
        else
        {
            isDragging = false;
            hasDraggingOnce = false;
            isFalling = false;
            transform.localScale = new Vector3(1f, 1f, 0);
            rectTransform.anchoredPosition = prevPos; //���̈ʒu��.
        }
    }

    // ScreenPosition����localPosition�ւ̕ϊ��֐�
    private Vector2 GetLocalPosition(Vector2 screenPosition)
    {
        Vector2 result = Vector2.zero;

        // screenPosition��e�̍��W�n(parentRectTransform)�ɑΉ�����悤�ϊ�����.
        RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRectTransform, screenPosition, Camera.main, out result);

        return result;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if(collision.gameObject.tag == "Panel")
        //{
        //    isDragging = false;
        //    hasDraggingOnce = false;
        //    isFalling = false;
        //    Debug.Log("������");
        //   transform.localScale = new Vector3(1f, 1f, 0);
        //}

        if (collision.gameObject.tag == ("GameOver"))
        {
            if(pazuruObject != null)
            {
                Destroy(pazuruObject);
                Debug.Log("������");
                isGameOver = true;
            }
        }
    }
}