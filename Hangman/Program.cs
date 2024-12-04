using System;
using System.Collections.Generic;

namespace HangmanGame
{
    public enum GuessResult
    {
        Correct = 0,
        Incorrect = 1,
        Duplicate = 2
    }

    public class HangmanService
    {
        private string[] words = { "Softsquaregroup", "Traveller", "Developer" };
        private string wordToGuess = string.Empty; 
        private HashSet<char> guessedLetters = new HashSet<char>(); 
        private int maxTries;
        private int remainingTries;
        private char[] displayWord = Array.Empty<char>(); 

        public HangmanService()
        {
            maxTries = 10;
            Restart();
        }

        public void Restart()
        {
            Random random = new Random();
            wordToGuess = words[random.Next(words.Length)].ToUpper();
            guessedLetters.Clear();
            remainingTries = maxTries;
            displayWord = new string('_', wordToGuess.Length).ToCharArray();
        }

        public string GetDisplay()
        {
            return new string(displayWord);
        }

        public GuessResult Input(char guess)
        {
            guess = char.ToUpper(guess);

            if (guessedLetters.Contains(guess))
            {
                return GuessResult.Duplicate;
            }

            guessedLetters.Add(guess);

            if (wordToGuess.Contains(guess))
            {
                for (int i = 0; i < wordToGuess.Length; i++)
                {
                    if (wordToGuess[i] == guess)
                    {
                        displayWord[i] = guess;
                    }
                }
                return GuessResult.Correct;
            }
            else
            {
                remainingTries--;
                return GuessResult.Incorrect;
            }
        }

        public int GetRemainingTries()
        {
            return remainingTries;
        }

        public bool IsWin()
        {
            return new string(displayWord) == wordToGuess;
        }

        public bool IsGameOver()
        {
            return remainingTries <= 0;
        }

        public string GetCorrectWord()
        {
            return wordToGuess;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            HangmanService hangman = new HangmanService();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Hangman Game");
                Console.WriteLine("คำ: " + hangman.GetDisplay());
                Console.WriteLine("โอกาสการทายคำ: " + hangman.GetRemainingTries());
                Console.WriteLine("กรอกตัวอักษร หรือ ปล่อยว่างแล้วกด Enter เพื่อเริ่มเกมใหม่");

                string input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("เริ่มเกมใหม่...");
                    hangman.Restart();
                    continue;
                }

                char guess = input.ToUpper()[0];

                GuessResult result = hangman.Input(guess);

                switch (result)
                {
                    case GuessResult.Correct:
                        Console.WriteLine("ถูกต้อง!");
                        break;
                    case GuessResult.Incorrect:
                        Console.WriteLine("ไม่ถูกต้อง.");
                        break;
                    case GuessResult.Duplicate:
                        Console.WriteLine("คุณเคยทายตัวอักษรนี้แล้ว.");
                        break;
                }

                if (hangman.IsWin())
                {
                    Console.WriteLine("ยินดีด้วย! คุณชนะ! คำนั้นคือ " + hangman.GetCorrectWord());
                    hangman.Restart();
                }
                else if (hangman.IsGameOver())
                {
                    Console.WriteLine("คุณแพ้. คำที่ถูกต้องคือ: " + hangman.GetCorrectWord());
                    hangman.Restart();
                }

                Console.WriteLine("กดปุ่มใด ๆ เพื่อดำเนินการต่อ");
                Console.ReadKey();
            }
        }
    }
}
