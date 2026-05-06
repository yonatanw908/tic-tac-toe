using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TicTacToe
{
    public class ResultPopup : MonoBehaviour
    {
        [SerializeField] private GameObject _root;
        [SerializeField] private TMP_Text _resultText;
        [SerializeField] private Button _newGameButton;
        [SerializeField] private Button _undoGameButton;

        private void Awake()
        {
            _newGameButton.onClick.AddListener(OnNewGameClicked);
            _undoGameButton.onClick.AddListener(CommandListManager.UndoCommand);
            _root.SetActive(false);
        }

        private void OnEnable()
        {
            GameEvents.ResultReady += OnResultReady;
            GameEvents.UndoGameOver += Hide;
        }

        private void OnDisable()
        {
            GameEvents.ResultReady -= OnResultReady;
            GameEvents.UndoGameOver -= Hide;
        }

        private void OnResultReady(string message)
        {
            _resultText.text = message;
            _root.SetActive(true);
        }

        private void Hide()
        {
            _root.SetActive(false);
        }

        private void OnNewGameClicked()
        {
            GameEvents.RestartGame?.Invoke();
            Hide();
        }
    }
}
