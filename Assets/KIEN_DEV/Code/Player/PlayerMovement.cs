using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    public float min = -14f;
    public float max = 14f;
    public float moveSpeed = 10f;

    private Vector3 moveDirection = Vector3.zero;

    private Vector2 startTouchPosition;
    private bool isTouching = false;
    public float swipeThreshold = 30f; // ngưỡng kéo nhẹ để chuyển hướng

    void Update()
    {
        if (DeviceChecker.Instance.IsPC())
        {
            Pc_Move();
        }
        else
        {
            Mobile_Move();
        }

        // Di chuyển liên tục theo hướng đã set
        Vector3 newPosition = transform.position + moveDirection * moveSpeed * Time.deltaTime;
        newPosition.x = Mathf.Clamp(newPosition.x, min, max);
        transform.position = newPosition;
    }

    void Pc_Move()
    {
        moveDirection = Vector3.zero;

        if (Input.GetKey(KeyCode.A))
        {
            moveDirection = Vector3.left;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            moveDirection = Vector3.right;
        }
    }

    void Mobile_Move()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // ✅ Bỏ qua nếu chạm UI
            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                return;

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    isTouching = true;
                    startTouchPosition = touch.position;
                    break;

                case TouchPhase.Moved:
                    Vector2 delta = touch.position - startTouchPosition;

                    if (Mathf.Abs(delta.x) < swipeThreshold)
                    {
                        // 🟡 Nếu kéo gần về giữa → đi thẳng
                        moveDirection = Vector3.zero;
                    }
                    else
                    {
                        // 🔵 Nếu kéo đủ lệch trái/phải → chuyển hướng
                        moveDirection = delta.x > 0 ? Vector3.right : Vector3.left;
                    }
                    break;

                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    isTouching = false;
                    moveDirection = Vector3.zero; // thả tay → đi thẳng lại
                    break;
            }
        }
        else
        {
            moveDirection = Vector3.zero; // không chạm gì → reset
        }
        }
}
