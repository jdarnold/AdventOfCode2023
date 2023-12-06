// Pretty much every AoC project starts with this code, so let's create a template for it.
using System.Diagnostics;
using System.Security.Cryptography;
using System.Threading.Tasks.Sources;

class Program
{
    class Seed
    {
        public long Number { get; set; }
        public long Range { get; set; }
        public long Soil { get; set; }
        public long Fertilizer { get; set; }
        public long Water { get; set; }
        public long Light { get; set; }
        public long Temperature { get; set; }
        public long Humidity { get; set; }
        public long Location { get; set; }
    };

    class MapSection
    {
        long destinationStart;
        long sourceStart;
        long rangeLength;

        public MapSection(long destinationStart, long sourceStart, long rangeLength)
        {
            this.destinationStart = destinationStart;
            this.sourceStart = sourceStart;
            this.rangeLength = rangeLength;
        }

        public long DestinationNumber( long source )
        {
            if ( source < sourceStart || source >= (sourceStart+rangeLength) )
                return source;

            return destinationStart + (source-sourceStart);
        }
    }

    class Map
    {
        public List<MapSection> sections = [];
        public long GetDestination(long source)
        {
            foreach(var section in sections)
            {
                var ns = section.DestinationNumber(source);
                if ( ns != source )
                    return ns;
            }

            return source;
        }
        public void AddMapData( string line )
        {
            string[] mapData = line.Split(' ');

            var ds = long.Parse(mapData[0]);
            var ss = long.Parse(mapData[1]);
            var rl = long.Parse(mapData[2]);
            MapSection ms = new MapSection(ds, ss, rl);
            sections.Add(ms);
            //Console.WriteLine($"Adding mapsection {ds}, {ss}, {rl}");

        }
    }

    private enum AlmanacState
    {
        None,
        Soil,
        Fertilizer,
        Water,
        Light,
        Temperature,
        Humidity,
        Location,
    }
    static void Main()
    {
        
        string filePath = "../../../input.txt";

        // Check if the file exists
        if (File.Exists(filePath))
        {
            // Read all lines from the file
            string[] lines = File.ReadAllLines(filePath);

            List<Seed> seeds = [];

            Map soil = new();
            Map fertilizer = new();
            Map water = new();
            Map light = new();
            Map temperature = new();
            Map humidity = new();
            Map location = new();
            AlmanacState aState = AlmanacState.None;
            
            foreach (string line in lines)
            {
                if ( line.Contains("seeds:") )
                { // parse "seeds:" line
                    string[] seed = line.Split(' ');
                    for ( int i = 1; i < seed.Length; i += 2 )
                    {
                        Seed s = new()
                        {
                            Number = long.Parse(seed[i]),
                            Range = long.Parse(seed[i + 1])
                        };
                        seeds.Add(s);
                        Console.WriteLine($"Adding new seed {s.Number}");
                    }
                }
                else if (line.Contains("to-fertilizer"))
                { // just skip this line. We'll assume they are always in the same order
                    aState = AlmanacState.Fertilizer;
                }
                else if (line.Contains("to-soil"))
                { // just skip this line. We'll assume they are always in the same order
                    aState = AlmanacState.Soil;
                }
                else if (line.Contains("to-water"))
                { // just skip this line. We'll assume they are always in the same order
                    aState = AlmanacState.Water;
                }
                else if (line.Contains("to-light"))
                { // just skip this line. We'll assume they are always in the same order
                    aState = AlmanacState.Light;
                }
                else if (line.Contains("to-temperature"))
                { // just skip this line. We'll assume they are always in the same order
                    aState = AlmanacState.Temperature;
                }
                else if (line.Contains("to-humidity"))
                { // just skip this line. We'll assume they are always in the same order
                    aState = AlmanacState.Humidity;
                }
                else if (line.Contains("to-location"))
                { // just skip this line. We'll assume they are always in the same order
                    aState = AlmanacState.Location;
                }
                else if ( line.Length < 2 )
                { // empty line
                    continue;
                }
                else if (aState == AlmanacState.Soil)
                { // add to seed-to-soil map
                    soil.AddMapData(line);
                }
                else if (aState == AlmanacState.Fertilizer)
                {
                    fertilizer.AddMapData(line);
                }
                else if (aState == AlmanacState.Water)
                {
                    water.AddMapData(line);
                }
                else if (aState == AlmanacState.Light)
                {
                    light.AddMapData(line);
                }
                else if (aState == AlmanacState.Temperature)
                {
                    temperature.AddMapData(line);
                }
                else if (aState == AlmanacState.Humidity)
                {
                    humidity.AddMapData(line);
                }
                else if (aState == AlmanacState.Location)
                {
                    location.AddMapData(line);
                }

            }

            long lowestLocation = long.MaxValue;
            foreach ( Seed seed in seeds )
            {
                for (long sn = seed.Number; sn < seed.Number + seed.Range; ++sn)
                {
                    //Console.Write($"Seed {seed.Number}, ");
                    var sd = soil.GetDestination(sn);
                    //Console.Write($"soil {sd}, ");
                    var fd = fertilizer.GetDestination(sd);
                    //Console.Write($"fertilizer {fd}, ");
                    var wd = water.GetDestination(fd);
                    //Console.Write($"water {wd}, ");
                    var ld = light.GetDestination(wd);
                    //Console.Write($"light {ld}, ");
                    var td = temperature.GetDestination(ld);
                    //Console.Write($"temperature {td}, ");
                    var hd = humidity.GetDestination(td);
                    //Console.Write($"humidity {hd}, ");
                    var loc = location.GetDestination(hd);
                    //Console.Write($"location {loc}");

                    if (lowestLocation > loc)
                        lowestLocation = loc;

                    //Console.WriteLine();
                }

            }

            Console.WriteLine($"Lowest location= {lowestLocation}");
        }
        else
        {
            Console.WriteLine($"File not found: {filePath}");
        }
    }
}
