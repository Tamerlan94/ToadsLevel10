using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

[RequireComponent(typeof(Button))]
public class RewardedAdsButton : MonoBehaviour, IUnityAdsListener
{

#if UNITY_IOS
    private string gameId = "3575917";
#elif UNITY_ANDROID
    private string gameId = "3575916";
#endif

    Button myButton;
    public Button heart;
    public Button second;
    public string myPlacementId = "rewardedVideo";

    void Start()
    {        
        myButton = GetComponent<Button>();

        // Set interactivity to be dependent on the Placement’s status:
        myButton.interactable = Advertisement.IsReady(myPlacementId);

        // Map the ShowRewardedVideo function to the button’s click listener:
        if (myButton) myButton.onClick.AddListener(ShowRewardedVideo);

        // Initialize the Ads listener and service:
        Advertisement.AddListener(this);
        Advertisement.Initialize(gameId, true);
    }

    // Implement a function for showing a rewarded video ad:
    void ShowRewardedVideo()
    {
        Advertisement.Show(myPlacementId);
    }

    // Implement IUnityAdsListener interface methods:
    public void OnUnityAdsReady(string placementId)
    {
        // If the ready Placement is rewarded, activate the button: 
        if (placementId == myPlacementId)
        {
            myButton.interactable = true;
        }        
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        int randomCoin = Random.Range(10, 60);


        // Define conditional logic for each ad completion status:
        if (showResult == ShowResult.Finished)
        {
            // Reward the user for watching the ad to completion.
            if(placementId == "rewardedVideo")
            {
                SoundManager.current.PlayMusic();
                second.gameObject.SetActive(false);
                GameManager.current.SecondLife();                
            }
            else if(placementId == "video")
            {
                heart.gameObject.SetActive(false);
                ScoreManager.current.AddScore(randomCoin);
            }
            
        }
        else if (showResult == ShowResult.Skipped)
        {
            // Do not reward the user for skipping the ad.
            if (placementId == "video")
            {
                heart.gameObject.SetActive(false);
                ScoreManager.current.AddScore(randomCoin);
            }
            else if(placementId == "rewardedVideo")
            {
                FindObjectOfType<ButtonController>().LoseGame();
            }
        }
        else if (showResult == ShowResult.Failed)
        {
            Debug.LogWarning("The ad did not finish due to an error.");
            if (placementId == "rewardedVideo")
            {
                FindObjectOfType<ButtonController>().LoseGame();
            }
        }
    }

    public void OnUnityAdsDidError(string message)
    {
        // Log the error.
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        // Optional actions to take when the end-users triggers an ad.
    }
}