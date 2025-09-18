using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Gameplay
{
    public class QuestNPC : NPC
    {
        [SerializeField] private ItemType[] _requiredItems;
        [SerializeField] private TMP_Text _messageView;

        [Space]

        [SerializeField] private string _welcomeMessage;
        [SerializeField] private string _farewellMessage;
        [SerializeField] private string _questMessage;
        [SerializeField] private string _refusalMessage;

        [Space]

        [SerializeField] private Transform _cameraPont;

        private bool _isQuestStarted;
        private bool _isQuestFinished;
        private int _currentRequiredItemIdx;

        public Transform CameraPoint => _cameraPont;
        private ItemType CurrentQuestItemType => _requiredItems[_currentRequiredItemIdx];

        private void Start()
        {
            _messageView.gameObject.SetActive(false);
        }

        public override void OnPlayerEnter(PlayerController player)
        {
            base.OnPlayerEnter(player);

            RotatateTo(Quaternion.LookRotation(player.transform.position - transform.position));

            if (_isQuestStarted) return;

            _messageView.text = "";
            _messageView.gameObject.SetActive(true);

            if (!_isQuestStarted && !_isQuestFinished)
            {
                PringMessage(_welcomeMessage);
            }
        }

        public override void OnPlayerExit()
        {
            base.OnPlayerExit();

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

        private void RotatateTo(Quaternion rotation)
        {
            transform.DORotateQuaternion(rotation, 0.25f)
                .SetEase(Ease.OutQuad);
        }

        private void PringMessage(string message) => StartCoroutine(PringMessageRoutine(message));
        private IEnumerator PringMessageRoutine(string targetMessage)
        {
            var currentMessage = "";
            _messageView.text = currentMessage;

            for (int i = 0; i < targetMessage.Length; i++)
            {
                currentMessage += targetMessage[i];
                _messageView.text = currentMessage;

                yield return new WaitForSeconds(0.01f);
            }
        }
    }
}
