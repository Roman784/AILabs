using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Gameplay
{
    public class DialogView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _mainTextView;
        [SerializeField] private GameObject _nextNodeButton;
        [SerializeField] private ResponseView  _responseViewPrefab;
        [SerializeField] private Transform _responsesContainer;

        private List<ResponseView> _responseViews = new();

        private Coroutine _printRoutine;

        public UnityEvent OnNextNodeButtonClickSignal = new();
        public UnityEvent<Response> OnResponseSelectSignal = new();

        public void ShowMainText(string text)
        {
            if (_printRoutine != null)
                StopCoroutine(_printRoutine);
            _printRoutine = StartCoroutine(PrintTextRoutine(_mainTextView, text));
        }

        public void ShowResponses(List<Response> responses)
        {
            foreach (var view in _responseViews)
                Destroy(view.gameObject);

            _responseViews.Clear();

            foreach (var response in responses)
                CreateResponse(response);
        }

        public void SetResponses(bool isActive)
        {
            _responsesContainer.gameObject.SetActive(isActive);
        }

        public void SetNextNodeButton(bool isActive)
        {
            _nextNodeButton.SetActive(isActive);
        }

        public void ShowNextNode()
        {
            OnNextNodeButtonClickSignal.Invoke();
        }

        private void CreateResponse(Response response)
        {
            var createdView = Instantiate(_responseViewPrefab, _responsesContainer);

            createdView.Show(response.Text);
            createdView.OnSelectSignal.AddListener(() =>
                OnResponseSelectSignal.Invoke(response));

            _responseViews.Add(createdView);
        }

        public static IEnumerator PrintTextRoutine(TMP_Text view, string targetMessage)
        {
            var currentMessage = "";
            view.text = currentMessage;

            for (int i = 0; i < targetMessage.Length; i++)
            {
                currentMessage += targetMessage[i];
                view.text = currentMessage;

                yield return new WaitForSeconds(0.01f);
            }
        }
    }
}
