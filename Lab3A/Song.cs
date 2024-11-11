/*
 * Class: Song.cs
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
    /// Represents a song with album and artist.
    /// </summary>
    public class Song : Media
    {
        /// <summary>
        /// Gets or sets the album of the song.
        /// </summary>
        public string Album { get; set; }

        /// <summary>
        /// Gets or sets the artist of the song.
        /// </summary>
        public string Artist { get; set; }

        public Song(string title, int year, string album, string artist)
            : base(title, year)
        {
            Album = album;
            Artist = artist;
        }

        /// <summary>
        /// Displays information about the song.
        /// </summary>
        public override void Display()
        {
            Console.WriteLine($"Title: {Title}, Year: {Year}, Album: {Album}, Artist: {Artist}");
        }
    }
}