using System.IO;
using System.Xml.Serialization;
using UnityEngine;

namespace Gameplay
{
    public class DialogProvider : MonoBehaviour
    {
        [SerializeField] private DialogView _view;

        private Dialog _dialog;
        private Node _currentNode;

        private void Start()
        {
            _view.OnNextNodeButtonClickSignal.AddListener(() 
                => ShowNextNode(_currentNode.NextNodeID));

            _view.OnResponseSelectSignal.AddListener((response) 
                => ShowNextNode(response.NextNodeID));

            LoadDialog("dialog");
        }

        public void LoadDialog(string dialogID)
        {
            var xmlFile = Resources.Load<TextAsset>($"{dialogID}");

            if (xmlFile == null)
            {
                Debug.LogError($"Диалог с ID '{dialogID}' не найден!");
                return;
            }

            var serializer = new XmlSerializer(typeof(Dialog));
            using (StringReader reader = new StringReader(xmlFile.text))
            {
                _dialog = (Dialog)serializer.Deserialize(reader);
            }

            StartDialog();
        }

        private void StartDialog()
        {
            if (_dialog == null) return;

            _currentNode = _dialog.Nodes.Find(n => n.ID == "0");
            if (_currentNode == null) return;

            ShowCurrentNode();
        }

        private void ShowCurrentNode()
        {
            var dialogueText = _currentNode.DialogueText.Text;

            _view.SetResponses(_currentNode.HasResponses);
            _view.SetNextNodeButton(!_currentNode.HasResponses);
            
            _view.ShowMainText(dialogueText);
            if (_currentNode.HasResponses)
                _view.ShowResponses(_currentNode.Responses);
        }

        private void ShowNextNode(string nextNodeId)
        {
            if (nextNodeId == "end")
            {
                EndDialog();
                return;
            }

            _currentNode = _dialog.Nodes.Find(n => n.ID == nextNodeId);

            ShowCurrentNode();
        }

        private void EndDialog()
        {
            ShowNextNode("0");
        }
    }
}
