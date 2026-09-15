using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    public RowUI rowUI;
    public ScoreManager scoreManager;

    private void Start()
    {
        if (rowUI == null || scoreManager == null)
        {
            Debug.LogError("Leaderboard UI is missing its row prefab or score manager reference.", this);
            return;
        }

        var scores = scoreManager.GetHighScores();
        if (scores == null || scores.Count == 0)
        {
            CreateEmptyState();
            return;
        }

        for (int i = 0; i < scores.Count; i++)
        {
            var row = Instantiate(rowUI, transform);
            row.SetRow(i + 1, scores[i]);
        }
    }

    private void CreateEmptyState()
    {
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
