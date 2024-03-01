using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// ova klasa se koristi za kontroliranje aplikacije
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
        // Provjerava se ima li dodira na zaslonu i je li to početak dodira.
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            // Ako je to početak dodira, bilježi se početna pozicija dodira.
            touchStart = Input.GetTouch(0).position;
        }
        
        // Provjerava se ima li dodira na zaslonu i je li to završetak dodira.
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            // Ako je to završetak dodira, bilježi se krajnja pozicija dodira.
            touchEnd = Input.GetTouch(0).position;

            // Izračunava se duljina poteza po x osi.
            float swipeLen = touchEnd.x - touchStart.x;

            // Provjerava se duljina poteza u odnosu na pragovnu vrijednost.
            if (swipeLen < -swipeThreshold)
            {
                // Ako je duljina poteza manja od negativnog praga, izvršava se potez s lijeva na desno.
                SwipeL2R();
            }
            else if (swipeLen > swipeThreshold)
            {
                // Ako je duljina poteza veća od praga, izvršava se potez s desna na lijevo.
                SwipeR2L();
            }
        }
    }

    // provjerava listamo li u opisu koraka i onda prebacije na slijedeći sadržaj
    private void SwipeR2L()
    {
        Debug.Log("R2L");
        if (SwipingInDescription) { if (currentInfo > 0) SwipeDescription(-1); }
        else if (currentPage > 0) SwipePage(-1);
    }
    
    // provjerava listamo li u opisu koraka i onda prebacije na slijedeći sadržaj
    private void SwipeL2R()
    {
        Debug.Log("L2R");
        if (SwipingInDescription)
        { if (currentInfo + 1 < curInfo.information.Count) SwipeDescription(1); }
        else if(currentPage + 1 < pages.Count) SwipePage(1);
    }

    // provjerava listamo li u katalogu i onda prebacije na slijedeći sadržaj
    private void SwipePage(int amount)
    {
        currentPage+= amount;
        foreach (var page in pages) page.SetActive(false); 
        pages[currentPage].SetActive(true);
    }

    // zamijenjuje UI elemente unutar opisa koraka prilikom klizanja prsta
    private void SwipeDescription(int amount)
    {
        currentInfo += amount;
        descriptionTitle.text = curInfo.title[currentInfo];
        descriptionImage.sprite = curInfo.image[currentInfo];
        descriptionText.text = curInfo.information[currentInfo];
        descriptionNumberText.text = currentInfo + 1 + "/" + curInfo.information.Count;
    }
    
    // pritiskom na gumb učitava potrebne UI elemente za opis koraka
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
    
    // nazad učitava glavni katalog 
    public void ToMainMenu()
    {
        GuideCanvas.SetActive(false);
        pages[currentPage].SetActive(true);
        curInfo = null;
        SwipingInDescription = false;
        currentInfo = 0;
    }
}
