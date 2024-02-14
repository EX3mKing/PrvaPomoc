using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swipe : MonoBehaviour
{
    private Vector2 touchStart;
    private Vector2 touchEnd;
    [SerializeField] private float swipeThreshold = 100f;

    public List<GameObject> pages;
    private int currentPage = 0;
    private void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            touchStart = Input.GetTouch(0).position;
        }
        
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            touchEnd = Input.GetTouch(0).position;

            float swipeLen = touchEnd.x - touchStart.x; 

            if (swipeLen < -swipeThreshold)
            {
                SwipeL2R();
            }
            else if (swipeLen > swipeThreshold)
            {
                SwipeR2L();
            }
        }
    }

    private void SwipeR2L()
    {
        Debug.Log("R2L");
        if (currentPage > 0) currentPage--;
        foreach (var page in pages)
        {
            page.SetActive(false);
        }
        pages[currentPage].SetActive(true);
    }
    
    private void SwipeL2R()
    {
        Debug.Log("L2R");
        if(currentPage + 1 < pages.Count) currentPage++;
        foreach (var page in pages)
        {
            page.SetActive(false);
        }
        pages[currentPage].SetActive(true);
    }
}
