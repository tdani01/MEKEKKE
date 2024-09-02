using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class WordItem
{
    public string main_word { get; }
    public List<string> anagrammas { get; }
    public int leastToFind { get; }
    public int points { get; }

    public WordItem(string main_word, List<string> anagrammas)
    {
        this.main_word = main_word;
        this.anagrammas = anagrammas;
        this.leastToFind = this.anagrammas.Count / 2;
        this.points = this.anagrammas.Count * 2;
    }
}

public class Handler : MonoBehaviour
{
    static List<WordItem> word_items = new List<WordItem>();
    public GameObject p_optionWord;
    
    static WordItem getRandomWord()
    {
        if (word_items.Count <= 0) { return null; }
        return word_items[Random.Range(0, word_items.Count)];
    }

    static WordItem? getWord(int index) 
    {
        try
        {
            return word_items[index];
        }
        catch (System.Exception ex)
        {
            return null;
        }
    }

    void showWord(Text main_text, Text points, GameObject[] finalWordPlaces, bool random, int index = 0)
    {
        WordItem word;
        if (main_text == null) { return; }
        if (points == null) { return; }
        if (finalWordPlaces == null) { return; }
        if (random) { word = getRandomWord(); }
        else { word = getWord(index); }
        main_text.text = word.main_word;
        
        void UpdatePoints(int c_points) { points.text = $"{c_points}/{word.points}"; }
        UpdatePoints(0);
        finalWordPlaces = new GameObject[word.anagrammas.Count];
        for (int i = 0; i < finalWordPlaces.Length; ++i) 
        {
            finalWordPlaces[i] = Instantiate(p_optionWord);
            finalWordPlaces[i].transform.SetParent(finalWordPlaces[i].transform, false);
            finalWordPlaces[i].AddComponent<Button>();
            finalWordPlaces[i].AddComponent<Text>();
            Text optionText = finalWordPlaces[i].GetComponent<Text>();
            optionText.text = new string('_', word.anagrammas[i].Length);
            optionText.text += $" ({word.anagrammas[i].Length})";
        }
        LimitKeyboard(new HashSet<char>(word.main_word));
    }

    private void LimitKeyboard(HashSet<char> e_keys)
    {
        if (e_keys.Count <= 0) { return; }
        //if ()
    }
}
