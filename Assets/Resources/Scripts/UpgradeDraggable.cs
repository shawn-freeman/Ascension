using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpgradeDraggable : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler {
    public Camera cam;
    private RectTransform DragIcon;

	// Use this for initialization
	void Start () {
		
	}

    void OnMouseDown()
    {
        Debug.Log("Dragable has been Clicked!");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        DragIcon = PoolManager.GetObject(LoadedAssets.UPGRADE_DRAGDROP_PREFAB).GetComponent<RectTransform>();
        DragIcon.SetParent(gameObject.GetComponentInParent<Canvas>().transform, false);
        DragIcon.SetAsLastSibling();

        Vector3 mousePos;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(gameObject.GetComponentInParent<Canvas>().GetComponent<RectTransform>(), Input.mousePosition, cam, out mousePos);
        //var mousePos = Input.mousePosition;// + gameObject.GetComponentInParent<Canvas>().GetComponent<RectTransform>().localPosition;

        DragIcon.localPosition = new Vector3(mousePos.x, mousePos.y, 0);

        //var pos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
        //pos.z = 10.0f;

        //DragIcon.transform.localPosition = Camera.main.ScreenToWorldPoint(pos);

        Debug.Log("OnBeginDrag");
    }

    private void SetDraggingPosition(PointerEventData eventData)
    {
        //if (eventData.pointerEnter != null && eventData.pointerEnter.transform as RectTransform != null)
        //    DragIcon.transform = eventData.pointerEnter.transform as RectTransform;

        Vector3 globalMousePos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(eventData.pointerEnter.transform as RectTransform, eventData.position, eventData.pressEventCamera, out globalMousePos))
        {
            DragIcon.position = globalMousePos;
            DragIcon.rotation = gameObject.GetComponentInParent<Canvas>().GetComponent<RectTransform>().rotation;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {

    }

    // Update is called once per frame
    void Update () {
        if (DragIcon != null)
        {

            
            //this is your object that you want to have the UI element hovering over
            //Vector3 WorldObject = Input.mousePosition;

            ////this is the ui element
            //RectTransform UI_Element;

            ////first you need the RectTransform component of your canvas
            //RectTransform CanvasRect = gameObject.GetComponentInParent<Canvas>().GetComponent<RectTransform>();

            ////then you calculate the position of the UI element
            ////0,0 for the canvas is at the center of the screen, whereas WorldToViewPortPoint treats the lower left corner as 0,0. Because of this, you need to subtract the height / width of the canvas * 0.5 to get the correct position.


            //Vector2 ViewportPosition = cam.WorldToViewportPoint(WorldObject);
            //Vector2 WorldObject_ScreenPosition = new Vector3(
            //((ViewportPosition.x * CanvasRect.sizeDelta.x) - (CanvasRect.sizeDelta.x * 0.5f)),
            //((ViewportPosition.y * CanvasRect.sizeDelta.y) - (CanvasRect.sizeDelta.y * 0.5f)), 0);

            ////now you can set the position of the ui element
            //DragIcon.transform.position = WorldObject_ScreenPosition;
            //var mousePos = Input.mousePosition - gameObject.GetComponentInParent<Canvas>().GetComponent<RectTransform>().localPosition;

            //DragIcon.transform.localPosition = new Vector3(mousePos.x, mousePos.y, 0);

            //var pos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
            //pos.z = 10.0f;

            //DragIcon.transform.position = Camera.main.ScreenToWorldPoint(pos);
            //DragIcon.trans

            //Debug.Log(string.Format("{0} | {1}", DragIcon.transform.position, Input.mousePosition));
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Destroy(DragIcon);
        DragIcon = null;

    }

    public void OnDrag(PointerEventData eventData)
    {
        
    }

    private Vector2 WorldToCanvasPosition(RectTransform canvas, Camera camera, Vector3 position)
    {
        //Vector position (percentage from 0 to 1) considering camera size.
        //For example (0,0) is lower left, middle is (0.5,0.5)
        Vector2 temp = camera.WorldToViewportPoint(position);

        //Calculate position considering our percentage, using our canvas size
        //So if canvas size is (1100,500), and percentage is (0.5,0.5), current value will be (550,250)
        temp.x *= canvas.sizeDelta.x;
        temp.y *= canvas.sizeDelta.y;

        //The result is ready, but, this result is correct if canvas recttransform pivot is 0,0 - left lower corner.
        //But in reality its middle (0.5,0.5) by default, so we remove the amount considering cavnas rectransform pivot.
        //We could multiply with constant 0.5, but we will actually read the value, so if custom rect transform is passed(with custom pivot) , 
        //returned value will still be correct.

        temp.x -= canvas.sizeDelta.x * canvas.pivot.x;
        temp.y -= canvas.sizeDelta.y * canvas.pivot.y;

        return temp;
    }
}
