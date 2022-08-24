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
    private string pattern = @"^[A-Za-z]+$";
    List<User> arrayNames = new List<User>();
    Dictionary<User, int> scoreGame = new Dictionary<User, int>() { };
    string firstWord;
    List<string> arrayWords = new List<string>();
    Dictionary<char, int> arrayLetters = new Dictionary<char, int>() { };
    public void EnterUserNumber()
    {
      while (true)
      {
        Console.Write("111");
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
          else if (arrayNames.Any(elem => elem.name == name))
          {
            Console.WriteLine("The name already exist.");
          }
          else
          {
            User newUser = new(name);
            arrayNames.Add(newUser);
            scoreGame[newUser] = 0;
            break;
          }
        }
      }
    }

    public void EnterFirstWord()
    {
      while (true)
      {
        Console.Write($"{arrayNames[0].name} enters first word: ");
        firstWord = Console.ReadLine();
        if (firstWord.Length < 9 || firstWord.Length > 30)
        {
          Console.WriteLine("Word must be more than 8 and less than 30 symbols.");
        }
        else if (!Regex.IsMatch(firstWord, pattern))
        {
          Console.WriteLine("You can't use symbols, only letters.");
        }
        else
        {
          foreach (char letter in firstWord.Distinct())
          {
            int count = firstWord.Count(i => i == letter);
            arrayLetters[letter] = count;
          }
          arrayWords.Add(firstWord);
          scoreGame[arrayNames[0]] += 1;
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
        Console.Write($"{arrayNames[i].name} enters: ");
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

        check = checkWord(arrayWords, arrayLetters, result);
        if (!check)
        {
          DataGame dataGame = new DataGame();
          string dataScore = dataGame.getScoreGame(scoreGame);
          dataGame.writeResultGame(dataScore);

          Console.Write($"{arrayNames[i].name} lose.");
          break;
        }

        arrayWords.Add(result);
        scoreGame[arrayNames[i]] += 1;

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
      foreach (string elem in arrayWords)
      {
        Console.WriteLine($"{elem}");
      }
    }

    private void ScoreCurrentGame()
    {
      foreach (var elem in scoreGame)
      {
        Console.WriteLine(elem.Key.name + " - " + elem.Value + ";   ");
      }
    }

    private void ScoreAllTime()
    {
      DataGame dataGame = new DataGame();
      Console.WriteLine(dataGame.getScoreGame(scoreGame));
    }

    private bool checkWord(List<string> arrayWords, Dictionary<char, int> arrayLetters, string result)
    {
      var containsResult = arrayWords.Contains(result);
      if (containsResult) return false;

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
