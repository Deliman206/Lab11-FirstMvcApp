# Lab11-FirstMvcApp
ASP.Net MVC app with html and css
 
 This ASP.Net Core MVC application allows the user to look up Time Magazine "Man of the Year" by the year in which they were awarded.

## Usage
At the home page the user has the ability to enter two dates, a start date and an end date, and submit to reveal information about all individuals who won the "Man of the Year" award between those dates.
The available years range from 1927 to 2016

## How it works
This application uses the MVC design concept to prop-up Views to the browser based on the Model of  the class TimePerson. How the application handled, in terms of changing the View in the brower, is done in the Controller. 

### Controller

The Controller has a simple job, prop the inital View when the user goes to the web application, redirect the user to the results page after the submission event fires (ultimately handling the filtering of the "Man of the Year" file), and finally prop the results View with the relevant information.

```c#
public class HomeController: Controller
{
[HttpGet]
public IActionResult Index()
{
return View();
}

[HttpPost]
public IActionResult Index(int startYear, int endYear)
{
return RedirectToAction("Results", new { startYear, endYear });
}

[HttpGet]
public IActionResult Results(int startYear, int endYear)
{
return View(TimePerson.GetPersons(startYear, endYear));
}
}
```

### View
The View is what the user sees when they travel from one page of the web application to another. In this web application there are two Views, the Home Page and the Results Page.
The View contains a mix of HTML5, CSS & Code to help build the UI and UX of the application.

#### Home Page
```html
<!DOCTYPE html>
<html>
<head>
<title>Home Page</title>
</head>
<body>
<div style="border-style: solid; width: 40%; margin: auto; margin-top:10%; background-color:red;">
<h1 style="text-align: center; color: white; font-size: 72px">TIME</h1>
<h2 style="text-align: center;">Man of the Year</h2>
<form method="post" style="text-align: center; margin-top:10%; margin-bottom:10%;">
<div style=" display: inline-block;">
<label style=" display: block; color: white; font-size: 25px; margin-right:3%;">Start Year</label>
<input type="number" name="startYear" />
</div>
<div style=" display: inline-block; margin-bottom:10%; margin-left:3%;">
<label style=" display: block; color: white; font-size: 25px;">End Year</label>
<input type="number" name="endYear" />
</div>

<button type="submit" style=" display: block; margin: auto;">Submit</button>

</form>
</div>
</body>
</html>
```

#### Results Page
```html
<!DOCTYPE html>
<html>
<body>

<table>
<tr style="border: solid 1px black; border-bottom:5px solid black;">
<th>Year</th>
<th>Name</th>
<th>Honor</th>
<th>Country</th>
<th>BirthYear</th>
<th>DeathYear</th>
<th>Title</th>
<th>Context</th>
<th>Category</th>
</tr>
@foreach(TimePerson person in Model)
{
<tr style="border: solid 1px black; border-bottom:5px solid black;">
<th>@person.Year</th>
<th>@person.Name</th>
<th>@person.Honor</th>
<th>@person.Country</th>
<th>
@if (person.BirthYear != 0)
{
@person.BirthYear
}
</th>
<th>
@if (person.DeathYear != 0)
{
@person.DeathYear
}
</th>
<th>@person.Title</th>
<th>@person.Context</th>
<th>@person.Category</th>
</tr>
}
</table>
</body>
</html>
```

### Model
This could be considered the most important part of the MVC process as it handles all the logic for what will happen on events.
In the Model this web application defines the class TimePerson along with its properties and methods.
It is also here that the application reads in the .csv document which holds the data for the "Man of the Year" awards.

It is clear in the following code that the method GetPersons uses StreamReader to read in all the data from the .csv file assigned to the variable "filepath" and then creates a collection which can then be queried and organized for what the user is looking for.

```c#
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
```


