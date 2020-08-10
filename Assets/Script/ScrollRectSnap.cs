using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollRectSnap : MonoBehaviour
{
    public RectTransform panel;
    public Image[] images;
    public RectTransform center;

    private float[] distance;
    private bool dragging = false;
    private int imageDistance;
    private int minImageNum;

    private void Start()
    {
        int imagesLenght = images.Length;
        distance = new float[imagesLenght];

        imageDistance = (int)Mathf.Abs(images[1].GetComponent<RectTransform>().anchoredPosition.x - images[0].GetComponent<RectTransform>().anchoredPosition.x);
    }
}
