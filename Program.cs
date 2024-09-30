namespace filbehandling_JSONSerializer_oppgave;
using System;
using System.IO;
using System.Text.Json;
class Program
{
    static void Main(string[] args)
    {
        try
        {
            string filePath = "kaffedrikk.json";

            List<Kaffedrikk>?   bestilling = new List<Kaffedrikk>();
            if (File.Exists(filePath))
            {
                string existingJSON = File.ReadAllText(filePath);
                Console.WriteLine($"Data already exists whitin the file kaffedrikk.json {File.ReadAllText(filePath)}");
                if(!String.IsNullOrWhiteSpace(existingJSON))
                {
                    bestilling = JsonSerializer.Deserialize<List<Kaffedrikk>>(existingJSON);
                }
            }

            Console.WriteLine("Vil du har varm eller kald kaffe?");
            string? temp = Console.ReadLine();
            Console.WriteLine("Hvor mangen ønsker du?");
            string? antallInput = Console.ReadLine();
            int antall;
            while (!int.TryParse(antallInput, out antall))
            {
                Console.WriteLine("Det oppstod en feil, vennligst oppgi antallet ditt i tall");
                antallInput = Console.ReadLine();
            }
            Console.WriteLine("Skal du sitte her eller ta med?");
            string? hvor = Console.ReadLine();

            var newKaffedrikk = new Kaffedrikk
            {
                Temp = temp,
                Antall = antall,
                Hvor = hvor
            };

            bestilling.Add(newKaffedrikk);
            Console.WriteLine($"Din bestilling er: {newKaffedrikk.Temp} kaffe, og du vil ha {newKaffedrikk.Antall} stykk. Du øsnker å {newKaffedrikk.Hvor} (med) din kaffe.");

            string Json = JsonSerializer.Serialize(bestilling, new JsonSerializerOptions { WriteIndented = true});

            File.WriteAllText(filePath, Json);

            Console.WriteLine("Data was succsessfully written to JSON object!");
        }
        catch (IOException exception)
        {
            Console.WriteLine($"An error occured while attempting to write to the file kaffedrikk.json: {exception.Message}");
        }
        catch (Exception exception)
        {
            Console.WriteLine($"{exception.Message}\n");
        }
        
    }
}
