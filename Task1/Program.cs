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
