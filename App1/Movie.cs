using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1
{
    class Movie
    {
        int id;
        String poster;
        String backDrop;
        String title;
        String overview;
        String releaseDate;
        String voteAvg;
        int suggestionScore;

        public Movie(int id, string poster, string title, string overview, string releaseDate, string voteAvg, String backDrop)
        {
            this.id = id;
            this.poster = poster;
            this.title = title;
            this.overview = overview;
            this.releaseDate = releaseDate;
            this.voteAvg = voteAvg;
            this.backDrop = backDrop;

        }
        public Movie(int id, string poster, string title, string overview, string releaseDate, string voteAvg, String backDrop, int suggestionScore)
        {
            this.id = id;
            this.poster = poster;
            this.title = title;
            this.overview = overview;
            this.releaseDate = releaseDate;
            this.voteAvg = voteAvg;
            this.backDrop = backDrop;
            this.suggestionScore = suggestionScore;
        }

        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public string Poster
        {
            get
            {
                return poster;
            }

            set
            {
                poster = value;
            }
        }

        public string Title
        {
            get
            {
                return title;
            }

            set
            {
                title = value;
            }
        }

        public string Overview
        {
            get
            {
                return overview;
            }

            set
            {
                overview = value;
            }
        }

        public string ReleaseDate
        {
            get
            {
                return releaseDate;
            }

            set
            {
                releaseDate = value;
            }
        }

        public string VoteAvg
        {
            get
            {
                return voteAvg;
            }

            set
            {
                voteAvg = value;
            }
        }

        public string BackDrop
        {
            get
            {
                return backDrop;
            }

            set
            {
                backDrop = value;
            }
        }

        public int SuggestionScore
        {
            get
            {
                return suggestionScore;
            }

            set
            {
                suggestionScore = value;
            }
        }
    }
}
