using UnityEngine;
using UnityEngine.UI;


public class FirePitBuilding {
    private int level = 0;

    public static int stoneCost = 4;
    public static int stickCost = 2;

    private GameObject board;
    private BoardBehavior boardBehavior;

    public FirePitBuilding()
    {
        board = GameObject.Find("Board");
        boardBehavior = board.GetComponent<BoardBehavior>();
    }

    public bool MeetRequirements()
    {
        if (boardBehavior.GetRessource(ressource.STONE) < stoneCost) return false;
        if (boardBehavior.GetRessource(ressource.STICK) < stickCost) return false;
        return true;
    }

    public void Upgrade()
    {
        boardBehavior.UseRessource(ressource.STONE, stoneCost);
        boardBehavior.UseRessource(ressource.STICK, stickCost);
        level += 1;
    }

    public int GetLevel()
    {
        return this.level;
    }
}


public class FirePitButton : MonoBehaviour
{
    public GameObject tooltip;
    private Text tooltipText;

    void Start() {
        tooltip.SetActive(false);
        tooltipText = tooltip.GetComponentInChildren<Text>();
    }

    string GetDescription() {
        return "A fire pit\nMakes bricks from\nclay every day\n\nCost:\n- "+FirePitBuilding.stoneCost+" stone\n- "+FirePitBuilding.stickCost+" stick";
    }

    public void OnMouseOver() {
        tooltip.SetActive(true);
        tooltipText.text = GetDescription();
    }

    public void OnMouseExit() {
        tooltip.SetActive(false);
    }
}
