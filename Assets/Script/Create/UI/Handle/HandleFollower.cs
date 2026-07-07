using System;
using System.Collections;
using UnityEngine;

namespace ShotBall.Create
{
    /// <summary>
    /// �I�u�W�F�N�g��I�������Ƃ��̘g����
    /// �I�u�W�F�N�g���g��E�k���������]���邱�Ƃ��ł���
    /// </summary>
    public class HandleFollower : MonoBehaviour
    {
        private const int CornerCount = 4;
        private const float RotationOffsetDegrees = 90f;

        static HandleFollower instance;
        public static HandleFollower Instance => instance;

        public Camera worldCamera; // �ʏ�̓��C���J����
        [SerializeField] SpriteResizeHandle topLeftHandle;
        [SerializeField] SpriteResizeHandle topRightHandle;
        [SerializeField] SpriteResizeHandle bottomLeftHandle;
        [SerializeField] SpriteResizeHandle bottomRightHandle;

        [SerializeField] Lines lines;

        public SpriteRenderer spriteRenderer;
        private Canvas canvas;

        [SerializeField] HandleButton[] handleButtons;
        [SerializeField] Objects objects;

        private SpriteResizeHandle[] Handles => new SpriteResizeHandle[]
        {
            topLeftHandle,
            topRightHandle,
            bottomLeftHandle,
            bottomRightHandle
        };

        [SerializeField] ConnectionManager connectionManager;

        public Action OnResize { get; set; } // ���T�C�Y�J�n���̃A�N�V����

        private void Awake()
        {
            instance = this;
        }
        void Start()
        {
            canvas = GetComponentInParent<Canvas>();

            if (worldCamera == null) worldCamera = Camera.main;

            SpriteSet(null, false); // ������Ԃł̓n���h�����\���ɂ���

            ChangeHandles();
        }

        void Update()
        {
            if (CreateInputSystem.Instance == null) return;
            if (CreateInputSystem.Instance.IsDragging || CreateInputSystem.Instance.IsZooming||isRotate)
            {
                ChangeHandles();
                return;
            }
            foreach (var handle in Handles)
            {
                if (handle.IsResize || handle.IsDragging) ChangeHandles();
            }
        }

        public bool ResizeNow()
        {
            foreach (var handle in Handles)
            {
                if (handle.IsResize ) return true;
            }
            return false;
        }

        public void SpriteSet(SpriteRenderer s, bool active)
        {
            if(s==null) active = false; // null �̏ꍇ�͔�\���ɂ���
            spriteRenderer = s;
            foreach (var handle in Handles)
            {
                handle.SetSprite(s);
                handle.gameObject.SetActive(active);
            }
            foreach (var button in handleButtons)
            {
                if(s==null) button.gameObject.SetActive(false);
                else  button.gameObject.SetActive(true);
            }
            if (lines == null||s==null)  lines.ActiveSelf(false);
            else lines.ActiveSelf(true);
            ChangeHandles();
            if (s == null) return;
            connectionManager.Connection(s.gameObject.GetComponent<Connect>());
        }
        void ChangeHandles()
        {
            if (spriteRenderer == null) return;

            Transform t = spriteRenderer.transform;

            // Sliced/Tiled �̏ꍇ�� spriteRenderer.size ���u�����ڂ̃T�C�Y�v
            Vector2 finalSize = spriteRenderer.size;

            // ���[�J����Ԃł̎l���i���S��j
            Vector3[] localCorners = new Vector3[]
            {
        new Vector3(-finalSize.x / 2f,  finalSize.y / 2f, 0), // Top Left
        new Vector3( finalSize.x / 2f,  finalSize.y / 2f, 0), // Top Right
        new Vector3(-finalSize.x / 2f, -finalSize.y / 2f, 0), // Bottom Left
        new Vector3( finalSize.x / 2f, -finalSize.y / 2f, 0), // Bottom Right
            };

            // ���[���h���W�ɕϊ��i��]��Transform�������Ă�̂�OK�j
            Vector3[] worldCorners = new Vector3[CornerCount];
            for (int i = 0; i < CornerCount; i++)
                worldCorners[i] = t.TransformPoint(localCorners[i]);

            // UI�n���h�����X�V
            SetHandlePosition(topLeftHandle.ThisRect, worldCorners[0]);
            SetHandlePosition(topRightHandle.ThisRect, worldCorners[1]);
            SetHandlePosition(bottomLeftHandle.ThisRect, worldCorners[2]);
            SetHandlePosition(bottomRightHandle.ThisRect, worldCorners[3]);

            OnResize?.Invoke();
        }

        void SetHandlePosition(RectTransform handle, Vector3 worldPos)
        {
            Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(worldCamera, worldPos);

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvas.transform as RectTransform,
                screenPoint,
                canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : worldCamera,
                out Vector2 localPoint
            );

            handle.anchoredPosition = localPoint;
        }
        [SerializeField] private bool isRotate = false;
        private float rotationSpeed = 0.3f; // �D�݂ɍ��킹�Ē���

        private Vector3 prevMousePos;
        public void RotateDown()
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = 0f;
            isRotate = true;
            prevMousePos = Input.mousePosition;
        }
        public void RotateUp()
        {
            isRotate = false;
        }
        public void RotateDrag()
        {
            // ���[���h���W�̃}�E�X�ʒu���擾�i�J��������̃X�N���[���ϊ��j
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = 0f; // 2D�Ȃ�Z�͌Œ�

            // �����̈ʒu����}�E�X�ւ̃x�N�g�����v�Z
            Vector3 dir = mouseWorldPos - spriteRenderer.transform.position;

            // �p�x�����߂�i���W�A�����x�j
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            // Z����]��ݒ�i�u����}�E�X�����ɂ���v�Ȃ� -90 �x�␳�j
            spriteRenderer.transform.rotation = Quaternion.Euler(0, 0, angle - RotationOffsetDegrees);
        }

        public void Delete()
        {
            Destroy(spriteRenderer.gameObject);
            SpriteSet(null, false);


            StartCoroutine(WaitForFrame());
            Debug.Log("Delete");
        }
        IEnumerator WaitForFrame()
        {
            yield return new WaitForEndOfFrame();
            objects.UpdateList();
        }

    }
}
