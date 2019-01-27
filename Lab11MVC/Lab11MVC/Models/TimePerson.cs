using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Lab11MVC.Models
{
    public class TimePerson
    {
        public int Year { get; set; }
        public string Honor { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public int BirthYear { get; set; }
        public int DeathYear { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string Context { get; set; }

        public TimePerson(int year, string honor, string name, string country, int birthYear, int deathYear, string title, string category, string context)
        {
            Year = year;
            Honor = honor;
            Name = name;
            Country = country;
            BirthYear = birthYear;
            DeathYear = deathYear;
            Title = title;
            Category = category;
            Context = context;
        }
        /// <summary>
        /// Get all the people in the requested time range
        /// </summary>
        /// <param name="startYear"></param>
        /// <param name="endYear"></param>
        /// <returns></returns>
        public static List<TimePerson> GetPersons(int startYear, int endYear)
        {
            string filepath = @"\\mac\Home\codefellows\dn401\Lab11-FirstMvcApp\Lab11MVC\Lab11MVC\wwwroot\personOfTheYear.csv";
            List <string> allPeople = Collection(filepath);
            
            var query = from line in allPeople
                        let data = line.Split(',')
                        where data[0] != "Year"
                        select new TimePerson(
                            Convert.ToInt32(data[0]),
                            data[1],
                            data[2],
                            data[3],
                            (data[4] == "") ? 0 : Convert.ToInt32(data[4]),
                            (data[5] == "") ? 0 : Convert.ToInt32(data[5]),
                            data[6],
                            data[7],
                            data[8]
                            );

            List<TimePerson> requestedPeople = new List<TimePerson>();

            foreach (TimePerson person in query)
            {
                if (person.Year >= startYear && person.Year <= endYear)
                    requestedPeople.Add(person);
            }

            return requestedPeople;
        }
        /// <summary>
        /// Create new StreamReader for data at file path
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns>List<string> of all data</returns>
        static List<string> Collection(string filepath)
        {
            var reader = new StreamReader(File.OpenRead(filepath));
            List<string> lines = new List<string>();

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                lines.Add(line);
            }
            return lines;
        }
    }
}
