/*
 * Class: Program.cs
 * Author: Antonette Petallo
 * Date: October 2024
 * 
 * Statement of Authorship: I, Antonette Petallo, 000900501 certify that this material is my original work.
 * No other person's work has been used without due acknowledgement.
 */
using System;
using System.Collections.Generic;
using System.IO;

namespace Lab3A
{
    class Program
    {
        private static List<Media> mediaList = new List<Media>();

        /// <summary>
        /// Reads data from the Data.txt file and loads media objects into mediaList.
        /// </summary>
        public static void ReadData()
        {
            try
            {
                string[] lines = File.ReadAllLines("Data.txt");
                List<string> entryBuffer = new List<string>();

                foreach (string line in lines)
                {
                    // If line is a delimiter, process the accumulated entry
                    if (line.Trim() == "-----")
                    {
                        ProcessEntry(entryBuffer);
                        entryBuffer.Clear(); // Clear buffer for next entry
                    }
                    else
                    {
                        // Add non-delimiter lines to buffer
                        entryBuffer.Add(line);
                    }
                }

                // Process any remaining entry after the loop
                if (entryBuffer.Count > 0)
                {
                    ProcessEntry(entryBuffer);
                }

                Console.WriteLine("Data loading complete.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading file: " + ex.Message);
            }
        }

        /// <summary>
        /// Processes a single media entry from buffered lines.
        /// </summary>
        private static void ProcessEntry(List<string> entryBuffer)
        {
            // Ensure entryBuffer is not empty
            if (entryBuffer.Count == 0) return;

            // Split the first line to determine the type
            string[] parts = entryBuffer[0].Split('|');
            if (parts.Length < 4) return; // Skip invalid entries

            string type = parts[0];
            string title = parts[1];
            int year = int.Parse(parts[2]);
            Media media = null;

            if (type == "SONG")
            {
                // Songs are single-line entries with title, year, album, and artist
                string album = parts[3];
                string artist = entryBuffer.Count > 1 ? entryBuffer[1] : ""; // No summary for songs
                media = new Song(title, year, album, artist);
                Console.WriteLine($"Loaded Song: {title}");
            }
            else if (type == "BOOK")
            {
                // Books have multi-line summaries
                string author = parts[3];
                string summary = string.Join(" ", entryBuffer.GetRange(1, entryBuffer.Count - 1));
                media = new Book(title, year, author, summary);
                Console.WriteLine($"Loaded Book: {title}");
            }
            else if (type == "MOVIE")
            {
                // Movies have multi-line summaries
                string director = parts[3];
                string summary = string.Join(" ", entryBuffer.GetRange(1, entryBuffer.Count - 1));
                media = new Movie(title, year, director, summary);
                Console.WriteLine($"Loaded Movie: {title}");
            }

            if (media is IEncryptable encryptable)
            {
                encryptable.Decrypt(); // Decrypt summary for books and movies
            }

            if (media != null)
            {
                mediaList.Add(media);
            }
        }

        /// <summary>
        /// Displays the main menu and handles user input for displaying media data.
        /// </summary>
        public static void DisplayMenu()
        {
            while (true)
            {
                Console.WriteLine("1. List All Books\n2. List All Movies\n3. List All Songs\n4. List All Media\n5. Search All Media by Title\n6. Exit Program");
                Console.Write("Enter your choice: ");
                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    switch (choice)
                    {
                        case 1: ListMedia("BOOK"); break;
                        case 2: ListMedia("MOVIE"); break;
                        case 3: ListMedia("SONG"); break;
                        case 4: ListMedia(); break;
                        case 5: SearchMedia(); break;
                        case 6: return;
                        default: Console.WriteLine("Invalid choice. Please try again."); break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                }
            }
        }

        /// <summary>
        /// Lists media objects based on type or all if type is null.
        /// </summary>
        private static void ListMedia(string type = null)
        {
            bool found = false; // Track if any items are found

            foreach (var media in mediaList)
            {
                string mediaType = media.GetType().Name.ToUpper(); // Get the type name in uppercase

                // If no specific type is given, or if the media type matches the requested type
                if (type == null || mediaType == type)
                {
                    media.Display();
                    found = true;
                }
            }

            if (!found)
            {
                Console.WriteLine($"No items found for type: {type}");
            }
        }

        /// <summary>
        /// Searches all media by title keyword and displays matched results.
        /// </summary>
        private static void SearchMedia()
        {
            Console.Write("Enter search keyword: ");
            string keyword = Console.ReadLine();
            foreach (var media in mediaList)
            {
                if (media.Search(keyword))
                {
                    media.Display();
                    if (media is IEncryptable encryptable)
                    {
                        Console.WriteLine($"Summary: {((dynamic)media).Summary}");
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            ReadData();
            DisplayMenu();
        }
    }
}