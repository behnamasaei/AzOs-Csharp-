// See https://aka.ms/new-console-template for more information

var boys = new List<string>
    { "ali", "reza", "yasin", "benyamin", "mehrdad", "sajjad", "aidin", "shahin"};

var girls = new List<string>
    { "sara", "zari", "neda", "homa", "eli", "goli", "mary", "mina"};

var random = new Random();
do
{
    var boy = boys[random.Next(boys.Count)];
    var girl = girls[random.Next(girls.Count)];
    Console.WriteLine($"{boy} with {girl}");
    boys.Remove(boy);
    girls.Remove(girl);
} while (boys.Count>0);
