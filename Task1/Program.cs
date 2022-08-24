using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using static Task1.Game;

namespace Task1
{

internal class Program
  {

    static void Main(string[] args)
    { 
      Game game = new Game();
      game.EnterUserNumber();
      game.EnterName();
      game.EnterFirstWord();
      game.GameProcess();
    }
  }
}
