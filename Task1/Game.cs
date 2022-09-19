using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Task1
{
  internal class Game
  {
    private byte playerNum;
    private const string pattern = @"^[A-Za-z]+$";
    string FirstWord;
    List<User> ArrayUsers = new List<User>();
    HashSet<String> ArrayWords = new HashSet<String>();
    Dictionary<char, int> ArrayLetters = new Dictionary<char, int>() { };
    public void EnterUserNumber()
    {
      while (true)
      {
        Console.Write("Enter number of players: ");
        string value = Console.ReadLine();
        bool checkParse = byte.TryParse(value, out playerNum);

        if (!checkParse)
        {
          Console.WriteLine("The number of players can be from 1 to 255. Please enter a valid value.");
        }
        else
        {
          break;
        }
      }
    }
    public void EnterName()
    {
      for (int k = 1; k <= playerNum; k++)
      {
        while (true)
        {
          Console.Write("User " + k + " enters your name: ");
          string name = Console.ReadLine();
          if (!Regex.IsMatch(name, pattern))
          {
            Console.WriteLine("You can't use symbols, only letters.");
          }
          else if (ArrayUsers.Any(elem => elem.Name == name))
          {
            Console.WriteLine("The name already exist.");
          }
          else
          {
            User newUser = new(name, 0);
            ArrayUsers.Add(newUser);
            break;
          }
        }
      }
    }

    public void EnterFirstWord()
    {
      while (true)
      {
        Console.Write($"{ArrayUsers[0].Name} enters first word: ");
        FirstWord = Console.ReadLine();
        if (FirstWord.Length < 9 || FirstWord.Length > 30)
        {
          Console.WriteLine("Word must be more than 8 and less than 30 symbols.");
        }
        else if (!Regex.IsMatch(FirstWord, pattern))
        {
          Console.WriteLine("You can't use symbols, only letters.");
        }
        else
        {
          foreach (char letter in FirstWord.Distinct())
          {
            int count = FirstWord.Count(i => i == letter);
            ArrayLetters[letter] = count;
          }
          ArrayWords.Add(FirstWord);
          ArrayUsers[0].Point += 1;
          break;
        }
      }
    }

    public void GameProcess ()
    {
      string? result;
      bool check = true;
      int i = 1;

      while (check)
      {
        Console.Write($"{ArrayUsers[i].Name} enters: ");
        result = Console.ReadLine();

        var Commands = new Dictionary<string, Action>()
        {
          {"/show-words", ShowAllWords },
          {"/score", ScoreCurrentGame },
          {"/total-score", ScoreAllTime }
        };
       
        // check for command enter
        if (result.Trim().StartsWith("/"))
        {
          Commands[result.Trim()]();
          if (i != 0) i -= 1;
          continue;
        }
        
        if (!Regex.IsMatch(result, pattern))
        {
          Console.Write("You can't use symbols, only letters. \nUser " + i + " enters: ");
          result = Console.ReadLine();
        }

        check = checkWord(ArrayWords, ArrayLetters, result);
        if (!check)
        {
          IDataService _service = new DataGame();
          _service.WriteResultGame(GetScoringPoints(ArrayUsers));

          Console.Write($"{ArrayUsers[i].Name} lose.");
          break;
        }

        ArrayWords.Add(result);
        ArrayUsers[i].Point += 1;

        if (i == playerNum - 1)
        {
          i = 0;
        }
        else
        {
          i++;
        }
      }
    }

    private void ShowAllWords()
    {
      foreach (string elem in ArrayWords)
      {
        Console.WriteLine($"{elem}");
      }
    }

    private void ScoreCurrentGame()
    {
      foreach (var elem in ArrayUsers)
      {
        Console.WriteLine(elem.Name + " - " + elem.Point + ";   ");
      }
    }

    private void ScoreAllTime()
    {
      foreach (var elem in GetScoringPoints(ArrayUsers))
      {
        Console.WriteLine(elem.Name + " - " + elem.Point + ";   ");
      }
    }

    private List<User> GetScoringPoints(List<User> arrayUsers)
    {
      IDataService _service = new DataGame();
      foreach (var elem in _service.GetScoreGame())
      {
        //if the player has already played the current result, add the previous one
        if (arrayUsers.Contains(elem))
        {
          var index = arrayUsers.FindIndex(user => user.Name == elem.Name);
          arrayUsers[index].Point += elem.Point;
        }
        else
        {
          arrayUsers.Add(elem);
        }
      }
      return arrayUsers;
    }

    private bool checkWord(HashSet<string> arrayWords, Dictionary<char, int> arrayLetters, string result)
    {
      bool isAdded = arrayWords.Add(result);
      if (!isAdded) return false;

      foreach (var elem in arrayLetters)
      {
        if (!result.Contains(elem.Key)) return false;
        int count = result.Count(i => i == elem.Key);
        if (count != elem.Value) return false;
      }

      return true;
    }

  }
}
