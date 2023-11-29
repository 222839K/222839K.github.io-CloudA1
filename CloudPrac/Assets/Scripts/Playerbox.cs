using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class Player
{
    public int level;
    public int xp;
    public string dt;
    public Player(int _level, int _xp, string _dt)
    {
        level = _level; 
        xp = _xp;
        dt = _dt;
    }
}

public class Playerbox : MonoBehaviour
{
    [SerializeField] TMP_Text LevelText;
    [SerializeField] TMP_Text XPText;
    [SerializeField] TMP_Text DtText;


    public Player ReturnClass()
    {
        int.TryParse(LevelText.text, out int level);
        int.TryParse(XPText.text, out int xp);
        return new Player((int)level, (int)xp, DtText.text);
    }
    public void SetUI(Player pl)
    {
        //PlayerName.text = pl.name;
        ////int.TryParse(ScoreText.text);
        ////int.TryParse(pl.score, out int score);
        //ScoreText.text = pl.score.ToString();
        //int.TryParse(pl.level, out int lvl);
        LevelText.text = pl.level.ToString();
        //int.TryParse(pl.xp, out int xp);
        XPText.text = pl.xp.ToString();
        DtText.text = pl.dt;
    }
    //public void SliderChangeUpdate(float num)
    //{
    //    SkillLevelText.text = SkillLevelSlider.value.ToString();
    //}
}
