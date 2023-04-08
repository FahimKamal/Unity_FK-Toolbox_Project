using Popup_Log_System;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SaveManager
{
    public class Controller : MonoBehaviour
    {
        [SerializeField] private PopupEvent popupEvent;

        // [SerializeField] private GameObject signInBtn;
        // [SerializeField] private GameObject signOutBtn;
        // [SerializeField] private GameObject cloudSaveBtn;
        // [SerializeField] private GameObject cloudLoadBtn;
        // [SerializeField] private GameObject localLoadBtn;
        // [SerializeField] private GameObject localSaveBtn;
        [SerializeField] private TextMeshProUGUI description;

        private int mRandomInt;
        private float mRandomFloat;
        private string mRandomString;

        private void OnEnable()
        {
            // if (PlayServiceManager.Instance != null)
            // {
            // PlayServiceManager.Instance.onSignedIn += OnSignedIn;
            // PlayServiceManager.Instance.onSignedOut += OnSignedOut;
            // PlayServiceManager.Instance.onSignInFailed += OnSignInFailed;
            // PlayServiceManager.Instance.onDataSaved += OnDataSaved;
            // PlayServiceManager.Instance.onDataSaveFailed += OnDataSaveFailed;
            // PlayServiceManager.Instance.onDataLoaded += OnDataLoaded;
            // PlayServiceManager.Instance.onDataLoadFailed += OnDataLoadFailed;
            // }
        }

        private void OnDisable()
        {
            // if (PlayServiceManager.Instance != null)
            // {
            // PlayServiceManager.Instance.onSignedIn -= OnSignedIn;
            // PlayServiceManager.Instance.onSignedOut -= OnSignedOut;
            // PlayServiceManager.Instance.onSignInFailed -= OnSignInFailed;
            // PlayServiceManager.Instance.onDataSaved -= OnDataSaved;
            // PlayServiceManager.Instance.onDataSaveFailed -= OnDataSaveFailed;
            // PlayServiceManager.Instance.onDataLoaded -= OnDataLoaded;
            // PlayServiceManager.Instance.onDataLoadFailed -= OnDataLoadFailed;
            // }
        }

        private void OnDataLoadFailed()
        {
            popupEvent.ShowPopup("Data not loaded.", "Failed");
        }

        private void OnDataSaveFailed()
        {
            popupEvent.ShowPopup("Data not saved.", "Failed");
        }

        private void OnDataLoaded()
        {
            popupEvent.ShowPopup("Data loaded successfully from cloud.", "Success");
            LocalLoad();
        }

        private void OnDataSaved()
        {
            popupEvent.ShowPopup("Data saved successfully to cloud.", "Success");
        }

        private void OnSignInFailed()
        {
            popupEvent.ShowPopup("SignInFailed.", "Failed");
        }




        private void OnSignedOut()
        {
            // signInBtn.SetActive(true);
            // signOutBtn.SetActive(false);
            // cloudSaveBtn.SetActive(false);
            // cloudLoadBtn.SetActive(false);
        }

        private void OnSignedIn()
        {
            popupEvent.ShowPopup("Signed in", onlyLog: true);
            // signInBtn.SetActive(false);
            // signOutBtn.SetActive(true);
            // cloudSaveBtn.SetActive(true);
            // cloudLoadBtn.SetActive(true);
        }

        public void RandomGen()
        {
            mRandomInt = Random.Range(1, 100);
            mRandomFloat = Random.Range(0.0f, 1.0f);
            mRandomString = RandomString(Random.Range(5, 15));

            description.text = "Int value: " + mRandomInt + "\n" +
                               "Float value: " + mRandomFloat + "\n" +
                               "String value: " + mRandomString;
        }

        private string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            char[] stringChars = new char[length];

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[Random.Range(0, chars.Length)];
            }

            return new string(stringChars);
        }

        public void LocalSave()
        {
            SaveData.SetInt("IntVal", mRandomInt);
            SaveData.SetFloat("FloatVal", mRandomFloat);
            SaveData.SetString("StringVal", mRandomString);

            description.text = "Value saved";
        }

        public void LocalLoad()
        {
            mRandomInt = SaveData.GetInt("IntVal", 0);
            mRandomFloat = SaveData.GetFloat("FloatVal", 0);
            mRandomString = SaveData.GetString("StringVal");

            description.text = "Int value: " + mRandomInt + "\n" +
                               "Float value: " + mRandomFloat + "\n" +
                               "String value: " + mRandomString + "\n" +
                               "Loaded from storage";
        }

        public void CloudSave()
        {
            // var storage = FileHandler.Load();
            //
            // fsData serializedData;
            // var serializer = new fsSerializer();
            // serializer.TrySerialize(storage, out serializedData).AssertSuccessWithoutWarnings();
            // var json = fsJsonPrinter.PrettyJson(serializedData);

            //PlayServiceManager.Instance.OpenSave(true);
        }

        public void CloudLoad()
        {
            // var storage = new GameDataClass();
            // storage.intData.Add("IntVal", mRandomInt);
            // storage.floatData.Add("FloatVal", mRandomFloat);
            // storage.stringData.Add("StringVal", mRandomString);
            //
            // FileHandler.Save(storage);

            //PlayServiceManager.Instance.OpenSave(false);
        }

    }
}
