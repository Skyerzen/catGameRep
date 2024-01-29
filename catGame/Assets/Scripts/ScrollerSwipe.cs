using UnityEngine;
using System.Collections;

    public enum Swipe { None, Up, Down, Left, Right };

    public class ScrollerSwipe : MonoBehaviour
    {
        public float minSwipeLength = 5f;//minimal lenght to determine if it's a swipe or a tap.
        Vector2 firstPressPos;//for the mobile device
        Vector2 secondPressPos;

        Vector2 firstClickPos;//for the mouse
        Vector2 secondClickPos;

        Vector2 currentSwipe;

        public static Swipe swipeDirection;

        float pageDistance = 0f;

        void Update()
        {
            DetectSwipe();
            scrollMove();
        }

    void scrollMove()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(pageDistance * -1, 0f, 0f), 8 * Time.deltaTime);
    }

        public void DetectSwipe()
        {
            if (Input.touches.Length > 0)
            {
                Touch t = Input.GetTouch(0);

                if (t.phase == TouchPhase.Began)
                {
                    firstPressPos = new Vector2(t.position.x, t.position.y);
                }

                if (t.phase == TouchPhase.Ended)
                {
                    secondPressPos = new Vector2(t.position.x, t.position.y);
                    currentSwipe = new Vector3(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

                    // Make sure it was a legit swipe, not a tap
                    if (currentSwipe.magnitude < minSwipeLength)
                    {
                        swipeDirection = Swipe.None;
                        return;
                    }

                    SwipeCommand();
                }
            }
            else
            {

                if (Input.GetMouseButtonDown(0))
                {
                    firstClickPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                }
                else {
                    swipeDirection = Swipe.None;
                    //Debug.Log ("None");
                }
                if (Input.GetMouseButtonUp(0))
                {
                    secondClickPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                    currentSwipe = new Vector3(secondClickPos.x - firstClickPos.x, secondClickPos.y - firstClickPos.y);

                    // Make sure it was a legit swipe, not a tap
                    if (currentSwipe.magnitude < minSwipeLength)
                    {
                        swipeDirection = Swipe.None;
                        return;
                    }

                    SwipeCommand();
                }
            }

        }

        void SwipeCommand()
        {
        currentSwipe.Normalize();
        if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
        {
            swipeDirection = Swipe.Left;
            pageDistance = Mathf.Clamp(pageDistance + 800f,-800f,800);
        }
        else if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
        {
            swipeDirection = Swipe.Right;
            pageDistance = Mathf.Clamp(pageDistance - 800f,-800f,1600);
        }
    }
}