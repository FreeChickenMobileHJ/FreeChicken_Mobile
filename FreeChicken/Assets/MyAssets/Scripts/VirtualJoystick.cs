using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.EventSystems;

public class VirtualJoystick : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
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
    private bool isDrag;

    [SerializeField]
    private HouseScenePlayer housePlayer1;

    public float sensitivity = 1f;

    public enum JoysitckType { Move, Rotate }
    public JoysitckType joystickType;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if(!housePlayer1.Dead && !housePlayer1.isRotating)
        {
            ControlJoystickLever(eventData);
            isInput = true;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(isInput && !housePlayer1.Dead)
        {
            ControlJoystickLever(eventData);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        lever.anchoredPosition = Vector2.zero;
        isInput = false;
        switch (joystickType)
        {
            case JoysitckType.Move:
                housePlayer1.Move(Vector2.zero);
                break;
            case JoysitckType.Rotate:
                housePlayer1.Move(Vector2.zero);
                break;
        }
    }

    private void ControlJoystickLever(PointerEventData eventData)
    {
        if(!housePlayer1.Dead && !housePlayer1.isRotating)
        {
            var scaledAnchoredPosition = rectTransform.anchoredPosition * mainCanvas.transform.localScale.x;
            var inputPos = eventData.position - scaledAnchoredPosition;
            var inputVector = inputPos.magnitude < leverRange ? inputPos : inputPos.normalized * leverRange;

            lever.anchoredPosition = inputVector;

            inputDirection = inputVector.normalized;
        }
        
    }

    private void InputControlVector()
    {
        if (!housePlayer1.Dead && !housePlayer1.isOpeningDoor && !housePlayer1.isRaisingDoor && !housePlayer1.isRotating)  // 조이스틱 입력을 받는 동안에만 플레이어 움직임을 처리
        {
            if(isInput)
            {
                switch (joystickType)
                {
                    case JoysitckType.Move:
                        housePlayer1.Move(inputDirection * sensitivity);
                        break;
                    case JoysitckType.Rotate:
                        housePlayer1.Move(Vector3.zero);
                        housePlayer1.isMove = false;
                        housePlayer1.LookAround(inputDirection * sensitivity);
                        break;
                }
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
