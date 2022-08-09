using System;
using System.Text.RegularExpressions;

namespace Task1
{
  internal class Program
  {
    static void Main(string[] args)
    {
      Console.Write("Enter number of players: ");
      byte playerNum = Convert.ToByte(Console.ReadLine());

      string? firstWord;
      string pattern = @"^[A-Za-z]+$";
      string[] arrayWords = new String[0];
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
          foreach (char letter in firstWord.Distinct().ToArray())
          {
            int count = firstWord.Count(i => i == letter);
            arrayLetters[letter] = count;
          }
          Array.Resize(ref arrayWords, arrayWords.Length + 1);
          arrayWords[0] = firstWord;
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

        check = checkWord(firstWord, arrayWords, arrayLetters, result);
        if (!check)
        {
          Console.Write("User " + i + " lose.");
          break;
        }
        Array.Resize(ref arrayWords, arrayWords.Length + 1);
        arrayWords[arrayWords.Length-1] = result;
        if (i == playerNum) i = 0;
      }
      
    }

    static bool checkWord(string firstWord, string[] arrayWords, Dictionary<char, int> arrayLetters, string result)
    {
      foreach (string str in arrayWords)
      {
        if (str == result) return false;
      }

      foreach (char ch in firstWord)
      {
        bool check = result.Contains(ch);
        if (!check) return false;
      }

      foreach (char letter in result.Distinct().ToArray())
      {
        int count = result.Count(i => i == letter);

        foreach (var elem in arrayLetters)
        {
          if (letter == elem.Key && count != elem.Value) return false;
        }
      }

      return true;
    }
  }
}
