using UnityEngine;
using UnityEngine.UI;


public class MonumentBuilding {
    private int level = 0;

    public static int stoneCost = 50;
    public static int foodCost = 20;

    private GameObject board;
    private BoardBehavior boardBehavior;

    public MonumentBuilding()
    {
        board = GameObject.Find("Board");
        boardBehavior = board.GetComponent<BoardBehavior>();
    }

    public bool MeetRequirements()
    {
        if (boardBehavior.GetRessource(ressource.STONE) < stoneCost) return false;
        if (boardBehavior.GetRessource(ressource.FOOD) < foodCost) return false;
        return true;
    }

    public void Upgrade()
    {
        boardBehavior.UseRessource(ressource.STONE, stoneCost);
        boardBehavior.UseRessource(ressource.FOOD, foodCost);
        level += 1;
    }

    public int GetLevel()
    {
        return this.level;
    }
}


public class MonumentButton : MonoBehaviour
{
    public GameObject tooltip;
    private Text tooltipText;

    void Start() {
        tooltip.SetActive(false);
        tooltipText = tooltip.GetComponentInChildren<Text>();
    }

    string GetDescription() {
        return "The Monument.\nYou win the game.\n\nCost:\n- "+MonumentBuilding.stoneCost+" stone\n- "+MonumentBuilding.foodCost+" food";
    }

    public void OnMouseOver() {
        tooltip.SetActive(true);
        tooltipText.text = GetDescription();
    }

    public void OnMouseExit() {
        tooltip.SetActive(false);
    }
}
