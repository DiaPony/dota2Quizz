using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.Rendering.RendererUtils;
using UnityEngine.UI;

public class QuestionManager : MonoBehaviour
{

    //Datei Manager für Unity
    public Item[] items;
    public Image ItemQuestionPicture;
    public List<Item> UpgradableItems;
    public List<Item> ItemsWithStats;
    public List<Item> ItemsWithSpecialAbility;
    public Text ItemQuestionText;
    public Text TextQuestionText;
    public Text AnswerText1;
    public Text AnswerText2;
    public Text AnswerText3;
    public Text AnswerText4;

    public AudioSource WindSound;
    public AudioSource LoseSound;


    public GameMechanic[] gameMechanics;
    public List<GameMechanic> allGameMechanics;

    private GameMechanic gameMechanic1;
    private GameMechanic gameMechanic2;
    private GameMechanic gameMechanic3;
    private GameMechanic gameMechanic4;

    private Item item1;
    private Item item2;
    private Item item3;
    private Item item4;

    public Text ScoreText;
    public int score;
    public int Highscore;
    public Text HighscoreText;
    public Text LastAnswerText;
    public List<Item> allItems;
    public int RightButton;


    [Header("Antwort 1 Bilder")]
    public Image Answer1Image1;
    public Image Answer1Image2;
    public Image Answer1Image3;
    public Image Answer1Image4;
    public Image Answer1Image5;
    public Image Answer1Image6;

    [Header("Antwort 2 Bilder")]
    public Image Answer2Image1;
    public Image Answer2Image2;
    public Image Answer2Image3;
    public Image Answer2Image4;
    public Image Answer2Image5;
    public Image Answer2Image6;

    [Header("Antwort 3 Bilder")]
    public Image Answer3Image1;
    public Image Answer3Image2;
    public Image Answer3Image3; 
    public Image Answer3Image4;
    public Image Answer3Image5;
    public Image Answer3Image6;

    [Header("Antwort 4 Bilder")]
    public Image Answer4Image1;
    public Image Answer4Image2;
    public Image Answer4Image3;
    public Image Answer4Image4;
    public Image Answer4Image5;
    public Image Answer4Image6;

    private void Start()
    {
        //Highscore wird geladen falls vorhanden
        HighScore test = SaveSystem.LoadHighscore();
        if (test == null)
        {
            Highscore = 0;
        }
        else 
        {
            Highscore = test.highScore;
        }
        HighscoreText.text = "Highscore: " + Highscore;

        //Items und Gamemechaniken werden geladen
        addAllItemsToList();
        addAllGamemechanicsToList();

        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].isUpgraded)
            {
                UpgradableItems.Add(items[i]);
            }
        }

        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].HasStats)
            {
                ItemsWithStats.Add(items[i]);
            }
        }

        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].HasSpecialAbility)
            {
                ItemsWithSpecialAbility.Add(items[i]);
            }
        }
       

        //erste Frage wird generiert
        CreateItemNameQuestion();

    }

    private void addAllItemsToList() 
    {
        for (int i = 0; i < items.Length; i++)
        {
            allItems.Add(items[i]);
        }
    }

    private void addAllGamemechanicsToList()
    {
        for (int i = 0; i < gameMechanics.Length; i++)
        {
            allGameMechanics.Add(gameMechanics[i]);
        }
    }
   
    //Score UI wird geupdated
    private void Update()
    {
        ScoreText.text = "Current Score: " + score.ToString();
    }

    //Neue Frage wird generiert
    public void CreateNewQuestion()
    {    
        //eine der verschiedenen Fragearten wir dzufällig ausgewählt
        int randomQuestion = Random.Range(0, 5);
        if (randomQuestion == 0 )
        {
            CreateItemNameQuestion();
        }
        else if (randomQuestion == 1 )
        {
            CreateItemCostQuestion();
        }
        else if (randomQuestion == 2)
        {
            CreateUpgradedQuestion();
        }
        else if (randomQuestion == 3)
        {
            CreateItemStatQuestion();
        }
        else if (randomQuestion == 4)
        {
            CreateGameMechanicQuestion();
        }

    }


    private Item createFromAllItems()
    {
        int randomNumber = Random.Range(0, allItems.Count);
        Item item = allItems[randomNumber];
        allItems.RemoveAt(randomNumber);
        return item;
    }
    private void ReturnToAllItems()
    {
        allItems.Add(item1);
        allItems.Add(item2);
        allItems.Add(item3);
        allItems.Add(item4);
    }

    private GameMechanic createFromAllGameMechanics()
    {
        int randomNumber = Random.Range(0, allGameMechanics.Count);
        GameMechanic gamemechanic = allGameMechanics[randomNumber];
        allGameMechanics.RemoveAt(randomNumber);
        return gamemechanic;
    }

    private void returnToAllGameMechanics()
    {
        allGameMechanics.Add(gameMechanic1);
        allGameMechanics.Add(gameMechanic2);
        allGameMechanics.Add(gameMechanic3);
        allGameMechanics.Add(gameMechanic4);
    }

    private Item createFromStatItems()
    {
        int randomNumber = Random.Range(0, ItemsWithStats.Count);
        Item item = ItemsWithStats[randomNumber];
        ItemsWithStats.RemoveAt(randomNumber);
        return item;
    }

    private void ReturnToStatItems()
    {
        ItemsWithStats.Add(item1);
        ItemsWithStats.Add(item2);
        ItemsWithStats.Add(item3);
        ItemsWithStats.Add(item4);

    }

    private Item createFromUpgradableItems()
    {
        int randomNumber = Random.Range(0, UpgradableItems.Count);
        Item item = UpgradableItems[randomNumber];
        UpgradableItems.RemoveAt(randomNumber);
        return item;
    }

    private void ReturnToUpgradableItems()
    {
        UpgradableItems.Add(item1);
        UpgradableItems.Add(item2);
        UpgradableItems.Add(item3);
        UpgradableItems.Add(item4);
    }

    private void SwitchToTextQuestion()
    {
        if (TextQuestionText.GetComponent<CanvasGroup>().alpha > 0)
        {
            return;
        }
        else
        {
            ItemQuestionPicture.GetComponent<CanvasGroup>().alpha = 0;
            TextQuestionText.GetComponent <CanvasGroup>().alpha = 1;
        }
    }

    private void SwitchToPictureQuestion()
    {
        if (TextQuestionText.GetComponent<CanvasGroup>().alpha == 0)
        {
            return;
        }
        else
        {
            ItemQuestionPicture.GetComponent<CanvasGroup>().alpha = 1;
            TextQuestionText.GetComponent<CanvasGroup>().alpha = 0;
        }
    }


    //Bilder UI wird angeschalten
    private void TurnPictureAnswerOn()
    {
        Answer1Image1.GetComponent<CanvasGroup>().alpha = 1;
        Answer1Image2.GetComponent<CanvasGroup>().alpha = 1;
        Answer1Image3.GetComponent<CanvasGroup>().alpha = 1;
        Answer1Image4.GetComponent<CanvasGroup>().alpha = 1;
        Answer1Image5.GetComponent<CanvasGroup>().alpha = 1;
        Answer1Image6.GetComponent<CanvasGroup>().alpha = 1;

        Answer2Image1.GetComponent<CanvasGroup>().alpha = 1;
        Answer2Image2.GetComponent<CanvasGroup>().alpha = 1;
        Answer2Image3.GetComponent<CanvasGroup>().alpha = 1;
        Answer2Image4.GetComponent<CanvasGroup>().alpha = 1;
        Answer2Image5.GetComponent<CanvasGroup>().alpha = 1;
        Answer2Image6.GetComponent<CanvasGroup>().alpha = 1;

        Answer3Image1.GetComponent<CanvasGroup>().alpha = 1;
        Answer3Image2.GetComponent<CanvasGroup>().alpha = 1;
        Answer3Image3.GetComponent<CanvasGroup>().alpha = 1;
        Answer3Image4.GetComponent<CanvasGroup>().alpha = 1;
        Answer3Image5.GetComponent<CanvasGroup>().alpha = 1;
        Answer3Image6.GetComponent<CanvasGroup>().alpha = 1;

        Answer4Image1.GetComponent<CanvasGroup>().alpha = 1;
        Answer4Image2.GetComponent<CanvasGroup>().alpha = 1;
        Answer4Image3.GetComponent<CanvasGroup>().alpha = 1;
        Answer4Image4.GetComponent<CanvasGroup>().alpha = 1;
        Answer4Image5.GetComponent<CanvasGroup>().alpha = 1;
        Answer4Image6.GetComponent<CanvasGroup>().alpha = 1;

        AnswerText1.text = "";
        AnswerText2.text = "";
        AnswerText3.text = "";
        AnswerText4.text = "";
    }


    //Bilder UI wird ausgeschalten
    private void TurnPictureAnswersOff()
    {
        Answer1Image1.GetComponent<CanvasGroup>().alpha = 0;
        Answer1Image2.GetComponent<CanvasGroup>().alpha = 0;
        Answer1Image3.GetComponent<CanvasGroup>().alpha = 0;
        Answer1Image4.GetComponent<CanvasGroup>().alpha = 0;
        Answer1Image5.GetComponent<CanvasGroup>().alpha = 0;
        Answer1Image6.GetComponent<CanvasGroup>().alpha = 0;

        Answer2Image1.GetComponent<CanvasGroup>().alpha = 0;
        Answer2Image2.GetComponent<CanvasGroup>().alpha = 0;
        Answer2Image3.GetComponent<CanvasGroup>().alpha = 0;
        Answer2Image4.GetComponent<CanvasGroup>().alpha = 0;
        Answer2Image5.GetComponent<CanvasGroup>().alpha = 0;
        Answer2Image6.GetComponent<CanvasGroup>().alpha = 0;

        Answer3Image1.GetComponent<CanvasGroup>().alpha = 0;
        Answer3Image2.GetComponent<CanvasGroup>().alpha = 0;
        Answer3Image3.GetComponent<CanvasGroup>().alpha = 0;
        Answer3Image4.GetComponent<CanvasGroup>().alpha = 0;
        Answer3Image5.GetComponent<CanvasGroup>().alpha = 0;
        Answer3Image6.GetComponent<CanvasGroup>().alpha = 0;

        Answer4Image1.GetComponent<CanvasGroup>().alpha = 0;
        Answer4Image2.GetComponent<CanvasGroup>().alpha = 0;
        Answer4Image3.GetComponent<CanvasGroup>().alpha = 0;
        Answer4Image4.GetComponent<CanvasGroup>().alpha = 0;
        Answer4Image5.GetComponent<CanvasGroup>().alpha = 0;
        Answer4Image6.GetComponent<CanvasGroup>().alpha = 0;
    }

    //Ausgabe der Frage als Richtig
    public void GetAnswerRight()
    {
        LastAnswerText.text = "Letzte Antwort war: RICHTIG";
        score += 30;
        CreateNewQuestion();
        LastAnswerText.color = Color.green;
        WindSound.Play();

    }
    //Ausgabe der Frage als Falsch
    public void GetAnswerWrong()
    {
        LastAnswerText.text = "Letzte Antwort war: Falsch";        
        CreateNewQuestion();
        if (score > Highscore)
        {
            SaveSystem.SaveHighscore(score);
            Highscore = score;
            HighscoreText.text = "Highscore: " + Highscore;
        }     
        score = 0;
        LastAnswerText.color = Color.red;
        LoseSound.Play();
    }


    //Knopf Logik
    public void PressButton1()
    {
        if (RightButton == 1)
        {
            GetAnswerRight();
        }
        else
        {
            GetAnswerWrong();
        }
    }

    public void PressButton2()
    {
        if (RightButton == 2)
        {
            GetAnswerRight();
        }
        else
        {
            GetAnswerWrong();
        }

    }

    public void PressButton3()
    {
        if (RightButton == 3)
        {
            GetAnswerRight();
        }
        else
        {
            GetAnswerWrong();
        }

    }

    public void PressButton4()
    {
        if (RightButton == 4)
        {
            GetAnswerRight();
        }
        else
        {
            GetAnswerWrong();
        }

    }



    public void CreateItemNameQuestion()
    {
        item1 = createFromAllItems();
        item2 = createFromAllItems();
        item3 = createFromAllItems();
        item4 = createFromAllItems();

        SwitchToPictureQuestion();
        TurnPictureAnswersOff();
        ItemQuestionPicture.sprite = item1.Icon;

        ItemQuestionText.text = "Wie heißt dieses Item?";

        int randomLayoutNumber = Random.Range(0, 4);
        if (randomLayoutNumber == 0)
        {
            AnswerText1.text = item1.Name;
            AnswerText2.text = item2.Name;
            AnswerText3.text = item3.Name;
            AnswerText4.text = item4.Name;

            RightButton = 1;
        }
        else if (randomLayoutNumber == 1)
        {
            AnswerText1.text = item2.Name;
            AnswerText2.text = item1.Name;
            AnswerText3.text = item3.Name;
            AnswerText4.text = item4.Name;
            RightButton = 2;
        }
        else if (randomLayoutNumber == 2)
        {
            AnswerText1.text = item2.Name;
            AnswerText2.text = item3.Name;
            AnswerText3.text = item1.Name;
            AnswerText4.text = item4.Name;
            RightButton = 3;
        }
        else
        {
            AnswerText1.text = item2.Name;
            AnswerText2.text = item3.Name;
            AnswerText3.text = item4.Name;
            AnswerText4.text = item1.Name;
            RightButton = 4;
        }

        ReturnToAllItems();

    }

    private void resetAnswerPictures()
    {
        Answer1Image1.sprite = null;
        Answer1Image2.sprite = null;
        Answer1Image3.sprite = null;
        Answer1Image4.sprite = null;
        Answer1Image5.sprite = null;
        Answer1Image6.sprite = null;

        Answer2Image1.sprite = null;
        Answer2Image2.sprite = null;
        Answer2Image3.sprite = null;
        Answer2Image4.sprite = null;
        Answer2Image5.sprite = null;
        Answer2Image6.sprite = null;

        Answer3Image1.sprite = null;
        Answer3Image2.sprite = null;
        Answer3Image3.sprite = null;
        Answer3Image4.sprite = null;
        Answer3Image5.sprite = null;
        Answer3Image6.sprite = null;

        Answer4Image1.sprite = null;
        Answer4Image2.sprite = null;
        Answer4Image3.sprite = null;
        Answer4Image4.sprite = null;
        Answer4Image5.sprite = null;
        Answer4Image6.sprite = null;
    }

    public void CreateItemCostQuestion()
    {

        item1 = createFromAllItems();
        item2 = createFromAllItems();
        item3 = createFromAllItems();
        item4 = createFromAllItems();

        SwitchToPictureQuestion();
        TurnPictureAnswersOff();
        ItemQuestionPicture.sprite = item1.Icon;

        ItemQuestionText.text = "Wieviel kostet " + item1.Name + "?";

        int randomLayoutNumber = Random.Range(0, 4);
        if (randomLayoutNumber == 0)
        {
            AnswerText1.text = item1.Cost.ToString();
            AnswerText2.text = item2.Cost.ToString();
            AnswerText3.text = item3.Cost.ToString();
            AnswerText4.text = item4.Cost.ToString();

            RightButton = 1;
        }
        else if (randomLayoutNumber == 1)
        {
            AnswerText1.text = item2.Cost.ToString();
            AnswerText2.text = item1.Cost.ToString();
            AnswerText3.text = item3.Cost.ToString();
            AnswerText4.text = item4.Cost.ToString();
            RightButton = 2;
        }
        else if (randomLayoutNumber == 2)
        {
            AnswerText1.text = item2.Cost.ToString();
            AnswerText2.text = item3.Cost.ToString();
            AnswerText3.text = item1.Cost.ToString();
            AnswerText4.text = item4.Cost.ToString();
            RightButton = 3;
        }
        else
        {
            AnswerText1.text = item2.Cost.ToString();
            AnswerText2.text = item3.Cost.ToString();
            AnswerText3.text = item4.Cost.ToString();
            AnswerText4.text = item1.Cost.ToString();
            RightButton = 4;
        }

        ReturnToAllItems();
    }

    public void CreateGameMechanicQuestion()
    {
        gameMechanic1 = createFromAllGameMechanics();
        gameMechanic2 = createFromAllGameMechanics();
        gameMechanic3 = createFromAllGameMechanics();
        gameMechanic4 = createFromAllGameMechanics();

        SwitchToTextQuestion();
        TurnPictureAnswersOff();
        TextQuestionText.text = gameMechanic1.Description;
        ItemQuestionText.text = "Wie heißt diese Gamemechanic?";
        

        int randomLayoutNumber = Random.Range(0, 4);
        if (randomLayoutNumber == 0)
        {
            AnswerText1.text = gameMechanic1.Name;
            AnswerText2.text = gameMechanic2.Name;
            AnswerText3.text = gameMechanic3.Name;
            AnswerText4.text = gameMechanic4.Name;

            RightButton = 1;
        }
        else if (randomLayoutNumber == 1)
        {
            AnswerText1.text = gameMechanic2.Name;
            AnswerText2.text = gameMechanic1.Name;
            AnswerText3.text = gameMechanic3.Name;
            AnswerText4.text = gameMechanic4.Name;
            RightButton = 2;
        }
        else if (randomLayoutNumber == 2)
        {
            AnswerText1.text = gameMechanic2.Name;
            AnswerText2.text = gameMechanic3.Name;
            AnswerText3.text = gameMechanic1.Name;
            AnswerText4.text = gameMechanic4.Name;
            RightButton = 3;
        }
        else
        {
            AnswerText1.text = gameMechanic2.Name;
            AnswerText2.text = gameMechanic3.Name;
            AnswerText3.text = gameMechanic4.Name;
            AnswerText4.text = gameMechanic1.Name;
            RightButton = 4;
        }
        returnToAllGameMechanics();

    }

    public void CreateItemStatQuestion()
    {
        item1 = createFromStatItems();
        item2 = createFromStatItems();
        item3 = createFromStatItems();
        item4 = createFromStatItems();

        SwitchToPictureQuestion();
        TurnPictureAnswersOff();
        ItemQuestionPicture.sprite = item1.Icon;

        ItemQuestionText.text = "Welche Stats hat " + item1.Name.ToString() + "?";

        int randomLayoutNumber = Random.Range(0, 4);
        if (randomLayoutNumber == 0)
        {
            AnswerText1.text = item1.Stats;
            AnswerText2.text = item2.Stats;
            AnswerText3.text = item3.Stats;
            AnswerText4.text = item4.Stats;

            RightButton = 1;
        }
        else if (randomLayoutNumber == 1)
        {
            AnswerText1.text = item2.Stats;
            AnswerText2.text = item1.Stats;
            AnswerText3.text = item3.Stats;
            AnswerText4.text = item4.Stats;
            RightButton = 2;
        }
        else if (randomLayoutNumber == 2)
        {
            AnswerText1.text = item2.Stats;
            AnswerText2.text = item3.Stats;
            AnswerText3.text = item1.Stats;
            AnswerText4.text = item4.Stats;
            RightButton = 3;
        }
        else
        {
            AnswerText1.text = item2.Stats;
            AnswerText2.text = item3.Stats;
            AnswerText3.text = item4.Stats;
            AnswerText4.text = item1.Stats;
            RightButton = 4;
        }

        ReturnToStatItems();

    }

    public void CreateUpgradedQuestion()
    {
        resetAnswerPictures();
        item1 = createFromUpgradableItems();
        item2 = createFromUpgradableItems();
        item3 = createFromUpgradableItems();
        item4 = createFromUpgradableItems();

        SwitchToPictureQuestion();
        TurnPictureAnswerOn();
        ItemQuestionText.text = "Welche Komponenten werden für " + item1.Name + " benötigt?";

        ItemQuestionPicture.sprite = item1.Icon;
        int randomLayoutNumber = Random.Range(0, 4);

        if (randomLayoutNumber == 0)
        {
            Answer1Image1.sprite = item1.ItemsNeededForUpgrade[0];
            Answer1Image2.sprite = item1.ItemsNeededForUpgrade[1];
            Answer1Image3.sprite = item1.ItemsNeededForUpgrade[2];
            Answer1Image4.sprite = item1.ItemsNeededForUpgrade[3];
            Answer1Image5.sprite = item1.ItemsNeededForUpgrade[4];
            Answer1Image6.sprite = item1.ItemsNeededForUpgrade[5];

            Answer2Image1.sprite = item2.ItemsNeededForUpgrade[0];
            Answer2Image2.sprite = item2.ItemsNeededForUpgrade[1];
            Answer2Image3.sprite = item2.ItemsNeededForUpgrade[2];
            Answer2Image4.sprite = item2.ItemsNeededForUpgrade[3];
            Answer2Image5.sprite = item2.ItemsNeededForUpgrade[4];
            Answer2Image6.sprite = item2.ItemsNeededForUpgrade[5];

            Answer3Image1.sprite = item3.ItemsNeededForUpgrade[0];
            Answer3Image2.sprite = item3.ItemsNeededForUpgrade[1];
            Answer3Image3.sprite = item3.ItemsNeededForUpgrade[2];
            Answer3Image4.sprite = item3.ItemsNeededForUpgrade[3];
            Answer3Image5.sprite = item3.ItemsNeededForUpgrade[4];
            Answer3Image6.sprite = item3.ItemsNeededForUpgrade[5];

            Answer4Image1.sprite = item4.ItemsNeededForUpgrade[0];
            Answer4Image2.sprite = item4.ItemsNeededForUpgrade[1];
            Answer4Image3.sprite = item4.ItemsNeededForUpgrade[2];
            Answer4Image4.sprite = item4.ItemsNeededForUpgrade[3];
            Answer4Image5.sprite = item4.ItemsNeededForUpgrade[4];
            Answer4Image6.sprite = item4.ItemsNeededForUpgrade[5];

            RightButton = 1;
        }
        else if (randomLayoutNumber == 1)
        {
            Answer1Image1.sprite = item2.ItemsNeededForUpgrade[0];
            Answer1Image2.sprite = item2.ItemsNeededForUpgrade[1];
            Answer1Image3.sprite = item2.ItemsNeededForUpgrade[2];
            Answer1Image4.sprite = item2.ItemsNeededForUpgrade[3];
            Answer1Image5.sprite = item2.ItemsNeededForUpgrade[4];
            Answer1Image6.sprite = item2.ItemsNeededForUpgrade[5];

            Answer2Image1.sprite = item1.ItemsNeededForUpgrade[0];
            Answer2Image2.sprite = item1.ItemsNeededForUpgrade[1];
            Answer2Image3.sprite = item1.ItemsNeededForUpgrade[2];
            Answer2Image4.sprite = item1.ItemsNeededForUpgrade[3];
            Answer2Image5.sprite = item1.ItemsNeededForUpgrade[4];
            Answer2Image6.sprite = item1.ItemsNeededForUpgrade[5];

            Answer3Image1.sprite = item3.ItemsNeededForUpgrade[0];
            Answer3Image2.sprite = item3.ItemsNeededForUpgrade[1];
            Answer3Image3.sprite = item3.ItemsNeededForUpgrade[2];
            Answer3Image4.sprite = item3.ItemsNeededForUpgrade[3];
            Answer3Image5.sprite = item3.ItemsNeededForUpgrade[4];
            Answer3Image6.sprite = item3.ItemsNeededForUpgrade[5];

            Answer4Image1.sprite = item4.ItemsNeededForUpgrade[0];
            Answer4Image2.sprite = item4.ItemsNeededForUpgrade[1];
            Answer4Image3.sprite = item4.ItemsNeededForUpgrade[2];
            Answer4Image4.sprite = item4.ItemsNeededForUpgrade[3];
            Answer4Image5.sprite = item4.ItemsNeededForUpgrade[4];
            Answer4Image6.sprite = item4.ItemsNeededForUpgrade[5];

            RightButton = 2;
        }
        else if (randomLayoutNumber == 2)
        {
            Answer1Image1.sprite = item2.ItemsNeededForUpgrade[0];
            Answer1Image2.sprite = item2.ItemsNeededForUpgrade[1];
            Answer1Image3.sprite = item2.ItemsNeededForUpgrade[2];
            Answer1Image4.sprite = item2.ItemsNeededForUpgrade[3];
            Answer1Image5.sprite = item2.ItemsNeededForUpgrade[4];
            Answer1Image6.sprite = item2.ItemsNeededForUpgrade[5];

            Answer2Image1.sprite = item3.ItemsNeededForUpgrade[0];
            Answer2Image2.sprite = item3.ItemsNeededForUpgrade[1];
            Answer2Image3.sprite = item3.ItemsNeededForUpgrade[2];
            Answer2Image4.sprite = item3.ItemsNeededForUpgrade[3];
            Answer2Image5.sprite = item3.ItemsNeededForUpgrade[4];
            Answer2Image6.sprite = item3.ItemsNeededForUpgrade[5];

            Answer3Image1.sprite = item1.ItemsNeededForUpgrade[0];
            Answer3Image2.sprite = item1.ItemsNeededForUpgrade[1];
            Answer3Image3.sprite = item1.ItemsNeededForUpgrade[2];
            Answer3Image4.sprite = item1.ItemsNeededForUpgrade[3];
            Answer3Image5.sprite = item1.ItemsNeededForUpgrade[4];
            Answer3Image6.sprite = item1.ItemsNeededForUpgrade[5];

            Answer4Image1.sprite = item4.ItemsNeededForUpgrade[0];
            Answer4Image2.sprite = item4.ItemsNeededForUpgrade[1];
            Answer4Image3.sprite = item4.ItemsNeededForUpgrade[2];
            Answer4Image4.sprite = item4.ItemsNeededForUpgrade[3];
            Answer4Image5.sprite = item4.ItemsNeededForUpgrade[4];
            Answer4Image6.sprite = item4.ItemsNeededForUpgrade[5];

            RightButton = 3;
        }
        else
        {
            Answer1Image1.sprite = item2.ItemsNeededForUpgrade[0];
            Answer1Image2.sprite = item2.ItemsNeededForUpgrade[1];
            Answer1Image3.sprite = item2.ItemsNeededForUpgrade[2];
            Answer1Image4.sprite = item2.ItemsNeededForUpgrade[3];
            Answer1Image5.sprite = item2.ItemsNeededForUpgrade[4];
            Answer1Image6.sprite = item2.ItemsNeededForUpgrade[5];

            Answer2Image1.sprite = item3.ItemsNeededForUpgrade[0];
            Answer2Image2.sprite = item3.ItemsNeededForUpgrade[1];
            Answer2Image3.sprite = item3.ItemsNeededForUpgrade[2];
            Answer2Image4.sprite = item3.ItemsNeededForUpgrade[3];
            Answer2Image5.sprite = item3.ItemsNeededForUpgrade[4];
            Answer2Image6.sprite = item3.ItemsNeededForUpgrade[5];

            Answer3Image1.sprite = item4.ItemsNeededForUpgrade[0];
            Answer3Image2.sprite = item4.ItemsNeededForUpgrade[1];
            Answer3Image3.sprite = item4.ItemsNeededForUpgrade[2];
            Answer3Image4.sprite = item4.ItemsNeededForUpgrade[3];
            Answer3Image5.sprite = item4.ItemsNeededForUpgrade[4];
            Answer3Image6.sprite = item4.ItemsNeededForUpgrade[5];

            Answer4Image1.sprite = item1.ItemsNeededForUpgrade[0];
            Answer4Image2.sprite = item1.ItemsNeededForUpgrade[1];
            Answer4Image3.sprite = item1.ItemsNeededForUpgrade[2];
            Answer4Image4.sprite = item1.ItemsNeededForUpgrade[3];
            Answer4Image5.sprite = item1.ItemsNeededForUpgrade[4];
            Answer4Image6.sprite = item1.ItemsNeededForUpgrade[5];

            RightButton = 4;
        }

        ReturnToUpgradableItems();
    }
}
