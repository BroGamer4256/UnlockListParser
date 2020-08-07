using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

public class HexRead
{
	static void Main(string[] args)
	{
		if (File.GetAttributes(args[0]).HasFlag(FileAttributes.Directory) && args[0].EndsWith("unlock_list"))
		{
			HexWrite.Write();
			Environment.Exit(0);
		}
		if (!args[0].EndsWith("unlock_list.bin") || !args[0].EndsWith("unlock_list_new.bin"))
			throw new Exception("Not unlock_list.bin");
		if (Directory.Exists("unlock_list"))
		{
			foreach (var file in Directory.GetFiles("unlock_list"))
				File.Delete(file); 
			Directory.Delete("unlock_list");
		}
		Directory.CreateDirectory("unlock_list");
		var fs = new FileStream(args[0], FileMode.Open);
		int hexIn;
		var hex = new List<string>();
		var tempHex = new List<string>();
		int tempCount = 0;
		int offset = 0;
		
		for (int i = 0; (hexIn = fs.ReadByte()) != -1; i++)
		{
			tempCount++;
			tempHex.Add(string.Format("{0:X2}", hexIn));
			if (tempCount == 4)
			{
				hex.Add(tempHex[3] + tempHex[2] + tempHex[1] + tempHex[0]);
				tempCount = 0;
				tempHex.Clear();
			}
		}
		fs.Close();

		GC.Collect();
		GC.WaitForPendingFinalizers();

		var ModuleUnlock = new DataStruct() 
		{
			arrarySize = Convert.ToUInt32(hex[0 + offset], 16), 
			offsetStart = Convert.ToUInt32(hex[1 + offset], 16), 
			type = "ModuleUnlock"
		};
		offset += 2;
		var PVUnlock = new DataStruct() 
		{
			arrarySize = Convert.ToUInt32(hex[0 + offset], 16), 
			offsetStart = Convert.ToUInt32(hex[1 + offset], 16), 
			type = "PVUnlock"
		};
		offset += 2;
		var CMNITMUnlock = new DataStruct() 
		{
			arrarySize = Convert.ToUInt32(hex[0 + offset], 16), 
			offsetStart = Convert.ToUInt32(hex[1 + offset], 16), 
			type = "CMNITMUnlock"
		};
		offset += 2;
		var VocaRoomUnlock = new DataStruct() 
		{
			arrarySize = Convert.ToUInt32(hex[0 + offset], 16), 
			offsetStart = Convert.ToUInt32(hex[1 + offset], 16), 
			type = "VocaRoomUnlock"
		};
		offset += 2;
		var RoomThemeUnlock = new DataStruct() 
		{
			arrarySize = Convert.ToUInt32(hex[0 + offset], 16), 
			offsetStart = Convert.ToUInt32(hex[1 + offset], 16), 
			type = "RoomThemeUnlock"
		};
		offset += 2;
		var RoomPartsUnlock = new DataStruct() 
		{
			arrarySize = Convert.ToUInt32(hex[0 + offset], 16), 
			offsetStart = Convert.ToUInt32(hex[1 + offset], 16), 
			type = "RoomPartsUnlock"
		};
		offset += 2;
		var RoomItemUnlock = new DataStruct() 
		{
			arrarySize = Convert.ToUInt32(hex[0 + offset], 16), 
			offsetStart = Convert.ToUInt32(hex[1 + offset], 16), 
			type = "RoomItemUnlock"
		};
		offset += 2;
		var GiftItemUnlock = new DataStruct() 
		{
			arrarySize = Convert.ToUInt32(hex[0 + offset], 16), 
			offsetStart = Convert.ToUInt32(hex[1 + offset], 16)
		};
		offset += 2;
		var PVTitleUnlock = new DataStruct() 
		{
			arrarySize = Convert.ToUInt32(hex[0 + offset], 16), 
			offsetStart = Convert.ToUInt32(hex[1 + offset], 16)
		};
		offset += 2;
		var RoomTitleUnlock = new DataStruct() 
		{
			arrarySize = Convert.ToUInt32(hex[0 + offset], 16), 
			offsetStart = Convert.ToUInt32(hex[1 + offset], 16)
		};
		offset += 2;
		var EditTitleUnlock = new DataStruct() 
		{
			arrarySize = Convert.ToUInt32(hex[0 + offset], 16), 
			offsetStart = Convert.ToUInt32(hex[1 + offset], 16)
		};
		offset += 2;
		var ARTitleUnlock = new DataStruct() 
		{
			arrarySize = Convert.ToUInt32(hex[0 + offset], 16), 
			offsetStart = Convert.ToUInt32(hex[1 + offset], 16)
		};
		offset += 2;
		var NetworkTitleUnlock = new DataStruct() 
		{
			arrarySize = Convert.ToUInt32(hex[0 + offset], 16), 
			offsetStart = Convert.ToUInt32(hex[1 + offset], 16)
		};
		offset += 2;
		var ItemTitleUnlock = new DataStruct() 
		{
			arrarySize = Convert.ToUInt32(hex[0 + offset], 16), 
			offsetStart = Convert.ToUInt32(hex[1 + offset], 16)
		};
		offset += 2;
		var SystemTitleUnlock = new DataStruct() 
		{
			arrarySize = Convert.ToUInt32(hex[0 + offset], 16), 
			offsetStart = Convert.ToUInt32(hex[1 + offset], 16)
		};

		var ModuleData = ModuleUnlock.Data(args[0]);

		var PVData = PVUnlock.Data(args[0]);

		var CMNData = CMNITMUnlock.Data(args[0]);

		var VocalRoomData = VocaRoomUnlock.Data(args[0]);

		var RoomThemeData = RoomThemeUnlock.Data(args[0]);

		xmlWrite(ModuleData, ModuleUnlock.type);
		xmlWrite(PVData, PVUnlock.type);
		xmlWrite(CMNData, CMNITMUnlock.type);
		xmlWrite(VocalRoomData, VocaRoomUnlock.type);
		xmlWrite(RoomThemeData, RoomThemeUnlock.type);

		GC.Collect();
		GC.WaitForPendingFinalizers();

		var headerInfo = new List<byte>();
		fs = new FileStream(args[0], FileMode.Open);
		var BWriter = new BinaryWriter(File.OpenWrite(@"unlock_list\\rawInfo.hex"));
		var info = new FileInfo(args[0]);

		fs.Seek(0, SeekOrigin.Begin);
		for (int i = 0; i < info.Length; i++)
		{
			string tempString = string.Format("{0:X2}", fs.ReadByte());
			byte[] tempByte = StringToByteArray(tempString);
			for (int i2 = 0; i2 < tempByte.Length; i2++)
			{
				headerInfo.Add(tempByte[i2]);
			}
		}
		fs.Close();

		for (int i = 0; i < headerInfo.Count; i++)
		{
			BWriter.Write(headerInfo[i]);
		}
		BWriter.Close();
	}

	public static int EntryLength(string Table)
	{
		switch(Table)
		{
			case "ModuleUnlock": return 12;
			case "PVUnlock": return 18;
			case "CMNITMUnlock": return 12;
			case "VocaRoomUnlock": return 3;
			case "RoomThemeUnlock": return 14;
			case "RoomPartsUnlock": return 14;
			case "RoomItemUnlock": return 18;
			default: return 0;
		}
	}

	public static void xmlWrite (List<dynamic> output, string Object)
	{
		var xmlWriterSettings = new XmlWriterSettings() { Indent = true };
		var Unlock = new Unlock{ ObjectList = output, name =  Object};
		var xsSubmit = new XmlSerializer(typeof(Unlock), new XmlRootAttribute(Object));
		string xml = "";
		using (var sww = new StringWriter())
		{
			using (var writers = XmlWriter.Create(sww, xmlWriterSettings))
			{
				xsSubmit.Serialize(writers, Unlock);
				xml = Convert.ToString(sww);
			}
		}
		if (!File.Exists(@"unlock_list\\" + Object + ".xml"))
			File.Create(@"unlock_list\\" + Object + ".xml");
		GC.Collect();
		GC.WaitForPendingFinalizers();
		xml = xml.Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>", "<?xml version=\"1.0\"?>");
		File.WriteAllText(@"unlock_list\\" + Object + ".xml", xml);
	}

	public static byte[] StringToByteArray(string hex)
	{
		int NumberChars = hex.Length;
		byte[] bytes = new byte[NumberChars / 2];
		for (int i = 0; i < NumberChars; i += 2)
			bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
		return bytes;
	}
}
