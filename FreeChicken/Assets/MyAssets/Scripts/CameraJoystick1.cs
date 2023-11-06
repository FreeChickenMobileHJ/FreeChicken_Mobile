using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraJoystick1 : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private RectTransform lever;
    private RectTransform rectTransform;

    [SerializeField]
    private Canvas mainCanvas;

    [SerializeField, Range(10, 150)]
    private float leverRange;

    private Vector2 inputDirection;
    private bool isInput;

    [SerializeField]
    private HouseScenePlayer housePlayer1;

    public float sensitivity = 1f;

    public enum JoysitckType { Rotate }
    public JoysitckType joystickType;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        ControlJoystickLever(eventData);
        isInput = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        ControlJoystickLever(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        lever.anchoredPosition = Vector2.zero;
        isInput = false;
        switch (joystickType)
        {
            case JoysitckType.Rotate:
                housePlayer1.Move(Vector2.zero);
                break;
        }
    }

    private void ControlJoystickLever(PointerEventData eventData)
    {
        var scaledAnchoredPosition = rectTransform.anchoredPosition * mainCanvas.transform.localScale.x;
        // 오른쪽 중앙을 기준으로 조절
        var inputPos = eventData.position - new Vector2(mainCanvas.pixelRect.width, mainCanvas.pixelRect.height) / 2 - scaledAnchoredPosition;
        var inputVector = inputPos.magnitude < leverRange ? inputPos : inputPos.normalized * leverRange;

        lever.anchoredPosition = inputVector;

        inputDirection = inputVector.normalized;
    }

    private void InputControlVector()
    {
        if (isInput && !housePlayer1.isOpeningDoor && !housePlayer1.isRaisingDoor)  // 조이스틱 입력을 받는 동안에만 플레이어 움직임을 처리
        {
            switch (joystickType)
            {
                case JoysitckType.Rotate:
                    housePlayer1.Move(Vector3.zero);
                    housePlayer1.isMove = false;
                    housePlayer1.LookAround(inputDirection * sensitivity);
                    break;
            }
        }
    }


    void Update()
    {
        if (!housePlayer1.isTalk || housePlayer1.TalkEnd)
        {
            InputControlVector();

        }
        else if (housePlayer1.isTalk || !housePlayer1.TalkEnd)
        {
            isInput = false;
        }
    }
}
