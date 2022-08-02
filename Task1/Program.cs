using System;

namespace Task1 {
	internal class Program {
		static void Main(string[] args) {
			string? word;
			while (true) {
				Console.Write("User1 enters first word: ");
				word = Console.ReadLine();
				if (word.Length < 9 || word.Length > 30) {
					Console.WriteLine("Word must be more than 8 and less than 30 symbols.");
				} else {
					break;
				}
			}

			string? result;
			string user = "";
			char[] chars = word.ToCharArray();
			bool check = true;
			int i = 1;
			while (check) {
				i++;
				user = i % 2 == 0 ? "User 2" : "User 1";
				Console.Write(user + " enters: ");
				result = Console.ReadLine();
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