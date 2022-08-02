using System;

namespace Task1
{
  internal class Program
  {
    static void Main(string[] args)
    {
      Console.Write("Enter number of players: ");
      byte playerNum = Convert.ToByte(Console.ReadLine());
      string? word;
      while (true)
      {
        Console.Write("User 1 enters first word: ");
        word = Console.ReadLine();
        if (word.Length < 9 || word.Length > 30)
        {
          Console.WriteLine("Word must be more than 8 and less than 30 symbols.");
        }
        else
        {
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
        check = checkWord(word, result);
        if (!check)
        {
          Console.Write("User " + i + " lose.");
          break;
        }
        if (i == playerNum) i = 0;
      }
      
    }

    static bool checkWord(string word, string result)
    {
      foreach (char ch in word)
      {
        bool check = result.Contains(ch);
        if (!check) return false;
      }
      return true;
    }
  }
}
