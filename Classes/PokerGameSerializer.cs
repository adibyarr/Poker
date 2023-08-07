using System.IO;
using System.Xml.Serialization;

namespace PokerCoba.Class;

public static class PokerGameSerializer
{
	public static void SerializeGame(string filePath, PokerGame pokerGame)
	{
		try
		{
			XmlSerializer serializer= new XmlSerializer(typeof(PokerGame));
			using(StreamWriter writer = new StreamWriter(filePath))
			{
				serializer.Serialize(writer, pokerGame);
			}
		}
		catch (Exception ex)
		{
				Console.Write($"Errorrr: {ex.Message}");
		}

	}
	public static PokerGame DeserializeGame(string filePath)
	{
		XmlSerializer serializer = new XmlSerializer(typeof(PokerGame));
		using(StreamReader reader = new StreamReader(filePath))
		{
			return (PokerGame)serializer.Deserialize(reader);
		}
		
	}
}