using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Task1
{
  internal class DataGame : IDataService
  {
    private const string path = "../../../score.json";
    public List<User> GetScoreGame()
    {
      List<User> result = new List<User>();
      try
      {
        using (StreamReader reader = new StreamReader(path))
        {
          JsonSerializer serializer = new JsonSerializer();
          result = (List<User>)serializer.Deserialize(reader, typeof(List<User>));
          reader.Close();
        }
      }
      catch (FileNotFoundException e)
      {
        Console.WriteLine(e.Message);
      }
      return result;
    }

    public void WriteResultGame (List<User> scoreGame)
    {
      using (StreamWriter file = File.CreateText(path))
      {
        JsonSerializer serializer = new JsonSerializer();
        serializer.Serialize(file, scoreGame);
        Console.WriteLine("Data has been saved to file");
      }
    }
  }
}
