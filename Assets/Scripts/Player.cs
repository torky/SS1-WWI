using UnityEngine;
using UnityEngine.UI;

using System.Collections;

public class Player : MonoBehaviour
{

    float money;
    int[] costs;
    public int incomeRate;
    float incomeDelay = 2f;
    public Text moneyDisplay;

    // Use this for initialization
    void Start()
    {
        money = 100;
        StartCoroutine(Income());

        PlayerPrefs.SetFloat("money", money);

        //stats stored in following order
        //{0},   {1}     {2}     {3}        {4}        {5}    {6}
        //cost, attack, speed, defense, attackspeed, health, kills
        float[] UnitStat = {50};
        float[] Unit2Stat = { 10};
        float[] Unit3Stat = { 1 };

        PlayerPrefsX.SetFloatArray("UnitStat", UnitStat);
        PlayerPrefsX.SetFloatArray("Unit2Stat", Unit2Stat);
        PlayerPrefsX.SetFloatArray("Unit3Stat", Unit3Stat);

    }

    // Update is called once per frame
    void Update()
    {
        money = PlayerPrefs.GetFloat("money");

        Income();
        moneyDisplay.text = "money:" + money.ToString();

    }

    IEnumerator Income()
    {
        while (true)
        {
            yield return new WaitForSeconds(incomeDelay);
            money += incomeRate;
            PlayerPrefs.SetFloat("money", money);
        }
    }

    //pass negative numbers if unchanged
    void UnitTemplateChanges(float[] stat, string unitName)
    {
        float[] tempStat = PlayerPrefsX.GetFloatArray(unitName);
            
        for (int i = 0; i < stat.Length; i++)
        {
            if (stat[i] > 0)
            {
                tempStat[i] = stat[i];
            }
        }

        PlayerPrefsX.SetFloatArray(unitName, stat);

    }

}
