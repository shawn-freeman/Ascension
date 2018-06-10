using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpgradeAssignmentHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler{
    public static GameObject draggingObject;
    public Vector3 startPosition;
    public GameObject myCanvas;

    public void OnBeginDrag(PointerEventData eventData)
    {
        draggingObject = gameObject;
        startPosition = transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos;
        var cam = myCanvas.GetComponent<Camera>();
        RectTransformUtility.ScreenPointToLocalPointInRectangle(myCanvas.transform as RectTransform, Input.mousePosition, cam, out pos);
        transform.position = myCanvas.transform.TransformPoint(pos);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        draggingObject = null;
        transform.position = startPosition;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
