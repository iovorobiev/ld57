using System;
using NUnit.Framework.Internal;
using TMPro;
using UnityEngine;

namespace GameEngine.Tutorial
{
    public class TutorialView : MonoBehaviour
    {
        public RectTransform _rectTransform;
        public GameObject tutorialParent;
        public GameObject arrow;
        public TMP_Text text;

        public bool shown;
        public void Start()
        {
            Game.tutorialView = this;
        }

        public void showAt(Rect rect, TutorialSequence.ArrowState arrowState, string hintText)
        {
            shown = true;
            tutorialParent.SetActive(true);
            if (hintText != "")
            {
                text.gameObject.SetActive(true);
            }
            
            _rectTransform.position = rect.position;
            _rectTransform.sizeDelta = rect.size;

            _rectTransform.gameObject.SetActive(true);
            arrow.SetActive(true);
            if (hintText == "")
            {
                text.gameObject.SetActive(false);
            }
            else
            {
                text.gameObject.SetActive(true);
                text.text = hintText;
            }

            if (arrowState == TutorialSequence.ArrowState.ABOVE)
            {
                arrow.transform.localScale = new Vector3(arrow.transform.localScale.x,
                    Mathf.Abs(arrow.transform.localScale.y), arrow.transform.localScale.z);
                arrow.transform.position = new Vector2(rect.position.x, rect.position.y + rect.size.y / 2f);
                text.transform.position = new Vector2(text.transform.position.x, arrow.transform.position.y + 3f);
            }
            else if (arrowState == TutorialSequence.ArrowState.BELOW)
            {
                arrow.transform.localScale = new Vector3(arrow.transform.localScale.x,
                    -Mathf.Abs(arrow.transform.localScale.y), arrow.transform.localScale.z);
                arrow.transform.position = new Vector2(rect.position.x, rect.position.y - rect.size.y / 2f);
                text.transform.position = new Vector2(text.transform.position.x, arrow.transform.position.y - 3f);
            }
            else
            {
                text.transform.position = new Vector3(0f, 0f, text.transform.position.z);
                arrow.SetActive(false);
            }
        }

        public void hide()
        {
            shown = false;
            tutorialParent.SetActive(false);
            text.gameObject.SetActive(false);
            arrow.SetActive(false);
        }
    }
}