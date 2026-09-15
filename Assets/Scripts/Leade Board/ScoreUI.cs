using UnityEngine;
using UnityEngine.UI;

/* Script: Builds leaderboard rows from saved scores, or shows an empty-state row.
   Cheat sheet: Instantiate with a parent adds UI under that transform; List.Count gives item count; LayoutElement controls UI layout size. */

public class ScoreUI : MonoBehaviour
{
    // Row prefab and manager are assigned in the Inspector.
    public RowUI rowUI;
    public ScoreManager scoreManager;

    private void Start()
    {
        // Validate scene references before creating any UI rows.
        if (rowUI == null || scoreManager == null)
        {
            Debug.LogError("Leaderboard UI is missing its row prefab or score manager reference.", this);
            return;
        }

        // Read saved scores once, then create one row for each score.
        var scores = scoreManager.GetHighScores();
        if (scores == null || scores.Count == 0)
        {
            CreateEmptyState();
            return;
        }

        for (int i = 0; i < scores.Count; i++)
        {
            // Rank starts at one while list indexes start at zero.
            var row = Instantiate(rowUI, transform);
            row.SetRow(i + 1, scores[i]);
        }
    }

    private void CreateEmptyState()
    {
        // Reuse the normal row prefab and hide columns not needed for the message.
        var row = Instantiate(rowUI, transform);
        row.rank.gameObject.SetActive(false);
        row.score.gameObject.SetActive(false);
        row.name.text = "No scores yet. Finish a run to claim the top spot.";
        row.name.alignment = TextAnchor.MiddleCenter;
        row.name.color = Color.white;

        var nameLayout = row.name.GetComponent<LayoutElement>();
        if (nameLayout == null)
            nameLayout = row.name.gameObject.AddComponent<LayoutElement>();
        nameLayout.flexibleWidth = 1;

        var background = row.GetComponent<Image>();
        if (background != null)
            background.color = new Color(0.07f, 0.15f, 0.24f, 0.96f);
    }
}
