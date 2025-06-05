using System;
using Cysharp.Threading.Tasks;
using NUnit.Framework.Internal;
using TMPEffects.Components;
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

        private TMPWriter writer;
        
        public bool shown;
        public void Start()
        {
            Game.tutorialView = this;
            writer = text.gameObject.GetComponent<TMPWriter>();
        }

        public async UniTask showAt(Rect rect, TutorialSequence.ArrowState arrowState, string hintText, Vector3? textPosition = null)
        {
            shown = true;
            tutorialParent.SetActive(true);
            
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
            } else if (arrowState == TutorialSequence.ArrowState.JUST_ARROW)
            {
                arrow.transform.localScale = new Vector3(arrow.transform.localScale.x,
                    Mathf.Abs(arrow.transform.localScale.y), arrow.transform.localScale.z);
                arrow.transform.position = new Vector2(rect.position.x, rect.position.y + rect.size.y / 2f);
                tutorialParent.SetActive(false);
            }
            else
            {
                if (textPosition != null)
                {
                    text.transform.position = (Vector3) textPosition;
                }
                else
                {
                    text.transform.position = new Vector3(0f, rect.position.y + rect.size.y / 2f, text.transform.position.z);
                }
                arrow.SetActive(false);
            }

            await UniTask.WhenAny(UniTask.WaitUntil(() => !writer.IsWriting), UniTask.WaitUntil(() => Input.GetMouseButtonDown(0)));
            writer.SkipWriter();
            await UniTask.WaitForSeconds(0.1f);
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