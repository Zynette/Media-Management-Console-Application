/*
 * Class: Book.cs
 * Author: Antonette Petallo
 * Date: October 2024
 * 
 * Statement of Authorship: I, Antonette Petallo, 000900501 certify that this material is my original work.
 * No other person's work has been used without due acknowledgement.
 */

using System;

namespace Lab3A
{
    /// <summary>
    /// Represents a book with author and summary.
    /// </summary>
    public class Book : Media, IEncryptable
    {
        /// <summary>
        /// Gets or sets the author of the book.
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// Gets or sets the encrypted summary of the book.
        /// </summary>
        public string Summary { get; set; }

        public Book(string title, int year, string author, string summary)
            : base(title, year)
        {
            Author = author;
            Summary = summary;
        }

        /// <summary>
        /// Encrypts the summary using a simple ROT13 algorithm.
        /// </summary>
        public void Encrypt() => Summary = ApplyRot13(Summary);

        /// <summary>
        /// Decrypts the summary using a simple ROT13 algorithm.
        /// </summary>
        public void Decrypt() => Summary = ApplyRot13(Summary);

        /// <summary>
        /// Displays information about the book.
        /// </summary>
        public override void Display()
        {
            Console.WriteLine($"Title: {Title}, Year: {Year}, Author: {Author}");
        }

        private string ApplyRot13(string input)
        {
            char[] array = input.ToCharArray();
            for (int i = 0; i < array.Length; i++)
            {
                int number = array[i];
                if (number >= 'a' && number <= 'z')
                    array[i] = (char)((number - 'a' + 13) % 26 + 'a');
                else if (number >= 'A' && number <= 'Z')
                    array[i] = (char)((number - 'A' + 13) % 26 + 'A');
            }
            return new string(array);
        }
    }
} 