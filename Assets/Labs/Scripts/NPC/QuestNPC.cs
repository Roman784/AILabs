using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Gameplay
{
    public class QuestNPC : MonoBehaviour
    {
        [SerializeField] private ItemType[] _requiredItems;
        [SerializeField] private TMP_Text _messageView;

        [Space]

        [SerializeField] private string _welcomeMessage;
        [SerializeField] private string _farewellMessage;
        [SerializeField] private string _questMessage;
        [SerializeField] private string _refusalMessage;

        private bool _isQuestStarted;
        private bool _isQuestFinished;
        private int _currentRequiredItemIdx;

        private ItemType CurrentQuestItemType => _requiredItems[_currentRequiredItemIdx];

        private void Start()
        {
            _messageView.gameObject.SetActive(false);
        }

        public void OnPlayerEnter()
        {
            if (_isQuestStarted) return;

            _messageView.text = "";
            _messageView.gameObject.SetActive(true);

            if (!_isQuestStarted && !_isQuestFinished)
            {
                PringMessage(_welcomeMessage);
            }
        }

        public void OnPlayerExit()
        {
            if (!_isQuestStarted || _isQuestFinished)
                _messageView.gameObject.SetActive(false);
        }

        public void Interact(PlayerController player)
        {
            if (!_isQuestStarted)
                StartQuest();
            else if (!_isQuestFinished)
                GiveItem(player.HoldingItem);
        }

        private void StartQuest()
        {
            _isQuestStarted = true;
            ShowCurrentItemMessage(_questMessage);
        }

        private void FinishQuest()
        {
            _isQuestFinished = true;
            PringMessage(_farewellMessage);
        }

        public void GiveItem(Item item)
        {
            if (!_isQuestStarted) return;

            if (item.Type != CurrentQuestItemType)
            {
                ShowCurrentItemMessage(_refusalMessage);
                return;
            }

            Destroy(item.gameObject);
            _currentRequiredItemIdx++;

            if (_currentRequiredItemIdx >= _requiredItems.Length)
                FinishQuest();
            else
                ShowCurrentItemMessage(_questMessage);
        }

        private void ShowCurrentItemMessage(string otherText)
        {
            var message = otherText.Replace("<item_name>", CurrentQuestItemType.ToString());
            PringMessage(message);
        }

        private void PringMessage(string message) => StartCoroutine(PringMessageRoutine(message));
        private IEnumerator PringMessageRoutine(string targetMessage)
        {
            var currentMessage = _messageView.text;

            while (currentMessage.Length > 0)
            {
                currentMessage = currentMessage[0..^1];
                _messageView.text = currentMessage;

                yield return new WaitForSeconds(0.001f);
            }

            currentMessage = "";

            for (int i = 0; i < targetMessage.Length; i++)
            {
                currentMessage += targetMessage[i];
                _messageView.text = currentMessage;

                yield return new WaitForSeconds(0.01f);
            }
        }
    }
}
