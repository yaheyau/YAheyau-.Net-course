using System;
using System.Text.RegularExpressions;

namespace Task1
{
  internal class Program
  {
    static void Main(string[] args)
    {
      byte playerNum;
      while (true)
      {
        Console.Write("Enter number of players: ");
        string value = Console.ReadLine();
        bool checkParse = byte.TryParse(value, out playerNum);

        if (!checkParse)
        {
          Console.WriteLine("The number of players can be from 1 to 255. Please enter a valid value.");
        } else
        {
          break;
        }
      }
      

      string? firstWord;
      string pattern = @"^[A-Za-z]+$";
      List<string> arrayWords = new List<string>();
      var arrayLetters = new Dictionary<char, int>(){};
      while (true)
      {
        Console.Write("User 1 enters first word: ");
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
          break;
        }
      }

      string? result;
      bool check = true;
      int i = 1;
      while (check)
      {
        i++;
        Console.Write("User " + i + " enters: ");
        result = Console.ReadLine();

        if (!Regex.IsMatch(result, pattern))
        {
          Console.Write("You can't use symbols, only letters. \nUser " + i + " enters: ");
          result = Console.ReadLine();
        }

        check = checkWord(arrayWords, arrayLetters, result);
        if (!check)
        {
          Console.Write("User " + i + " lose.");
          break;
        }
        arrayWords.Add(result);
        if (i == playerNum) i = 0;
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
