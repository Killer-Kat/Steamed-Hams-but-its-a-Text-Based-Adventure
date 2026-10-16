using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TrophyButtonUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TrophyData trophyData;
    public Image iconImage;

    private Vector3 originalScale;
    private TrophyTooltip tooltip;

    private void Awake()
    {
        originalScale = transform.localScale;
    }

    private void Start()
    {
        tooltip = FindObjectOfType<TrophyTooltip>();

        // Set correct icon
        if (trophyData.unlocked)
            iconImage.sprite = trophyData.unlockedIcon;
        else
            iconImage.sprite = trophyData.lockedIcon;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = originalScale * 1.2f; // pop effect
        tooltip.Show(trophyData, transform.position);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = originalScale;
        tooltip.Hide();
    }
}
