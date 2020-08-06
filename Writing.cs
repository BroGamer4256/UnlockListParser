using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

public class HexWrite
{
    public static int VocaRoomUnlockTblSize;
    public static int VocaRoomUnlockTblHexSize;
	public static int VocaRoomUnlockTblOffset;
    public static int CMNDataUnlockTblSize;
    public static int CMNDataUnlockTblHexSize;
	public static int CMNDataUnlockTblOffset;

    public static void Write()
    {
        Writer();
        CMNData();
        VocaData();

        HeaderData();
    }

    public static string[] IntToHex(int bitch)
    {
        string dummy = bitch.ToString("X");
        dummy = dummy.PadLeft(8, '0');
        string[] dummyA = {dummy.Substring(6,2), dummy.Substring(4,2), dummy.Substring(2,2), dummy.Substring(0,2)};
		return dummyA;
    }

    public static void HeaderData()
    {
        GC.Collect();
		GC.WaitForPendingFinalizers();

        var BWriter = new BinaryWriter(File.OpenWrite(@"unlock_list.bin"));
        var HeaderData = new List<byte>();
		var intList = new List<int>();

		intList.Add(VocaRoomUnlockTblSize);
		intList.Add(CMNDataUnlockTblSize);

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

    public static void Writer()
    {
        if(!File.Exists(@"unlock_list.bin"))
            File.Create(@"unlock_list.bin");
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
		var VocaRoomReadData = new List<dynamic>();
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
		var CMNReadData = new List<dynamic>();
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