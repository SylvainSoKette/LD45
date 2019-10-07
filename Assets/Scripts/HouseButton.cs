using UnityEngine;
using UnityEngine.UI;


public class HouseBuilding {
    private int level = 0;

    public static int stoneCost = 6;
    public static int stickCost = 2;
    public static int brickCost = 2;

    private GameObject board;
    private BoardBehavior boardBehavior;

    public HouseBuilding()
    {
        board = GameObject.Find("Board");
        boardBehavior = board.GetComponent<BoardBehavior>();
    }

    public bool MeetRequirements()
    {
        if (boardBehavior.GetRessource(ressource.STONE) < stoneCost) return false;
        if (boardBehavior.GetRessource(ressource.STICK) < stickCost) return false;
        if (boardBehavior.GetRessource(ressource.BRICK) < brickCost) return false;
        return true;
    }

    public void Upgrade()
    {
        boardBehavior.UseRessource(ressource.STONE, stoneCost);
        boardBehavior.UseRessource(ressource.STICK, stickCost);
        boardBehavior.UseRessource(ressource.BRICK, brickCost);
        level += 1;
    }

    public int GetLevel()
    {
        return this.level;
    }
}


public class HouseButton : MonoBehaviour
{
    public GameObject tooltip;
    private Text tooltipText;

    void Start() {
        tooltip.SetActive(false);
        tooltipText = tooltip.GetComponentInChildren<Text>();
    }

    string GetDescription() {
        return "A nice house\nExplore faster\n\nCost:\n- "+HouseBuilding.stoneCost+" stone\n- "+HouseBuilding.stickCost+" stick\n- "+HouseBuilding.brickCost+" brick";
    }

    public void OnMouseOver() {
        tooltip.SetActive(true);
        tooltipText.text = GetDescription();
    }

    public void OnMouseExit() {
        tooltip.SetActive(false);
    }
}
