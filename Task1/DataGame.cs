using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
  internal class DataGame
  {
    private const string path = "../../../score.txt";
    public string getScoreGame(Dictionary<User, int> scoreGame)
    {
      string toString = "";
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
              //if the player has already played the current result, add the previous one
              if (scoreGame.ContainsKey(new User(keyValue[0])))
              {
                toString += keyValue[0] + ": " + (int.Parse(keyValue[1].Trim(';')) + scoreGame[new User(keyValue[0])]) + ";\n";
                scoreGame.Remove(new User(keyValue[0]));
              }
              else
              {
                toString += keyValue[0] + ": " + keyValue[1].Trim(';') + ";\n";
              }
            }
          }

          //played for the first time
          foreach (var elem in scoreGame)
          {
            toString += elem.Key.name + ": " + elem.Value + ";\n";
          }
          reader.Close();
        }
      }
      catch (FileNotFoundException e)
      {
        foreach (var elem in scoreGame)
        {
          toString += elem.Key.name + ": " + elem.Value + ";\n";
        }
      }
      return toString;
    }

    public void writeResultGame (string toString)
    {
      using (StreamWriter writer = new StreamWriter(path, false))
      {
        writer.WriteLineAsync(toString);
        writer.Close();
      }
    }
  }
}
