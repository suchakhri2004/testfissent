﻿using System;
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
                Console.WriteLine("โอกาสการทายคำ : " + hangman.GetRemainingTries());

                Console.Write("กรอกตัวอักษร : ");
                char input = Console.ReadKey().KeyChar;
                Console.WriteLine();

                GuessResult result = hangman.Input(input);

                switch (result)
                {
                    case GuessResult.Correct:
                        Console.WriteLine("ถูกต้อง!");
                        break;
                    case GuessResult.Incorrect:
                        Console.WriteLine("ไม่ถูกต้อง.");
                        break;
                    case GuessResult.Duplicate:
                        Console.WriteLine("พร้อมหรือยังที่จะทาย");
                        break;
                }

                if (hangman.IsWin())
                {
                    Console.WriteLine("ยินดีด้วยคุณชนะ");
                    hangman.Restart();
                }
                else if (hangman.IsGameOver())
                {
                    Console.WriteLine("คุณแพ้. คำนั้นคือ: " + hangman.GetCorrectWord());
                    hangman.Restart();
                }

                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }
    }
}