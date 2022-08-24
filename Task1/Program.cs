using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Task1
{

internal class Program
  {

    static void Main(string[] args)
    {
      string path = "../../../result.txt";
      byte playerNum;
      string? firstWord;
      string pattern = @"^[A-Za-z]+$";
      List<string> arrayNames = new List<string>();
      List<string> arrayWords = new List<string>();
      var scoreGame = new Dictionary<string, int>() { };
      var arrayLetters = new Dictionary<char, int>() { };

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
          else if (arrayNames.Contains(name))
          {
            Console.WriteLine("The name already exist.");
          }
          else
          {
            arrayNames.Add(name);
            scoreGame[name] = 0;
            break;
          }
        }
      }

      while (true)
      {
        Console.Write($"{arrayNames[0]} enters first word: ");
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

      string? result;
      bool check = true;
      int i = 1;
      while (check)
      {
        Console.Write($"{arrayNames[i]} enters: ");
        result = Console.ReadLine();

        if (result.Equals("/show-words"))
        {
          foreach (string elem in arrayWords)
          {
            Console.WriteLine($"{elem}");
          }
          if (i != 0) i -= 1;
          continue;
        }

        if (result.Equals("/score"))
        {
          foreach(var elem in scoreGame)
          {
            Console.WriteLine(elem.Key + " - " + elem.Value + ";   ");
          }
          if (i != 0) i -= 1;
          continue;
        }

        if (result.Equals("/total-score"))
        {
          var copyScoreGame = scoreGame;
          // if the file doesn't exist print the data for the current game
          try
          {
            using (StreamReader reader = new StreamReader(path))
            {
              string? line;
              while ((line = reader.ReadLine()) != null)
              {
                if (line != "")
                {
                  string[] keyValue = line.TrimStart('\n').Split(':');
                  // if the player has already played the current result, add the previous one
                  if (copyScoreGame.ContainsKey(keyValue[0]))
                  {
                    Console.WriteLine(keyValue[0] + " - " + (int.Parse(keyValue[1].Trim(';')) + copyScoreGame[keyValue[0]]));
                    copyScoreGame.Remove(keyValue[0]);
                  }
                  else
                  {
                    Console.WriteLine(keyValue[0] + " - " + keyValue[1].Trim(';'));
                  }
                }
              }

              //played for the first time
              foreach (var item in copyScoreGame)
              {
                Console.WriteLine(item.Key + " - " + item.Value);
              }
              reader.Close();
            }
          } 
          catch (FileNotFoundException e)
          {
            foreach (var item in copyScoreGame)
            {
              Console.WriteLine(item.Key + " - " + item.Value);
            }
          }
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
          string toString = "";
          var copyScoreGame = scoreGame;

          // if the file doesn't exist print the data for the current game
          try
          {
            using (StreamReader reader = new StreamReader(path))
            {
              string? line;
              while ((line = reader.ReadLine()) != null)
              {
                if (line != "")
                {
                  string[] keyValue = line.TrimStart('\n').Split(':');
                  // if the player has already played the current result, add the previous one
                  if (copyScoreGame.ContainsKey(keyValue[0]))
                  {
                    toString += keyValue[0] + ": " + (int.Parse(keyValue[1].Trim(';')) + copyScoreGame[keyValue[0]]) + ";\n";
                    copyScoreGame.Remove(keyValue[0]);
                  }
                  else
                  {
                    toString += keyValue[0] + ": " + keyValue[1].Trim(';') + ";\n";
                  }
                }
              }

              //played for the first time
              foreach (var elem in copyScoreGame)
              {
                toString += elem.Key + ": " + elem.Value + ";\n";
              }
              reader.Close();
            }
          }
          catch (FileNotFoundException e)
          {
            foreach (var elem in scoreGame)
            {
              toString += elem.Key + ": " + elem.Value + ";\n";
            }
          }

          using (StreamWriter writer = new StreamWriter(path, false))
          {
            writer.WriteLineAsync(toString);
            writer.Close();
          }

          Console.Write($"{arrayNames[i]} lose.");
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

    static bool checkWord(List<string> arrayWords, Dictionary<char, int> arrayLetters, string result)
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
