/*
  Class:  Media.cs
  Author: Nicholas J. Corkigian
  Date:   October 5, 2017

          This code is not to be altered.
*/


using System;
/// <summary>
/// Purpose: This abstract class represents a single object of a media type,
///          be it something like a book, movie, or song.
///          
///          Other classes must be derived from this class.
///          
///          Because it implements the ISearchable interface, all derived
///          classes will also need to implement the methods of that
///          interface as well.
/// </summary>
namespace Lab3A
{
    public abstract class Media : ISearchable
    {
        public string Title { get; protected set; }          // Title of the media object
        public int Year { get; protected set; }              // The year this media object was released

        /// <summary>
        /// Two-argument constructor sets the two properties that all Media objects have
        /// </summary>
        /// <param name="title">The title of the media object</param>
        /// <param name="year">The year of publication and/or release</param>

        public Media(string title, int year)
        {
            Title = title;
            Year = year;
        }
        public bool Search(string keyword) => Title.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0;

        public abstract void Display();
    }
}