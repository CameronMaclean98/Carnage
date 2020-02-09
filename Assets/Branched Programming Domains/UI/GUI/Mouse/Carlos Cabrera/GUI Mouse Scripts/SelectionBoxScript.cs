using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class SelectionBoxScript : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    Image selectionboximage;

    [SerializeField]
    Image selectionboximage2;

    Vector2 startingPosition;

    Rect rectSelection;
    Rect rectSelection2;

    public void OnBeginDrag(PointerEventData eventData)
    {
        selectionboximage.gameObject.SetActive(true);
        startingPosition = eventData.position;
        rectSelection = new Rect();

        selectionboximage2.gameObject.SetActive(true);
        //startingPosition = eventData.position;
        rectSelection2 = new Rect();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(eventData.position.x < startingPosition.x)
        {
            rectSelection.xMin = eventData.position.x;
            rectSelection.xMax = startingPosition.x;

            rectSelection2.xMin = eventData.position.x;
            rectSelection2.xMax = startingPosition.x;
        }
        else
        {
            rectSelection.xMin = startingPosition.x;
            rectSelection.xMax = eventData.position.x;

            rectSelection2.xMin = startingPosition.x;
            rectSelection2.xMax = eventData.position.x;
        }

        if (eventData.position.y < startingPosition.y)
        {
            rectSelection.yMin = eventData.position.y;
            rectSelection.yMax = startingPosition.y;

            rectSelection2.yMin = eventData.position.y;
            rectSelection2.yMax = startingPosition.y;
        }
        else
        {
            rectSelection.yMin = startingPosition.y;
            rectSelection.yMax = eventData.position.y;

            rectSelection2.yMin = startingPosition.y;
            rectSelection2.yMax = eventData.position.y;
        }

        selectionboximage.rectTransform.offsetMin = rectSelection.min;
        selectionboximage.rectTransform.offsetMax = rectSelection.max;

        selectionboximage2.rectTransform.offsetMin = rectSelection2.min;
        selectionboximage2.rectTransform.offsetMax = rectSelection2.max;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        selectionboximage.gameObject.SetActive(false);
        selectionboximage2.gameObject.SetActive(false);
    }
}
