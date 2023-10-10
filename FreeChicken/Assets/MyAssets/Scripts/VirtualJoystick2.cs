using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class VirtualJoystick2 : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
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
    private HouseScene2_Player housePlayer2;

    public float sensitivity = 1f;

    public enum JoysitckType { Move, Rotate }
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
            case JoysitckType.Move:
                housePlayer2.Move(Vector2.zero);
                break;
            case JoysitckType.Rotate:
                housePlayer2.Move(Vector2.zero);
                break;
        }
    }

    private void ControlJoystickLever(PointerEventData eventData)
    {
        var scaledAnchoredPosition = rectTransform.anchoredPosition * mainCanvas.transform.localScale.x;
        var inputPos = eventData.position - scaledAnchoredPosition;
        var inputVector = inputPos.magnitude < leverRange ? inputPos : inputPos.normalized * leverRange;
        lever.anchoredPosition = inputVector;

        inputDirection = inputVector.normalized;
    }

    private void InputControlVector()
    {
        if (isInput)  // 조이스틱 입력을 받는 동안에만 플레이어 움직임을 처리
        {
            switch (joystickType)
            {
                case JoysitckType.Move:
                    housePlayer2.Move(inputDirection * sensitivity);
                    break;
                case JoysitckType.Rotate:
                    housePlayer2.Move(Vector3.zero);
                    housePlayer2.isMove = false;
                    housePlayer2.LookAround(inputDirection * sensitivity);
                    break;
            }
        }
    }

    void Start()
    {

    }

    void Update()
    {
        if (!housePlayer2.isTalk1 || housePlayer2.TalkEnd1 || !housePlayer2.isTalk2 || housePlayer2.TalkEnd2)
        {
            InputControlVector();

        }
        else if (housePlayer2.isTalk1 || !housePlayer2.TalkEnd1 || housePlayer2.isTalk2 || !housePlayer2.TalkEnd2)
        {
            isInput = false;
        }
    }
}
