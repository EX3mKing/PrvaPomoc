using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    private Vector2 touchStart;
    private Vector2 touchEnd;
    [SerializeField] private float swipeThreshold = 100f;

    public List<GameObject> pages;
    private int currentPage = 0;

    public GameObject GuideCanvas;
    public Image descriptionImage;
    public TextMeshProUGUI descriptionTitle;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI descriptionNumberText;
    private int currentInfo = 0;

    private bool SwipingInDescription = false;
    private Information curInfo;

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
        if (SwipingInDescription) { if (currentInfo > 0) SwipeDescription(-1); }
        else if (currentPage > 0) SwipePage(-1);
        
        
    }
    
    private void SwipeL2R()
    {
        Debug.Log("L2R");
        if (SwipingInDescription)
        { if (currentInfo + 1 < curInfo.information.Count) SwipeDescription(1); }
        else if(currentPage + 1 < pages.Count) SwipePage(1);
    }

    private void SwipePage(int amount)
    {
        currentPage+= amount;
        foreach (var page in pages) page.SetActive(false); 
        pages[currentPage].SetActive(true);
    }

    private void SwipeDescription(int amount)
    {
        currentInfo += amount;
        descriptionTitle.text = curInfo.title[currentInfo];
        descriptionImage.sprite = curInfo.image[currentInfo];
        descriptionText.text = curInfo.information[currentInfo];
        descriptionNumberText.text = currentInfo + 1 + "/" + curInfo.information.Count;
    }
    
    public void LoadInfo(Information info)
    {
        GuideCanvas.SetActive(true);
        pages[currentPage].SetActive(false);
        curInfo = info;
        descriptionTitle.text = curInfo.title[currentInfo];
        descriptionImage.sprite = curInfo.image[currentInfo];
        descriptionText.text = curInfo.information[currentInfo];
        SwipingInDescription = true;
        descriptionNumberText.text = currentInfo + 1 + "/" + curInfo.information.Count;
    }
    
    public void ToMainMenu()
    {
        GuideCanvas.SetActive(false);
        pages[currentPage].SetActive(true);
        curInfo = null;
        SwipingInDescription = false;
        currentInfo = 0;
    }
}
