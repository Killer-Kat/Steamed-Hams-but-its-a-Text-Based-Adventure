using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TrophyTooltip : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;
    public Image iconImage;

    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        gameObject.SetActive(false);
    }

    public void Show(TrophyData data, Vector3 iconWorldPos)
    {
        gameObject.SetActive(true);

        // Position tooltip slightly to the right of the icon
        rectTransform.position = iconWorldPos + new Vector3(120f, 0f, 0f);

        if (data.unlocked)
        {
            nameText.text = data.trophyName;
            descriptionText.text = data.trophyDescription;
            iconImage.sprite = data.unlockedIcon;
        }
        else
        {
            nameText.text = data.secretTrophy ? "???" : data.trophyName;
            descriptionText.text = data.secretTrophy
                ? "Unlock this trophy to learn more."
                : data.unlockHint;

            iconImage.sprite = data.lockedIcon;
        }
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
