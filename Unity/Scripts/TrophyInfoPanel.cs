using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TrophyInfoPanel : MonoBehaviour
{
    public TextMeshProUGUI trophyNameText;
    public TextMeshProUGUI trophyDescriptionText;
    public Image trophyIconImage;

    public void ShowInfo(TrophyData data)
    {
        if (data.unlocked)
        {
            trophyNameText.text = data.trophyName;
            trophyDescriptionText.text = data.trophyDescription;
            trophyIconImage.sprite = data.unlockedIcon;
        }
        else
        {
            trophyNameText.text = data.secretTrophy ? "???" : data.trophyName;
            trophyDescriptionText.text = data.secretTrophy ? "Unlock this trophy to learn more." : data.unlockHint;
            trophyIconImage.sprite = data.lockedIcon;
        }
    }

    public void ClearInfo()
    {
        trophyNameText.text = "";
        trophyDescriptionText.text = "";
        trophyIconImage.sprite = null;
    }
}
