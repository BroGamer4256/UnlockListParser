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
	public static int RoomThemeUnlockTblSize;
    public static int RoomThemeUnlockTblHexSize;
	public static int RoomThemeUnlockTblOffset;
	public static List<dynamic> RoomThemeReadData = new List<dynamic>();
	public static List<dynamic> RoomThemeWriteData = new List<dynamic>();
	public static int RoomPartsUnlockTblSize;
    public static int RoomPartsUnlockTblHexSize;
	public static int RoomPartsUnlockTblOffset;
	public static List<dynamic> RoomPartsReadData = new List<dynamic>();
	public static List<dynamic> RoomPartsWriteData = new List<dynamic>();
	public static int RoomItemsUnlockTblSize;
    public static int RoomItemsUnlockTblHexSize;
	public static int RoomItemsUnlockTblOffset;
	public static List<dynamic> RoomItemsReadData = new List<dynamic>();
	public static List<dynamic> RoomItemsWriteData = new List<dynamic>();

    public static void Write()
    {
		if(!File.Exists(@"unlock_list_new.bin"))
            File.Create(@"unlock_list_new.bin");
        //Writer();
		ModuleData();
		PVData();
        CMNData();
        VocaData();
		RoomThemeData();
		RoomPartData();
		RoomItemData();

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
		DataList.Add(RoomThemeReadData);
		DataList.Add(RoomPartsReadData);
		DataList.Add(RoomItemsReadData);

		foreach (var item in DataList)
		{
			var MainData = new List<byte>();
        	IntToHex(item);
			var fuckingbitch = new List<string>();
			var BWriter = new BinaryWriter(File.OpenWrite(@"unlock_list_new.bin"));

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
			foreach (string[] item3 in RoomThemeWriteData.ToArray())
			{
				foreach (string item4 in item3)
				{
					fuckingbitch.Add(item4);
				}
			}
			foreach (string[] item3 in RoomPartsWriteData.ToArray())
			{
				foreach (string item4 in item3)
				{
					fuckingbitch.Add(item4);
				}
			}
			foreach (string[] item3 in RoomItemsWriteData.ToArray())
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

			BWriter.Seek(ModuleUnlockTblOffset, SeekOrigin.Begin);
			for (int i = 0; i < MainData.Count; i++)
			{
				BWriter.Write(MainData[i]);
			}
			BWriter.Close();
		}
	}

	public static void RoomItemData()
	{
		var doc = new XmlDocument();
		doc.Load(@"unlock_list\\RoomItemUnlock.xml");
        var ReadNodes = new List<dynamic>();
		foreach (var node in doc.DocumentElement.ChildNodes)
		{
			ReadNodes.Add(node);
		}
        string[] xmlData = {};
        for (int i = 0; i < ReadNodes.Count; i++)
		{
			Array.Resize(ref xmlData, xmlData.Length + 1);
			xmlData[i] = ReadNodes[i].InnerXml;
			string[] xmlDataSplit = xmlData[i].Split(new string[] { "<", "/<", ">" }, StringSplitOptions.None);
            var TBA = new RoomItemUnlock() 
			{
				riItemID = Convert.ToInt32(xmlDataSplit[2]),
				riUnk01 = Convert.ToInt32(xmlDataSplit[6]),
				riUnlkReq = Convert.ToInt32(xmlDataSplit[10]),
				riUnk02 = Convert.ToInt32(xmlDataSplit[14]),
				riUnk03 = Convert.ToInt32(xmlDataSplit[18]),
				riLwClearDiff = Convert.ToInt32(xmlDataSplit[22]),
				riLwClearRank = Convert.ToInt32(xmlDataSplit[26]),
				riUnk04 = Convert.ToInt32(xmlDataSplit[30]),
				riUnk05 = Convert.ToInt32(xmlDataSplit[34]),
				riUnk06 = Convert.ToInt32(xmlDataSplit[38]),
				riUnk07 = Convert.ToInt32(xmlDataSplit[42]),
				riUnk08 = Convert.ToInt32(xmlDataSplit[46]),
				riUnk09 = Convert.ToInt32(xmlDataSplit[50]),
				riHgClearDiff = Convert.ToInt32(xmlDataSplit[54]),
				riHgClearRank = Convert.ToInt32(xmlDataSplit[58]),
				riUnk10 = Convert.ToInt32(xmlDataSplit[62]),
				riUnk11 = Convert.ToInt32(xmlDataSplit[66]),
				riUnk12 = Convert.ToInt32(xmlDataSplit[70])
			};
            RoomItemsReadData.Add(TBA);
		}
        RoomItemsUnlockTblSize = ReadNodes.Count;
        RoomItemsUnlockTblHexSize = HexRead.EntryLength("RoomItemUnlock")*(RoomItemsUnlockTblSize*4);
	}

	public static void RoomPartData()
	{
		var doc = new XmlDocument();
		doc.Load(@"unlock_list\\RoomPartsUnlock.xml");
        var ReadNodes = new List<dynamic>();
		foreach (var node in doc.DocumentElement.ChildNodes)
		{
			ReadNodes.Add(node);
		}
        string[] xmlData = {};
        for (int i = 0; i < ReadNodes.Count; i++)
		{
			Array.Resize(ref xmlData, xmlData.Length + 1);
			xmlData[i] = ReadNodes[i].InnerXml;
			string[] xmlDataSplit = xmlData[i].Split(new string[] { "<", "/<", ">" }, StringSplitOptions.None);
            var TBA = new RoomPartsUnlock() 
			{
				rpThemeID = Convert.ToInt32(xmlDataSplit[2]),
				rpUnk01 = Convert.ToInt32(xmlDataSplit[6]),
				rpUnlkFlag = Convert.ToInt32(xmlDataSplit[10]),
				rpUnk02 = Convert.ToInt32(xmlDataSplit[14]),
				rpUnk03 = Convert.ToInt32(xmlDataSplit[18]),
				rpLwDiff = Convert.ToInt32(xmlDataSplit[22]),
				rpLwRank = Convert.ToInt32(xmlDataSplit[26]),
				rpUnk04 = Convert.ToInt32(xmlDataSplit[30]),
				rpUnk05 = Convert.ToInt32(xmlDataSplit[34]),
				rpUnk06 = Convert.ToInt32(xmlDataSplit[38]),
				rpUnk07 = Convert.ToInt32(xmlDataSplit[42]),
				rpHgDiff = Convert.ToInt32(xmlDataSplit[46]),
				rpHgRank = Convert.ToInt32(xmlDataSplit[50]),
				rpUnkEnd = Convert.ToInt32(xmlDataSplit[54])
			};
            RoomPartsReadData.Add(TBA);
		}
        RoomPartsUnlockTblSize = ReadNodes.Count;
        RoomPartsUnlockTblHexSize = HexRead.EntryLength("RoomPartsUnlock")*(RoomPartsUnlockTblSize*4);
	}

	public static void RoomThemeData()
	{
		var doc = new XmlDocument();
		doc.Load(@"unlock_list\\RoomThemeUnlock.xml");
        var ReadNodes = new List<dynamic>();
		foreach (var node in doc.DocumentElement.ChildNodes)
		{
			ReadNodes.Add(node);
		}
        string[] xmlData = {};
        for (int i = 0; i < ReadNodes.Count; i++)
		{
			Array.Resize(ref xmlData, xmlData.Length + 1);
			xmlData[i] = ReadNodes[i].InnerXml;
			string[] xmlDataSplit = xmlData[i].Split(new string[] { "<", "/<", ">" }, StringSplitOptions.None);
            var TBA = new RoomThemeUnlock() 
			{
				rtThemeID = Convert.ToInt32(xmlDataSplit[2]),
				rtUnk01 = Convert.ToInt32(xmlDataSplit[6]),
				rtUnlkFlag = Convert.ToInt32(xmlDataSplit[10]),
				rtUnk02 = Convert.ToInt32(xmlDataSplit[14]),
				rtUnk03 = Convert.ToInt32(xmlDataSplit[18]),
				rtLwDiff = Convert.ToInt32(xmlDataSplit[22]),
				rtLwRank = Convert.ToInt32(xmlDataSplit[26]),
				rtUnk04 = Convert.ToInt32(xmlDataSplit[30]),
				rtUnk05 = Convert.ToInt32(xmlDataSplit[34]),
				rtUnk06 = Convert.ToInt32(xmlDataSplit[38]),
				rtUnk07 = Convert.ToInt32(xmlDataSplit[42]),
				rtHgDiff = Convert.ToInt32(xmlDataSplit[46]),
				rtHgRank = Convert.ToInt32(xmlDataSplit[50]),
				rtUnkEnd = Convert.ToInt32(xmlDataSplit[54]),
			};
            RoomThemeReadData.Add(TBA);
		}
        RoomThemeUnlockTblSize = ReadNodes.Count;
        RoomThemeUnlockTblHexSize = HexRead.EntryLength("RoomThemeUnlock")*(RoomThemeUnlockTblSize*4);
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
				case "RoomThemeUnlock":
					RoomThemeWriteData.Add(IntToHex(item.rtThemeID));
					RoomThemeWriteData.Add(IntToHex(item.rtUnk01));
					RoomThemeWriteData.Add(IntToHex(item.rtUnlkFlag));
					RoomThemeWriteData.Add(IntToHex(item.rtUnk02));
					RoomThemeWriteData.Add(IntToHex(item.rtUnk03));
					RoomThemeWriteData.Add(IntToHex(item.rtLwDiff));
					RoomThemeWriteData.Add(IntToHex(item.rtLwRank));
					RoomThemeWriteData.Add(IntToHex(item.rtUnk04));
					RoomThemeWriteData.Add(IntToHex(item.rtUnk05));
					RoomThemeWriteData.Add(IntToHex(item.rtUnk06));
					RoomThemeWriteData.Add(IntToHex(item.rtUnk07));
					RoomThemeWriteData.Add(IntToHex(item.rtHgDiff));
					RoomThemeWriteData.Add(IntToHex(item.rtHgRank));
					RoomThemeWriteData.Add(IntToHex(item.rtUnkEnd));
					break;
				case "RoomPartsUnlock":
					RoomPartsWriteData.Add(IntToHex(item.rpThemeID));
					RoomPartsWriteData.Add(IntToHex(item.rpUnk01));
					RoomPartsWriteData.Add(IntToHex(item.rpUnlkFlag));
					RoomPartsWriteData.Add(IntToHex(item.rpUnk02));
					RoomPartsWriteData.Add(IntToHex(item.rpUnk03));
					RoomPartsWriteData.Add(IntToHex(item.rpLwDiff));
					RoomPartsWriteData.Add(IntToHex(item.rpLwRank));
					RoomPartsWriteData.Add(IntToHex(item.rpUnk04));
					RoomPartsWriteData.Add(IntToHex(item.rpUnk05));
					RoomPartsWriteData.Add(IntToHex(item.rpUnk06));
					RoomPartsWriteData.Add(IntToHex(item.rpUnk07));
					RoomPartsWriteData.Add(IntToHex(item.rpHgDiff));
					RoomPartsWriteData.Add(IntToHex(item.rpHgRank));
					RoomPartsWriteData.Add(IntToHex(item.rpUnkEnd));
					break;
				case "RoomItemUnlock":
					RoomItemsWriteData.Add(IntToHex(item.riItemID));
					RoomItemsWriteData.Add(IntToHex(item.riUnk01));
					RoomItemsWriteData.Add(IntToHex(item.riUnlkReq));
					RoomItemsWriteData.Add(IntToHex(item.riUnk02));
					RoomItemsWriteData.Add(IntToHex(item.riUnk03));
					RoomItemsWriteData.Add(IntToHex(item.riLwClearDiff));
					RoomItemsWriteData.Add(IntToHex(item.riLwClearRank));
					RoomItemsWriteData.Add(IntToHex(item.riUnk04));
					RoomItemsWriteData.Add(IntToHex(item.riUnk05));
					RoomItemsWriteData.Add(IntToHex(item.riUnk06));
					RoomItemsWriteData.Add(IntToHex(item.riUnk07));
					RoomItemsWriteData.Add(IntToHex(item.riUnk08));
					RoomItemsWriteData.Add(IntToHex(item.riUnk09));
					RoomItemsWriteData.Add(IntToHex(item.riHgClearDiff));
					RoomItemsWriteData.Add(IntToHex(item.riHgClearRank));
					RoomItemsWriteData.Add(IntToHex(item.riUnk10));
					RoomItemsWriteData.Add(IntToHex(item.riUnk11));
					RoomItemsWriteData.Add(IntToHex(item.riUnk12));
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

        var BWriter = new BinaryWriter(File.OpenWrite(@"unlock_list_new.bin"));
        var HeaderData = new List<byte>();
		var intList = new List<int>();

		ModuleUnlockTblOffset = 128;
		PVUnlockTblOffset = ModuleUnlockTblOffset + ModuleUnlockTblHexSize;
		CMNDataUnlockTblOffset = PVUnlockTblOffset + PVUnlockTblHexSize;
		VocaRoomUnlockTblOffset = CMNDataUnlockTblOffset + CMNDataUnlockTblHexSize;
		RoomThemeUnlockTblOffset = VocaRoomUnlockTblOffset + VocaRoomUnlockTblHexSize;
		RoomPartsUnlockTblOffset = RoomThemeUnlockTblOffset + RoomThemeUnlockTblHexSize;
		RoomItemsUnlockTblOffset = RoomPartsUnlockTblOffset + RoomPartsUnlockTblHexSize;

		intList.Add(ModuleUnlockTblSize);
		intList.Add(ModuleUnlockTblOffset);
		intList.Add(PVUnlockTblSize);
		intList.Add(PVUnlockTblOffset);
		intList.Add(CMNDataUnlockTblSize);
		intList.Add(CMNDataUnlockTblOffset);
		intList.Add(VocaRoomUnlockTblSize);
		intList.Add(VocaRoomUnlockTblOffset);
		intList.Add(RoomThemeUnlockTblSize);
		intList.Add(RoomThemeUnlockTblOffset);
		intList.Add(RoomPartsUnlockTblSize);
		intList.Add(RoomPartsUnlockTblOffset);
		intList.Add(RoomItemsUnlockTblSize);
		intList.Add(RoomItemsUnlockTblOffset);

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
		var BWriter = new BinaryWriter(File.OpenWrite(@"unlock_list_new.bin"));
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