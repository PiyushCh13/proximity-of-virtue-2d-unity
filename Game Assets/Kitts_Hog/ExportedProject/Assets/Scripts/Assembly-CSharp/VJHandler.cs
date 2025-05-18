using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VJHandler : MonoBehaviour, IDragHandler, IEventSystemHandler, IPointerUpHandler, IPointerDownHandler
{
	private Image jsContainer;

	private Image joystick;

	public Vector3 InputDirection;

	private void Start()
	{
		jsContainer = GetComponent<Image>();
		joystick = base.transform.GetChild(0).GetComponent<Image>();
		InputDirection = Vector3.zero;
	}

	public void OnDrag(PointerEventData ped)
	{
		Vector2 localPoint = Vector2.zero;
		RectTransformUtility.ScreenPointToLocalPointInRectangle(jsContainer.rectTransform, ped.position, ped.pressEventCamera, out localPoint);
		localPoint.x /= jsContainer.rectTransform.sizeDelta.x;
		localPoint.y /= jsContainer.rectTransform.sizeDelta.y;
		float x = ((jsContainer.rectTransform.pivot.x == 1f) ? (localPoint.x * 2f + 1f) : (localPoint.x * 2f - 1f));
		float y = ((jsContainer.rectTransform.pivot.y == 1f) ? (localPoint.y * 2f + 1f) : (localPoint.y * 2f - 1f));
		InputDirection = new Vector3(x, y, 0f);
		InputDirection = ((InputDirection.magnitude > 1f) ? InputDirection.normalized : InputDirection);
		joystick.rectTransform.anchoredPosition = new Vector3(InputDirection.x * (jsContainer.rectTransform.sizeDelta.x / 3f), InputDirection.y * jsContainer.rectTransform.sizeDelta.y / 3f);
	}

	public void OnPointerDown(PointerEventData ped)
	{
		OnDrag(ped);
	}

	public void OnPointerUp(PointerEventData ped)
	{
		InputDirection = Vector3.zero;
		joystick.rectTransform.anchoredPosition = Vector3.zero;
	}
}
