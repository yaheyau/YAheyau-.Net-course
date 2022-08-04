using System;

namespace Task1 {
	// code formattings ctrl+k+f
	// program do not contain any methods
	internal class Program {
		static void Main(string[] args) {
			string? word;
			while (true) {
				Console.Write("User1 enters first word: ");
				word = Console.ReadLine();
				// I can pass here any char I want
				// *7Ju123d^&323e - is valid in this program
				if (word.Length < 9 || word.Length > 30) {
					Console.WriteLine("Word must be more than 8 and less than 30 symbols.");
				} else {
					break;
				}
			}

			string? result;
			string user = "";
			// unnessesary action, bcs string is char array
			char[] chars = word.ToCharArray();
			bool check = true;
			int i = 1;
			while (check) {
				i++;
				// what if we'll allow to add 3,4,5.. users?:) 
				user = i % 2 == 0 ? "User 2" : "User 1";
				Console.Write(user + " enters: ");
				result = Console.ReadLine();
				// no validation on enetered word, 
				// words length can be < than entered word, but have to pass validation as well
				// no checks on count of used chars 
				if (result.Length != chars.Length) break;
				
				foreach (char ch in chars) {
					check = result.Contains(ch);
					if (!check) break;
				}
			}
			Console.Write($"{user} lose.");
		}
	}
}