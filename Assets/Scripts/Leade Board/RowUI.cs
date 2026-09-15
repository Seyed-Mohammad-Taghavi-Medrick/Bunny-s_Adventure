using UnityEngine;
using UnityEngine.UI;

/* Script: Fills and styles one leaderboard row, including rank colors and column layout.
   Cheat sheet: static readonly shares fixed values safely; ternary operator chooses one of two values; RectOffset sets UI padding. */

public class RowUI : MonoBehaviour
{
    // Shared colors keep every generated row visually consistent.
    private static readonly Color PrimaryTextColor = new Color(0.96f, 0.98f, 1f, 1f);
    private static readonly Color SecondaryTextColor = new Color(0.76f, 0.85f, 0.94f, 1f);
    private static readonly Color FirstPlaceColor = new Color(0.47f, 0.31f, 0.08f, 0.96f);
    private static readonly Color SecondPlaceColor = new Color(0.24f, 0.30f, 0.38f, 0.96f);
    private static readonly Color ThirdPlaceColor = new Color(0.35f, 0.20f, 0.14f, 0.96f);
    private static readonly Color OddRowColor = new Color(0.07f, 0.15f, 0.24f, 0.96f);
    private static readonly Color EvenRowColor = new Color(0.10f, 0.21f, 0.32f, 0.96f);

    public Text rank;
    public Text name;
    public Text score;

    public void SetRow(int rankNumber, ScoreSimple scoreData)
    {
        // Fill the three UI columns with the supplied leaderboard record.
        rank.text = rankNumber.ToString();
        name.text = string.IsNullOrWhiteSpace(scoreData.name) ? "Anonymous" : scoreData.name;
        score.text = scoreData.score.ToString("N0");

        rank.alignment = TextAnchor.MiddleCenter;
        name.alignment = TextAnchor.MiddleLeft;
        score.alignment = TextAnchor.MiddleRight;
        rank.color = rankNumber <= 3 ? PrimaryTextColor : SecondaryTextColor;
        name.color = PrimaryTextColor;
        score.color = PrimaryTextColor;

        // Configure the optional layout component when the prefab has one.
        var layout = GetComponent<HorizontalLayoutGroup>();
        if (layout != null)
        {
            layout.padding = new RectOffset(24, 24, 0, 0);
            layout.spacing = 16;
            layout.childControlWidth = true;
            layout.childForceExpandWidth = false;
        }

        SetColumnWidth(rank, 82, 0);
        SetColumnWidth(name, 0, 1);
        SetColumnWidth(score, 156, 0);

        var background = GetComponent<Image>();
        if (background != null)
            background.color = GetRowColor(rankNumber);
    }

    private static void SetColumnWidth(Text text, float preferredWidth, float flexibleWidth)
    {
        // Add a layout component only when the text object does not already have one.
        var layoutElement = text.GetComponent<LayoutElement>();
        if (layoutElement == null)
            layoutElement = text.gameObject.AddComponent<LayoutElement>();

        layoutElement.preferredWidth = preferredWidth;
        layoutElement.flexibleWidth = flexibleWidth;
    }

    private static Color GetRowColor(int rankNumber)
    {
        // Highlight the top three rows and alternate all remaining row colors.
        if (rankNumber == 1) return FirstPlaceColor;
        if (rankNumber == 2) return SecondPlaceColor;
        if (rankNumber == 3) return ThirdPlaceColor;
        return rankNumber % 2 == 0 ? EvenRowColor : OddRowColor;
    }
}
