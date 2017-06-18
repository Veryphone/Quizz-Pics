using UnityEngine;
using UnityEngine.UI;

public class RefreshQuizSelectionLevels : MonoBehaviour
{

    public static string selectedCategory = "";
    public static int levelSelected = -1;
    public GameObject levelImagePrefab;
    Word_Database Words_Database_Selected;
    int solvedWords;
    int totalWords;
    public void selectedThisGameObjectName ()
    {
        selectedCategory = gameObject.name;
        PlayerPrefs.SetString("SelectedCategory", selectedCategory);
    }
    void Start ()
    {

        if (GameObject.Find("Main Camera").transform.position.x == -11 && PlayerPrefs.HasKey("SelectedCategory") && gameObject.name == "Quiz_Selection_Controller")
        {
            refreshQuizLevels();
        }
        
    }
    public void refreshQuizLevels()
    {
        selectedCategory = PlayerPrefs.GetString("SelectedCategory");
        foreach (Transform transformObj in GameObject.Find("QUIZ_SELECTION").transform.FindChild("Canvas").transform.FindChild("Mask").transform.FindChild("ScrollRect").transform.FindChild("Levels_Container").transform)
        {
            DestroyImmediate(transformObj.gameObject);
        }

        Words_Database_Selected = GameObject.Find("DATABASES").transform.FindChild(selectedCategory).GetComponent<Word_Database>();

        GameObject.Find("QUIZ_SELECTION").transform.FindChild("Canvas").transform.FindChild("TopBox").transform.FindChild("Title").GetComponent<Text>().text = Words_Database_Selected.nameOfCategory[PlayerPrefs.GetInt("numberLanguae")];

        for (int i = 0; i < Words_Database_Selected.words_List.Length;)
        {
            for (int j = 0; j < 4; j++)
            {
                if (i < Words_Database_Selected.words_List.Length)
                {
                    GameObject go = Instantiate(GameObject.Find("Quiz_Selection_Controller").GetComponent<RefreshQuizSelectionLevels>().levelImagePrefab, new Vector3(-11, 0, 0), Quaternion.identity) as GameObject;
                    go.transform.SetParent(GameObject.Find("QUIZ_SELECTION").transform.FindChild("Canvas").transform.FindChild("Mask").transform.FindChild("ScrollRect").transform.FindChild("Levels_Container").transform);
                    go.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                    go.transform.FindChild("Image").GetComponent<Image>().sprite = Words_Database_Selected.image[i];
                    if (Words_Database_Selected.solvedWords[i])
                    {
                        go.transform.FindChild("SolvedQuiz").gameObject.SetActive(true);
                        go.GetComponent<Image>().color = Color.green;
                        solvedWords++;
                    }
                    totalWords++;
                    go.name = i.ToString();
                    i++;
                }
            }
        }

        GameObject.Find("Main Camera").transform.position = new Vector3(-11, 0, -10);

        GameObject.Find("QUIZ_SELECTION").transform.FindChild("Canvas").transform.FindChild("TopBox").transform.FindChild("SolvedWords").transform.FindChild("TotalSolvedWords").GetComponent<Text>().text = solvedWords + "/" + totalWords;
        GameObject.Find("QUIZ_SELECTION").transform.FindChild("Canvas").transform.FindChild("TopBox").transform.FindChild("TotalPorcent").transform.FindChild("TotalPorcent").GetComponent<Text>().text = ((solvedWords * 100) / totalWords).ToString() + "%";

    }

    public void setLevelSelected()
    {
          levelSelected = int.Parse(gameObject.name);
          Application.LoadLevel(Application.loadedLevelName);
          GameObject.Find("Main Camera").GetComponent<Animation>().Play("startScene");
          GameObject.Find("Main Camera").transform.position = new Vector3(0, 0, -10);
     
    }
}
