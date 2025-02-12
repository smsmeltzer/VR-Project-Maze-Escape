using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ReadyPlayerMe.AvatarCreator
{
    /// <summary>
    /// This class provides all the functionality required to create a basic Account Creation UI element
    /// It allows users to input an email address, send an email, and continue without signup.
    /// </summary>
    public class SignupElement : MonoBehaviour
    {
        [SerializeField] private InputField emailField;
        [SerializeField] private Button sendEmailButton;
        [SerializeField] private Button continueWithoutSignupButton;

        // Event invoked when the "Send Email" button is clicked with the entered email address.
        public UnityEvent<string> OnSendEmail;

        // Event invoked when the "Continue Without Signup" button is clicked.
        public UnityEvent OnContinueWithoutSignup;

        private void OnEnable()
        {
            emailField.onValueChanged.AddListener(OnEmailChanged);
            sendEmailButton.onClick.AddListener(OnSendEmailButton);
            continueWithoutSignupButton.onClick.AddListener(OnContinueWithoutSignupButton);
        }

        private void OnDisable()
        {
            emailField.onValueChanged.RemoveListener(OnEmailChanged);
            sendEmailButton.onClick.RemoveListener(OnSendEmailButton);
            continueWithoutSignupButton.onClick.RemoveListener(OnContinueWithoutSignupButton);
        }

        private void OnEmailChanged(string newEmailValue)
        {
            sendEmailButton.interactable = !string.IsNullOrEmpty(newEmailValue) && ValidatorUtil.IsValidEmail(newEmailValue);
        }

        private void OnSendEmailButton()
        {
            var email = emailField.text;
            AuthManager.Signup(email);
            OnSendEmail?.Invoke(email);
            gameObject.SetActive(false);
        }

        private void OnContinueWithoutSignupButton()
        {
            OnContinueWithoutSignup?.Invoke();
            gameObject.SetActive(false);
        }
    }
}
