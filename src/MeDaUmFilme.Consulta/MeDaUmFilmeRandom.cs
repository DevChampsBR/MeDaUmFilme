using System;
using System.Collections;
using System.Collections.Generic;

namespace MeDaUmFilme
{
    public class MeDaUmFilmeRandom
    {
        private readonly IList<string> _titles;
        private readonly IList<string> _years;

        private static int INITIAL_YEAR = DateTime.Now.AddYears(-10).Year;
        private static int FINAL_YEAR = DateTime.Now.Year;

        public MeDaUmFilmeRandom()
        {
            _titles = new List<string>()
            {
                "horror",
                "super",
                "love",
                "vampire",
                "blood",
                "space",
                "mars",
                "glory"
            };

            _years = new List<string>();

            for (int year = INITIAL_YEAR; year <= FINAL_YEAR; year++)
            {
                _years.Add(year.ToString());
            }
        }

        private int GetRandomTitlePosition
        {
            get
            {
                Random rndTitle = new Random();
                return rndTitle.Next(0, _titles.Count - 1);
            }
        }

        private int GetRandomYearPosition
        {
            get
            {
                Random rndYear = new Random();
                return rndYear.Next(0, _years.Count - 1);
            }
        }

        public string GetRandomTitle
        {
            get
            {
                int position = GetRandomTitlePosition;
                return _titles[position];
            }
        }

        public string GetRandomYear
        {
            get
            {
                int position = GetRandomYearPosition;
                return _years[position];
            }
        }
    }
}
