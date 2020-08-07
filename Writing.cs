using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

public class HexWrite
{
	public static int ModuleUnlockTblSize;
	public static int ModuleUnlockTblHexSize;
	public static int ModuleUnlockTblOffset;
	public static List<dynamic> ModuleUnlockReadData = new List<dynamic>();
	public static List<dynamic> ModuleUnlockWriteData = new List<dynamic>();
	public static int PVUnlockTblSize;
	public static int PVUnlockTblHexSize;
	public static int PVUnlockTblOffset;
	public static List<dynamic> PVUnlockReadData = new List<dynamic>();
	public static List<dynamic> PVUnlockWriteData = new List<dynamic>();
    public static int CMNDataUnlockTblSize;
    public static int CMNDataUnlockTblHexSize;
	public static int CMNDataUnlockTblOffset;
	public static List<dynamic> CMNReadData = new List<dynamic>();
	public static List<dynamic> CMNWriteData = new List<dynamic>();
	public static int VocaRoomUnlockTblSize;
    public static int VocaRoomUnlockTblHexSize;
	public static int VocaRoomUnlockTblOffset;
	public static List<dynamic> VocaRoomReadData = new List<dynamic>();
	public static List<dynamic> VocaRoomWriteData = new List<dynamic>();

    public static void Write()
    {
		if(!File.Exists(@"unlock_list.bin"))
            File.Create(@"unlock_list.bin");
        //Writer();
		ModuleData();
		PVData();
        CMNData();
        VocaData();

        HeaderData();
		MainData();
    }

	public static void MainData()
	{
		var DataList = new List<dynamic>();
		
		DataList.Add(ModuleUnlockReadData);
		DataList.Add(PVUnlockReadData);
		DataList.Add(CMNReadData);
		DataList.Add(VocaRoomReadData);

		foreach (var item in DataList)
		{
			var MainData = new List<byte>();
        	IntToHex(item);
			var fuckingbitch = new List<string>();
			var BWriter = new BinaryWriter(File.OpenWrite(@"unlock_list.bin"));

			foreach (string[] item3 in ModuleUnlockWriteData.ToArray())
			{
				foreach (string item4 in item3)
				{
					fuckingbitch.Add(item4);
				}
			}

			foreach (string[] item3 in PVUnlockWriteData.ToArray())
			{
				foreach (string item4 in item3)
				{
					fuckingbitch.Add(item4);
				}
			}

			foreach (string[] item3 in CMNWriteData.ToArray())
			{
				foreach (string item4 in item3)
				{
					fuckingbitch.Add(item4);
				}
			}

			foreach (string[] item3 in VocaRoomWriteData.ToArray())
			{
				foreach (string item4 in item3)
				{
					fuckingbitch.Add(item4);
				}
			}

			string[] dummyA = fuckingbitch.ToArray();
        	for (int i = 0; i < dummyA.Length; i++)
        	{
            	byte[] tempByte = HexRead.StringToByteArray(dummyA[i]);
				for (int i2 = 0; i2 < tempByte.Length; i2++)
				{
					MainData.Add(tempByte[i2]);
				}
        	}

			BWriter.Seek(120, SeekOrigin.Begin);
			for (int i = 0; i < MainData.Count; i++)
			{
				BWriter.Write(MainData[i]);
			}
			BWriter.Close();
		}
	}

    public static string[] IntToHex(int bitch)
    {
        string dummy = bitch.ToString("X");
        dummy = dummy.PadLeft(8, '0');
        string[] dummyA = {dummy.Substring(6,2), dummy.Substring(4,2), dummy.Substring(2,2), dummy.Substring(0,2)};
		return dummyA;
    }

	public static string[] IntToHex(List<dynamic> fucker)
	{
		string[] items = {};
		foreach (var item in fucker)
		{
			string dummy = Convert.ToString(item.GetType());
			switch (dummy)
			{
				case "ModuleUnlockEntry":
					ModuleUnlockWriteData.Add(IntToHex(item.ModuleID));
					ModuleUnlockWriteData.Add(IntToHex(item.muUnk01));
					ModuleUnlockWriteData.Add(IntToHex(item.muPvPID));
					ModuleUnlockWriteData.Add(IntToHex(item.muSCC));
					ModuleUnlockWriteData.Add(IntToHex(item.muUnk02));
					ModuleUnlockWriteData.Add(IntToHex(item.muDiffClr));
					ModuleUnlockWriteData.Add(IntToHex(item.muRankClr));
					ModuleUnlockWriteData.Add(IntToHex(item.muUnk05));
					ModuleUnlockWriteData.Add(IntToHex(item.muTCC));
					ModuleUnlockWriteData.Add(IntToHex(item.muUnk07));
					ModuleUnlockWriteData.Add(IntToHex(item.muUnk08));
					ModuleUnlockWriteData.Add(IntToHex(item.muUnk09));
					break;
				case "PvUnlockEntry":
					PVUnlockWriteData.Add(IntToHex(item.suPVID));
					PVUnlockWriteData.Add(IntToHex(item.suUnk01));
					PVUnlockWriteData.Add(IntToHex(item.suPVClr01));
					PVUnlockWriteData.Add(IntToHex(item.suPVClr02));
					PVUnlockWriteData.Add(IntToHex(item.suPVClr03));
					PVUnlockWriteData.Add(IntToHex(item.suPVClr04));
					PVUnlockWriteData.Add(IntToHex(item.suPVClr05));
					PVUnlockWriteData.Add(IntToHex(item.suPVClrDiff));
					PVUnlockWriteData.Add(IntToHex(item.suPVClrRank));
					PVUnlockWriteData.Add(IntToHex(item.suUnk02));
					PVUnlockWriteData.Add(IntToHex(item.suUnk03));
					PVUnlockWriteData.Add(IntToHex(item.suUnk04));
					PVUnlockWriteData.Add(IntToHex(item.suUnk05));
					PVUnlockWriteData.Add(IntToHex(item.suUnk06));
					PVUnlockWriteData.Add(IntToHex(item.suUnk07));
					PVUnlockWriteData.Add(IntToHex(item.suPVHighClrDiff));
					PVUnlockWriteData.Add(IntToHex(item.suPVHighClrRank));
					PVUnlockWriteData.Add(IntToHex(item.suUnk08));
					break;
				case "CmnUnlock":
					CMNWriteData.Add(IntToHex(item.cuModID));
					CMNWriteData.Add(IntToHex(item.cuUnk01));
					CMNWriteData.Add(IntToHex(item.cuPvPID));
					CMNWriteData.Add(IntToHex(item.cuSCC));
					CMNWriteData.Add(IntToHex(item.cuUnk02));
					CMNWriteData.Add(IntToHex(item.cuDiffClr));
					CMNWriteData.Add(IntToHex(item.cuRankClr));
					CMNWriteData.Add(IntToHex(item.cuUnk05));
					CMNWriteData.Add(IntToHex(item.cuTCC));
					CMNWriteData.Add(IntToHex(item.cuUnk07));
					CMNWriteData.Add(IntToHex(item.cuUnk08));
					CMNWriteData.Add(IntToHex(item.cuUnk09));
					break;
				case "VocaRoomUnlock":
					VocaRoomWriteData.Add(IntToHex(item.bvVocaloid));
					VocaRoomWriteData.Add(IntToHex(item.bvSongId));
					VocaRoomWriteData.Add(IntToHex(item.bvScc));
					break;
				default: 
					Console.WriteLine("Case not found"); 
					break;
			}
		}
		return items;
	}

    public static void HeaderData()
    {
        GC.Collect();
		GC.WaitForPendingFinalizers();

        var BWriter = new BinaryWriter(File.OpenWrite(@"unlock_list.bin"));
        var HeaderData = new List<byte>();
		var intList = new List<int>();

		ModuleUnlockTblOffset = 120;
		PVUnlockTblOffset = ModuleUnlockTblOffset + ModuleUnlockTblHexSize;
		CMNDataUnlockTblOffset = PVUnlockTblOffset + PVUnlockTblHexSize;
		VocaRoomUnlockTblOffset = CMNDataUnlockTblOffset + CMNDataUnlockTblHexSize;

		intList.Add(ModuleUnlockTblSize);
		intList.Add(ModuleUnlockTblOffset);
		intList.Add(PVUnlockTblSize);
		intList.Add(PVUnlockTblOffset);
		intList.Add(CMNDataUnlockTblSize);
		intList.Add(CMNDataUnlockTblOffset);
		intList.Add(VocaRoomUnlockTblSize);
		intList.Add(VocaRoomUnlockTblOffset);

		foreach (int item in intList)
		{
        	string[] dummyA = IntToHex(item);
        	for (int i = 0; i < dummyA.Length; i++)
        	{
            	byte[] tempByte = HexRead.StringToByteArray(dummyA[i]);
				for (int i2 = 0; i2 < tempByte.Length; i2++)
				{
					HeaderData.Add(tempByte[i2]);
				}
        	}
		}

        for (int i = 0; i < HeaderData.Count; i++)
		{
			BWriter.Write(HeaderData[i]);
		}
		BWriter.Close();
    }

	public static void PVData()
	{
		var doc = new XmlDocument();
		doc.Load(@"unlock_list\\PVUnlock.xml");
        var PVUnlockReadNode = new List<dynamic>();
		foreach (var node in doc.DocumentElement.ChildNodes)
		{
			PVUnlockReadNode.Add(node);
		}
        string[] xmlData = {};
        for (int i = 0; i < PVUnlockReadNode.Count; i++)
		{
			Array.Resize(ref xmlData, xmlData.Length + 1);
			xmlData[i] = PVUnlockReadNode[i].InnerXml;
			string[] xmlDataSplit = xmlData[i].Split(new string[] { "<", "/<", ">" }, StringSplitOptions.None);
            var TBA = new PvUnlockEntry() 
			{
				suPVID = Convert.ToInt32(xmlDataSplit[2]),
				suUnk01 = Convert.ToInt32(xmlDataSplit[6]),
				suPVClr01 = Convert.ToInt32(xmlDataSplit[10]),
				suPVClr02 = Convert.ToInt32(xmlDataSplit[14]),
				suPVClr03 = Convert.ToInt32(xmlDataSplit[18]),
				suPVClr04 = Convert.ToInt32(xmlDataSplit[22]),
				suPVClr05 = Convert.ToInt32(xmlDataSplit[26]),
				suPVClrDiff = Convert.ToInt32(xmlDataSplit[30]),
				suPVClrRank = Convert.ToInt32(xmlDataSplit[34]),
				suUnk02 = Convert.ToInt32(xmlDataSplit[38]),
				suUnk03 = Convert.ToInt32(xmlDataSplit[42]),
				suUnk04 = Convert.ToInt32(xmlDataSplit[46]),
				suUnk05 = Convert.ToInt32(xmlDataSplit[50]),
				suUnk06 = Convert.ToInt32(xmlDataSplit[54]),
				suUnk07 = Convert.ToInt32(xmlDataSplit[58]),
				suPVHighClrDiff = Convert.ToInt32(xmlDataSplit[62]),
				suPVHighClrRank = Convert.ToInt32(xmlDataSplit[66]),
				suUnk08 = Convert.ToInt32(xmlDataSplit[70])
			};
            PVUnlockReadData.Add(TBA);
		}
        PVUnlockTblSize = PVUnlockReadNode.Count;
        PVUnlockTblHexSize = HexRead.EntryLength("PVUnlock")*(PVUnlockTblSize*4);
	}

	public static void ModuleData()
	{
		var doc = new XmlDocument();
		doc.Load(@"unlock_list\\ModuleUnlock.xml");
        var ModuleUnlockReadNode = new List<dynamic>();
		foreach (var node in doc.DocumentElement.ChildNodes)
		{
			ModuleUnlockReadNode.Add(node);
		}
        string[] xmlData = {};
        for (int i = 0; i < ModuleUnlockReadNode.Count; i++)
		{
			Array.Resize(ref xmlData, xmlData.Length + 1);
			xmlData[i] = ModuleUnlockReadNode[i].InnerXml;
			string[] xmlDataSplit = xmlData[i].Split(new string[] { "<", "/<", ">" }, StringSplitOptions.None);
            var TBA = new ModuleUnlockEntry() 
			{
				ModuleID = Convert.ToInt32(xmlDataSplit[2]),
				muUnk01 = Convert.ToInt32(xmlDataSplit[6]),
				muPvPID = Convert.ToInt32(xmlDataSplit[10]),
				muSCC = Convert.ToInt32(xmlDataSplit[14]),
				muUnk02 = Convert.ToInt32(xmlDataSplit[18]),
				muDiffClr = Convert.ToInt32(xmlDataSplit[22]),
				muRankClr = Convert.ToInt32(xmlDataSplit[26]),
				muUnk05 = Convert.ToInt32(xmlDataSplit[30]),
				muTCC = Convert.ToInt32(xmlDataSplit[34]),
				muUnk07 = Convert.ToInt32(xmlDataSplit[38]),
				muUnk08 = Convert.ToInt32(xmlDataSplit[42]),
				muUnk09 = Convert.ToInt32(xmlDataSplit[46])
			};
            ModuleUnlockReadData.Add(TBA);
		}
        ModuleUnlockTblSize = ModuleUnlockReadNode.Count;
        ModuleUnlockTblHexSize = HexRead.EntryLength("ModuleUnlock")*(ModuleUnlockTblSize*4);
	}

    public static void Writer()
    {
        GC.Collect();
		GC.WaitForPendingFinalizers();

		var OverflowData = new List<byte>();
		var fs = new FileStream(@"unlock_list\\rawInfo.hex", FileMode.Open);
		var BWriter = new BinaryWriter(File.OpenWrite(@"unlock_list.bin"));
		var info = new FileInfo(@"unlock_list\\rawInfo.hex");

		fs.Seek(0, SeekOrigin.Begin);
		for (int i = 0; i < info.Length; i++)
		{
			string tempString = string.Format("{0:X2}", fs.ReadByte());
			byte[] tempByte = HexRead.StringToByteArray(tempString);
			for (int i2 = 0; i2 < tempByte.Length; i2++)
			{
				OverflowData.Add(tempByte[i2]);
			}
		}
		fs.Close();

		for (int i = 0; i < OverflowData.Count; i++)
		{
			BWriter.Write(OverflowData[i]);
		}
		BWriter.Close();
    }

    public static void VocaData()
    {
        var doc = new XmlDocument();
		doc.Load(@"unlock_list\\VocaRoomUnlock.xml");
        var VocaRoomReadNode = new List<dynamic>();
		foreach (var node in doc.DocumentElement.ChildNodes)
		{
			VocaRoomReadNode.Add(node);
		}
        string[] xmlData = {};
        for (int i = 0; i < VocaRoomReadNode.Count; i++)
		{
			Array.Resize(ref xmlData, xmlData.Length + 1);
			xmlData[i] = VocaRoomReadNode[i].InnerXml;
			string[] xmlDataSplit = xmlData[i].Split(new string[] { "<", "/<", ">" }, StringSplitOptions.None);
            var TBA = new VocaRoomUnlock() 
			{
				bvVocaloid = Convert.ToInt32(xmlDataSplit[2]),
                bvSongId = Convert.ToInt32(xmlDataSplit[6]),
                bvScc = Convert.ToInt32(xmlDataSplit[10])
			};
            VocaRoomReadData.Add(TBA);
		}
        VocaRoomUnlockTblSize = VocaRoomReadNode.Count;
        VocaRoomUnlockTblHexSize = HexRead.EntryLength("VocaRoomUnlock")*(VocaRoomUnlockTblSize*4);
    }

    public static void CMNData()
    {
        var doc = new XmlDocument();
		doc.Load(@"unlock_list\\CMNITMUnlock.xml");
		var CMNReadNode = new List<dynamic>();
		foreach (var node in doc.DocumentElement.ChildNodes)
		{
			CMNReadNode.Add(node);
		}
		string[] xmlData = {};
		for (int i = 0; i < CMNReadNode.Count; i++)
		{
			Array.Resize(ref xmlData, xmlData.Length + 1);
			xmlData[i] = CMNReadNode[i].InnerXml;
			string[] xmlDataSplit = xmlData[i].Split(new string[] { "<", "/<", ">" }, StringSplitOptions.None);
			var TBA = new CmnUnlock() 
			{
				cuModID = Convert.ToInt32(xmlDataSplit[2]),
				cuUnk01 = Convert.ToInt32(xmlDataSplit[6]), 
				cuPvPID = Convert.ToInt32(xmlDataSplit[10]), 
				cuSCC = Convert.ToInt32(xmlDataSplit[14]),
				cuUnk02 = Convert.ToInt32(xmlDataSplit[18]),
				cuDiffClr = Convert.ToInt32(xmlDataSplit[22]), 
				cuRankClr = Convert.ToInt32(xmlDataSplit[26]), 
				cuUnk05 = Convert.ToInt32(xmlDataSplit[30]), 
				cuTCC = Convert.ToInt32(xmlDataSplit[34]), 
				cuUnk07 = Convert.ToInt32(xmlDataSplit[38]), 
				cuUnk08 = Convert.ToInt32(xmlDataSplit[42]), 
				cuUnk09 = Convert.ToInt32(xmlDataSplit[46])
			};
            CMNReadData.Add(TBA);
		}
        CMNDataUnlockTblSize = CMNReadNode.Count;
        CMNDataUnlockTblHexSize = HexRead.EntryLength("CMNITMUnlock")*(CMNDataUnlockTblSize*4);
    }
}